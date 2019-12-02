using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Wappa.Teste.Project.Infra.Helper
{
    public class GoogleHelper
    {
        public static GoogleModelHelper GetCoordinate(string zipCode)
        {
            GoogleModelHelper result = new GoogleModelHelper();
            WebRequest request = WebRequest.Create(string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=true&key=AIzaSyC105gZaWIpkmTfzZ4m7Nb2IAHRWhAZ9cY", zipCode));
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                result = JsonConvert.DeserializeObject<GoogleModelHelper>(reader.ReadToEnd());

                if (result != null && result.Results != null)
                {
                    return result;
                }
            }
            return result;
        }
    }
}
