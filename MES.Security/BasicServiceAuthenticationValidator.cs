using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace MES.Security.Authentication
{
    public class BasicServiceAuthenticationValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (!FormsAuthentication.Authenticate(userName, password))
            {
                throw new SecurityTokenException("Authentication Failed!");
            }
        }
    }
}