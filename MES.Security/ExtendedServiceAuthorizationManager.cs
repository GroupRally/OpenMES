using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;

namespace MES.Security.Authorization
{
    public class ExtendedServiceAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            AuthorizationAttribute authorizationAttribute = null;

            string requestedUri = String.Empty;

            // Finds out which REST method is being requested
            if(WebOperationContext.Current.IncomingRequest.UriTemplateMatch != null)
            {
                requestedUri = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.Data.ToString(); 
            }

            // Skips WCF help pages
            if ((requestedUri.Equals("GetHelpPage")) || (requestedUri.Equals("GetOperationHelpPage")))
            {
                return true;
            }

            //Checks if Authentication required custom attribute is set
            object[] attributes = null;

            if(!String.IsNullOrEmpty(requestedUri))
            {
                attributes = operationContext.EndpointDispatcher.DispatchRuntime.Type.GetMethod(requestedUri).GetCustomAttributes(true);
            }

            if ((attributes != null) && (attributes.Length > 0))
            {
                foreach (var attribute in attributes)
                {
                    if (attribute is AuthorizationAttribute)
                    {
                        authorizationAttribute = attribute as AuthorizationAttribute;
                        break;
                    }
                }
            }

            if ((authorizationAttribute == null) || (authorizationAttribute.IsRequiringAuthentication == false))
            {
                return true;
            }

            string token = WebOperationContext.Current.IncomingRequest.Headers[HttpRequestHeader.Authorization];

            if (String.IsNullOrEmpty(token))
            {
                return false;
            }

            string[] credentials = token.Split(new string[]{":"}, StringSplitOptions.None);

            if ((credentials == null) || (credentials.Length < 2))
            {
                return false;
            }

            //if ((Membership.ValidateUser(credentials[0], credentials[1])) && (!Roles.IsUserInRole(credentials[0], RoleManager.SystemRole_ForbiddenUser))) 
            if (Membership.ValidateUser(credentials[0], credentials[1])) 
            {
                if ((authorizationAttribute.Roles != null) && (authorizationAttribute.Roles.Length > 0))
                {
                    foreach (string roleName in authorizationAttribute.Roles)
                    {
                        if (Roles.IsUserInRole(credentials[0], roleName))
                        {
                            return true;
                        }
                    }

                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                    return false;
                }
                else if (Roles.IsUserInRole(credentials[0], RoleManager.SystemRole_ForbiddenUser))
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                    return false;
                }

                return true;
            }
            else
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                return false;
            }
        }
    }

    public class AuthorizationAttribute : Attribute 
    {
        public bool IsRequiringAuthentication { get; set; }
        public string[] Roles { get; set; }
    }
}