
using log4net;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SearchAndFind.Services
{
    public class CloudMessageSender : ICloudMessageSender
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string FIREBASE_URL = "firebaseURL";
        private readonly string API_KEY = "apiKey";
        private readonly string ERROR_CODE = "-1";

        public void SendMessage(CloudMessage cloudMessage)
        {
            var result = ERROR_CODE;
            try
            {
               
                var httpWebRequest = GetHttpWebRequest();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = BuildJson(cloudMessage);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                if (result.Equals(ERROR_CODE))
                {
                    logger.Error("Error with notifications, result:" + result);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                logger.Error("Error with notifications: " +result);
                throw new ServiceOperationException("Error al realizar notificaciones");
            }
        }

        private string BuildJson(CloudMessage cloudMessage)
        {
            return JsonConvert.SerializeObject(cloudMessage);
        }

        private HttpWebRequest GetHttpWebRequest()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(GetPropertyFromConfiguration(FIREBASE_URL));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization:key=" + GetPropertyFromConfiguration(API_KEY));
            httpWebRequest.Method = "POST";
            return httpWebRequest;
        }

        private string GetPropertyFromConfiguration(string propertyName)
        {
            return ConfigurationManager.AppSettings.Get(propertyName);
        }
    }
}
