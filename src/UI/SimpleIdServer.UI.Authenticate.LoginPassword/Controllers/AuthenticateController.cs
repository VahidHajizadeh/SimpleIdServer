﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleIdServer.Common.Exceptions;
using SimpleIdServer.OAuth.Extensions;
using SimpleIdServer.OAuth.Helpers;
using SimpleIdServer.OAuth.Persistence;
using SimpleIdServer.OpenID.Extensions;
using SimpleIdServer.OpenID.Helpers;
using SimpleIdServer.OpenID.MVC.Controllers;
using SimpleIdServer.OpenID.Options;
using SimpleIdServer.UI.Authenticate.LoginPassword.Services;
using SimpleIdServer.UI.Authenticate.LoginPassword.ViewModels;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.UI.Authenticate.LoginPassword.Controllers
{
    [Area(Constants.AMR)]
    public class AuthenticateController : BaseAuthenticateController
    {
        private readonly IPasswordAuthService _passwordAuthService;
        private readonly ITranslationHelper _translationHelper;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public AuthenticateController(
            IOptions<OpenIDHostOptions> options,
            IPasswordAuthService passwordAuthService,
            ITranslationHelper translationHelper,
            IAuthenticationSchemeProvider authenticationSchemeProvider,
            IDataProtectionProvider dataProtectionProvider,
            IAmrHelper amrHelper,
            IOAuthClientRepository oauthClientRepository,
            IOAuthUserRepository oauthUserCommandRepository) : base(options, dataProtectionProvider, oauthClientRepository, amrHelper, oauthUserCommandRepository)
        {
            _passwordAuthService = passwordAuthService;
            _translationHelper = translationHelper;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Errors", new { code = "invalid_request", ReturnUrl = $"{Request.Path}{Request.QueryString}", area = string.Empty });
            }

            try
            {
                var schemes = await _authenticationSchemeProvider.GetAllSchemesAsync();
                var query = Unprotect(returnUrl).GetQueries().ToJObj();
                var clientId = query.GetClientIdFromAuthorizationRequest();
                var client = await OAuthClientQueryRepository.FindOAuthClientById(clientId, cancellationToken);
                var loginHint = query.GetLoginHintFromAuthorizationRequest();
                var externalIdProviders = schemes.Where(s => !string.IsNullOrWhiteSpace(s.DisplayName));
                return View(new AuthenticateViewModel(
                    loginHint, 
                    returnUrl,
                    _translationHelper.Translate(client.ClientNames),
                    _translationHelper.Translate(client.LogoUris),
                    _translationHelper.Translate(client.TosUris),
                    _translationHelper.Translate(client.PolicyUris),
                    externalIdProviders.Select(e => new OpenID.UI.ViewModels.ExternalIdProvider
                    {
                        AuthenticationScheme = e.Name,
                        DisplayName = e.DisplayName
                    }).ToList()));
            }
            catch(CryptographicException)
            {
                return RedirectToAction("Index", "Errors", new { code = "invalid_request", ReturnUrl = $"{Request.Path}{Request.QueryString}", area = string.Empty });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AuthenticateViewModel viewModel, CancellationToken token)
        {
            if (viewModel == null)
            {
                return RedirectToAction("Index", "Errors", new { code = "invalid_request", ReturnUrl = $"{Request.Path}{Request.QueryString}", area = string.Empty });
            }

            viewModel.Check(ModelState);
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var user = await _passwordAuthService.Authenticate(viewModel.Login, viewModel.Password, token);
                return await Authenticate(viewModel.ReturnUrl, Constants.AMR, user, token, viewModel.RememberLogin);
            }
            catch (CryptographicException)
            {
                ModelState.AddModelError("invalid_request", "invalid_request");
                return View(viewModel);
            }
            catch (BaseUIException ex)
            {
                ModelState.AddModelError(ex.Code, ex.Code);
                return View(viewModel);
            }
        }
    }
}