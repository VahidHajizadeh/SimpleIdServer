﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimpleIdServer.IdServer.Jobs;
using SimpleIdServer.IdServer.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace SimpleIdServer.IdServer.Api.Provisioning
{
    public class IdentityProvisioningController : BaseController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IJwtBuilder _jwtBuilder;

        public IdentityProvisioningController(IServiceProvider serviceProvider, IJwtBuilder jwtBuilder)
        {
            _serviceProvider = serviceProvider;
            _jwtBuilder = jwtBuilder;
        }

        public IActionResult Enqueue([FromRoute] string prefix, string name, string id)
        {
            prefix = prefix ?? Constants.DefaultRealm;
            CheckAccessToken(prefix, Constants.StandardScopes.Provisioning.Name, _jwtBuilder);
            var jobs = _serviceProvider.GetRequiredService<IEnumerable<IRepresentationExtractionJob>>();
            var job = jobs.SingleOrDefault(j => j.Name == name);
            BackgroundJob.Enqueue(() => job.Execute(id, prefix));
            return new NoContentResult();
        }

        public IActionResult Import([FromRoute] string prefix)
        {
            prefix = prefix ?? Constants.DefaultRealm;
            CheckAccessToken(prefix, Constants.StandardScopes.Provisioning.Name, _jwtBuilder);
            string id = Guid.NewGuid().ToString();
            BackgroundJob.Enqueue<IImportRepresentationJob>((j) => j.Execute(prefix, id));
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.Created,
                Content = JsonSerializer.Serialize(new { id = id }),
                ContentType = "application/json"
            };
        }
    }
}
