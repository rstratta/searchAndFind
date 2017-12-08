using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SearchAndFind.ApiRest
{
    public class ControllerHelper
    {
        private static string TOKEN_NAME = "token";
        private static string AUTHENTICATION_TYPE = "authType";

        public static string GetTokenFromHttpRequest(HttpRequestMessage request)
        {
            return GetDataFromHttpRequest(request, TOKEN_NAME);
        }

        public static string GetAuthenticationTypeFromHttpRequest(HttpRequestMessage request)
        {
            return GetDataFromHttpRequest(request, AUTHENTICATION_TYPE);
        }

        private static string GetDataFromHttpRequest(HttpRequestMessage request, string fieldName)
        {
            string result = null;
            if (request != null && request.Headers.Contains(fieldName))
            {
                result = request.Headers.GetValues(fieldName).First();
            }
            return result;
        }
    }
}