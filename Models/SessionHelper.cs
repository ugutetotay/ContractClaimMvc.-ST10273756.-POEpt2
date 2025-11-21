using Microsoft.AspNetCore.Http;

namespace ContractClaimMvc.Models
{
    public static class SessionHelper
    {
        // Check if user is logged in
        public static bool IsLoggedIn(ISession session)
        {
            return session.GetInt32("UserId").HasValue;
        }

        // Get current user ID
        public static int? GetUserId(ISession session)
        {
            return session.GetInt32("UserId");
        }

        // Get current user role
        public static string? GetUserRole(ISession session)
        {
            return session.GetString("Role");
        }

        // Get current user name
        public static string? GetUserName(ISession session)
        {
            return session.GetString("FullName");
        }

        // Check if user has required role
        public static bool HasRole(ISession session, params string[] allowedRoles)
        {
            var userRole = GetUserRole(session);
            return userRole != null && allowedRoles.Contains(userRole);
        }

        // Redirect to login if not authenticated
        public static bool RequireAuth(ISession session, HttpResponse response)
        {
            if (!IsLoggedIn(session))
            {
                response.Redirect("/Account/Login");
                return false;
            }
            return true;
        }

        // Check authorization for specific roles
        public static bool RequireRole(ISession session, HttpResponse response, params string[] allowedRoles)
        {
            if (!IsLoggedIn(session))
            {
                response.Redirect("/Account/Login");
                return false;
            }

            if (!HasRole(session, allowedRoles))
            {
                response.Redirect("/Home/AccessDenied");
                return false;
            }

            return true;
        }
    }
}