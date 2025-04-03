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
            var options = new RestClientOptions(url);
            var restClient = new RestClient(options);
            return restClient;
        }

        public RestRequest PostRequestWithBody(string request)
        {
            restRequest = new RestRequest();
            restRequest.Method = Method.Post;
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddJsonBody(request);
            return restRequest;
        }

        public RestRequest UpdateRequestWithBody(string request)
        {
            restRequest = new RestRequest();
            restRequest.Method = Method.Put;
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddJsonBody(request);
            return restRequest;
        }

        public RestRequest DeleteRequest()
        {
            restRequest = new RestRequest();
            restRequest.Method = Method.Delete;
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public RestRequest SendRequest()
        {
            restRequest = new RestRequest();
            restRequest.Method = Method.Get;
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public RestResponse GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }

        public Attributes GetContent<Attributes>(RestResponse restResponse)
        {
            var content = restResponse.Content;
            Attributes attribute = JsonConvert.DeserializeObject<Attributes>(content);
            return attribute;
        }
    }
}
