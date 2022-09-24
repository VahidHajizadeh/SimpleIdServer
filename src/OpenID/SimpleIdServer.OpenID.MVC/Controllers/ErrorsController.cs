﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Mvc;
using SimpleIdServer.OpenID.UI.ViewModels;

namespace SimpleIdServer.OpenID.MVC.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index(string code, string message)
        {
            return View(new ErrorViewModel(code, message));
        }
    }
}
