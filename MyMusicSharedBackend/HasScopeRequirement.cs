using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicSharedBackend
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }

        public string Issuer { get; }
        public string Scope { get; }
    }
}