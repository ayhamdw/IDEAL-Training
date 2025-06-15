
using DataEntity.Models;
using System.Security.Claims;

namespace Services.Helpers
{
    public class AuthenticationHelper
    {
        public static bool CheckAuthentication(string pageName, string permissionKey, string userName)
        {
            try
            {
                using (var db = new ProjectBaseContext())
                {
                    var user = db.AspNetUsers.FirstOrDefault(x => x.UserName == userName);
                    if (user == null)
                    {
                        //User Not Exist
                        return false;
                    }

                    var userRoles = db.AspNetUserRoles.Where(x => x.UserId == user.Id).ToList();
                    foreach (var role in userRoles)
                    {
                        var permission = db.Permissions.FirstOrDefault(x => x.PageName.Equals(pageName) && x.PermissionKey.Equals(permissionKey));
                        if (permission == null || role == null)
                        {
                            return false;
                        }
                        var userHasPermissions = db.RolePermissions.Any(x => x.RoleId == role.RoleId.ToString() && x.PermissionId == permission.Id);
                        if (userHasPermissions)
                        {
                            return true;
                        }
                    }
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(userName, ex, "Error while check authentication!");
                return false;
            }

           
        }
        public static string? GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetUserEmail(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";
        }
    }
}
