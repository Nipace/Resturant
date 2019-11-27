using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDoyle.Common
{
    public class APIHelper
    {

        public RestSharp.RestRequest PreparePOSTRequest_Form_URLEncoded(string uri)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            //var serviceUrl = ConfigurationManager.AppSettings["CREWGOSMSAPI"].ToString();            
            var request = new RestSharp.RestRequest(uri, RestSharp.Method.POST);
            request.AddHeader("Accept", "application/x-www-form-urlencoded");
            return request;
        }

        public RestSharp.RestRequest PrepareGETRequest_Form_URLEncoded(string tail)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            //var serviceUrl = ConfigurationManager.AppSettings["CREWGOSMSAPI"].ToString();            
            var request = new RestSharp.RestRequest(tail, RestSharp.Method.GET);
            //request.Method = RestSharp.Method.GET;
            //request.AddHeader("Accept", "application/x-www-form-urlencoded");
            return request;
        }

        public string GET(string URI)
        {
            var client = new RestSharp.RestClient(URI);
            var request = new RestSharp.RestRequest(RestSharp.Method.GET);
            RestSharp.IRestResponse response = null;
            try
            {
                response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex){

            }
            return null;
        }

        public string POST(string URI, Dictionary<string,string> postParams = null)
        {
            var client = new RestSharp.RestClient(URI);
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            //var serviceUrl = ConfigurationManager.AppSettings["CREWGOSMSAPI"].ToString();            
            var request = new RestSharp.RestRequest(URI, RestSharp.Method.POST);
            request.AddHeader("Accept", "application/x-www-form-urlencoded");
            RestSharp.IRestResponse response = null;

            if (postParams != null)
            {                
                foreach(System.Collections.Generic.KeyValuePair<string, string> vocab in postParams)
                {
                    request.AddParameter(vocab.Key, vocab.Value);
                }               
            }
            try
            {
                response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {

            }
            return null;

        }


    }
}
