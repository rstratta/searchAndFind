using log4net;
using Newtonsoft.Json;
using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class GoogleAuthenticationChecker : IAuthenticationChecker
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IAuthenticationChecker searchAndFindAuthenticator;
        private static readonly string GOOGLE_URL_PROPERTY_NAME = "tokenAuthURL";

        public GoogleAuthenticationChecker(IAuthenticationChecker sAndFAuth)
        {
            searchAndFindAuthenticator = sAndFAuth;
        }
        private void CheckAuthentication(UserRequest request)
        {
            try { 
            var url =GetGoogleURLFromConfiguration() + request.CurrentToken;
            HttpWebRequest googleRequest;
            googleRequest = WebRequest.Create(url) as HttpWebRequest;
            googleRequest.Timeout = 10 * 1000;
            googleRequest.Method = "GET";
            HttpWebResponse response = googleRequest.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string resp = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                logger.Error("Error with google authentication: ", e);
                throw new AuthenticationException("Por favor, por su seguridad, vuelva a autenticarse.");
            }
        }

        private static string GetGoogleURLFromConfiguration()
        {
            return ConfigurationManager.AppSettings.Get(GOOGLE_URL_PROPERTY_NAME);
        }

        public void CheckClientAuthentication(UserRequest request)
        {
            CheckAuthentication(request);
            searchAndFindAuthenticator.CheckClientAuthentication(request);
        }

        public void CheckSalerAuthentication(UserRequest request)
        {
            CheckAuthentication(request);
            searchAndFindAuthenticator.CheckSalerAuthentication(request);
        }

        public void CheckBothProfileAuthentication(UserRequest request)
        {
            CheckAuthentication(request);
            searchAndFindAuthenticator.CheckBothProfileAuthentication(request);
        }
    }
}
