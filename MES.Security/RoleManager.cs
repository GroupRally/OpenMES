using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace MES.Security
{
    public class RoleManager
    {

        public const string SystemUser_BuiltIn = "MES";
        public const string SystemRole_SupperUser = "SupperUser";
        public const string SystemRole_ForbiddenUser = "ForbiddenUser";
        public const string SystemRole_LineMaster = "LineMaster";
        public const string SystemRole_LineOperator = "LineOperator";

        public IDictionary<string, string[]> GetAllUsersInRoles() 
        {
            Dictionary<string, string[]> returnValue = null;

            string[] users = null;

            string[] roles = Roles.GetAllRoles();

            if ((roles != null) && (roles.Length > 0))
            {
                returnValue = new Dictionary<string, string[]>();
            }

            foreach (string role in roles)
            {
                users = Roles.GetUsersInRole(role);

                if ((users != null) && (users.Length > 0))
                {
                    users = ((string[])(users.Where((o) => (o.ToUpper() != SystemUser_BuiltIn))));
                }

                returnValue.Add(role, users);
            }

            return returnValue;
        }

        public string[] SetUserRole(string UserName, string RoleName) 
        {
            string[] userRoles = Roles.GetRolesForUser(UserName);

            if ((userRoles != null) && (userRoles.Length > 0))
            {
                Roles.RemoveUserFromRoles(UserName, userRoles);
            }

            Roles.AddUserToRole(UserName, RoleName);

            return Roles.GetRolesForUser(UserName);
        }
    }
}
