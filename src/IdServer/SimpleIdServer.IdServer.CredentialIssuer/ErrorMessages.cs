﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace SimpleIdServer.IdServer.CredentialIssuer
{
    public static class ErrorMessages
    {
        public const string CREDENTIAL_REQUEST_INVALID = "the incoming request is invalid";
        public const string MISSING_PARAMETER = "the parameter {0} is missing";
        public const string UNSUPPORTED_FORMAT = "the format {0} is not supported";
        public const string UNKNOWN_ACCESS_TOKEN = "either the access token has been revoked or is invalid";
        public const string INVALID_ACCESS_TOKEN_SCOPE = "access token has an invalid scope";
        public const string UNKNOWN_CREDENTIAL_OFFER = "the credential offer doesn't exist";
    }
}
