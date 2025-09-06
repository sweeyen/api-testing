using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace APITesting.Helper
{
    public class ApiHelper<T>
    {
        private RestClient RestClient;
        private RestRequest RestRequest;
        private String BaseUrl = @"https://reqres.in";

        public RestClient SetUrl(String endpoint)
        {
            var url = String.Concat(BaseUrl, endpoint);
            RestClient = new RestClient(new RestClientOptions(url)); // Simplified 'new' expression
            return RestClient;
        }

        public RestRequest PostRequestWithBody(string request)
        {
            RestRequest = new RestRequest
            {
                Method = Method.Post
            };
            RestRequest.AddHeader("Accept", "application/json");
            var headers = new Dictionary<string, string>
            {
                { "x-api-key", "reqres-free-v1" },
                { "Accept", "application/json"  }
            };
            RestRequest.AddHeaders(headers);
            RestRequest.AddJsonBody(request);
            return RestRequest;
        }

        public RestRequest UpdateRequestWithBody(string request)
        {
            RestRequest = new RestRequest
            {
                Method = Method.Put
            };
            var headers = new Dictionary<string, string>
            {
                { "x-api-key", "reqres-free-v1" },
                { "Accept", "application/json"  }
            };
            RestRequest.AddHeaders(headers);
            RestRequest.AddJsonBody(request);
            return RestRequest;
        }

        public RestRequest DeleteRequest()
        {
            RestRequest = new RestRequest
            {
                Method = Method.Delete
            };
            var headers = new Dictionary<string, string>
            {
                { "x-api-key", "reqres-free-v1" },
                { "Accept", "application/json"  }
            };
            RestRequest.AddHeaders(headers);
            return RestRequest;
        }

        public RestRequest SendRequest()
        {
            RestRequest = new RestRequest
            {
                Method = Method.Get
            };
            RestRequest.AddHeader("Accept", "application/json");
            return RestRequest;
        }

        public RestResponse GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }

        public TResult GetContent<TResult>(RestResponse response) // Renamed the inner type parameter to TResult
        {
            var content = response.Content;
            TResult attribute = JsonConvert.DeserializeObject<TResult>(content);
            return attribute;
        }

        public void DisposeRestClient()
        {
            RestClient.Dispose();
        }
    }
}
