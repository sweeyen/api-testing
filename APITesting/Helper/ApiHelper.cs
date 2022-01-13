using System;
using Newtonsoft.Json;
using RestSharp;

namespace APITesting.Helper
{
    public class ApiHelper<T>
    {
        public RestClient restClient;
        public RestRequest restRequest;
        public RestResponse restResponse;
        public String baseUrl = @"https://reqres.in";

        public RestClient SetUrl(String endpoint)
        {
            var url = String.Concat(baseUrl, endpoint);
            var restClient = new RestClient(url);
            return restClient;
        }

        public RestRequest PostRequestWithBody(string request)
        {
            restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", request, ParameterType.RequestBody);
            return restRequest;
        }

        public RestRequest UpdateRequestWithBody(string request)
        {
            restRequest = new RestRequest(Method.PUT);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", request, ParameterType.RequestBody);
            return restRequest;
        }


        public RestRequest DeleteRequest()
        {
            restRequest = new RestRequest(Method.DELETE);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public RestRequest SendRequest()
        {
            restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public IRestResponse GetResponse(RestClient restClient,RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }

        public Attributes GetContent<Attributes>(IRestResponse restResponse)
        {
            var content = restResponse.Content;
            Attributes attribute = JsonConvert.DeserializeObject<Attributes>(content);
            return attribute;
        }

    }
}
