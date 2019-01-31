﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GraduwayExamApp.API.Attributes
{
    class AuthorizedAttribute : TypeFilterAttribute
    {
        public AuthorizedAttribute() : base(typeof(AuthorizeFilter))
        {
            //Arguments = new object[] {new Claim()};
            //}
            //public AuthorizeAttribute(string claimType, string claimValue) : base(typeof(AuthorizeFilter))
            //{
            //    Arguments = new object[] { new Claim(claimType, claimValue) };
            //}
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        //readonly Claim _claim;

        public AuthorizeFilter(/*Claim claim*/)
        {
            //_claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"];
            //HttpContext.Request.Headers["Authorization"]
            //var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            var hasClaim = !string.IsNullOrEmpty(token);
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
