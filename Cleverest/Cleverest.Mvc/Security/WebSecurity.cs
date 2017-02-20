using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Cleverest.Business.Entities;
using Cleverest.Core.Security;
using Cleverest.Mvc.Security;

namespace Cleverest.Mvc.Security
{
    public static class WebSecurity
    {
        public static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        public static bool IsUserContextAvailable()
        {
            return Context != null &&
                    Context.User != null &&
                    Context.User.Identity != null &&
                    Context.User.Identity.IsAuthenticated;              
        }

        public static ExtendedPrincipal User
        {
            get
            {
                return IsUserContextAvailable() ? (ExtendedPrincipal)Context.User : null;
            }
        }

        public static Account Authenticate(string email, string password)
        {
            var account = Site.Managers.Account.GetByEmail(email);
            if (account == null)
                return null;

            var newPasswordHash = new CryptoHelper().GenerateHash(password, account.PasswordSalt);

            if (!newPasswordHash.Equals(account.Password))
                return null;

            return account;
        }

        public static bool AccountExists(string email)
        {
            return Site.Managers.Account.GetByEmail(email) != null;
        }

        public static bool SetAuthenticationCookie(string email, bool isPersisted)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var account = Site.Managers.Account.GetByEmail(email);
            if (account == null)
                return false;

            try
            {
                var serializedModel = new ExtendedPrincipalSerializedModel()
                {
                    Name = account.Name,
                    Email = account.Email,
                    Id = account.Id
                };
                var serializer = new JavaScriptSerializer();
                var serializedUserData = serializer.Serialize(serializedModel);

                var ticket = new FormsAuthenticationTicket(1, account.Email, DateTime.Now, DateTime.Now.AddHours(8), isPersisted, serializedUserData);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

                HttpContext.Current.Response.Cookies.Add(cookie);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void ProcessPostAuthenticateRequest()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return;

            try
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);

                var customPrincipalSerializedModel = new JavaScriptSerializer().Deserialize<ExtendedPrincipalSerializedModel>(ticket.UserData);
                var userToSet = new ExtendedPrincipal(customPrincipalSerializedModel.Email)
                {
                    Email = customPrincipalSerializedModel.Email,
                    Name = customPrincipalSerializedModel.Name,
                    Id = customPrincipalSerializedModel.Id
                };

                HttpContext.Current.User = userToSet;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
