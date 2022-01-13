using APITesting.Attributes;
using APITesting.Helper;
using Newtonsoft.Json;
using RestSharp;

namespace APITesting
{
    public class UserAction
    {
        public UserList GetUsers()
        {
            var restClient = new RestClient("https://reqres.in");
            var restRequest = new RestRequest("/api/users?page=2", Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            var users = JsonConvert.DeserializeObject<UserList>(content);
            return users;
        }

      //  public User CreateUser()
       // {
           // var restClient = new RestClient("https://reqres.in");
          //  var restRequest = new RestRequest("/api/users", Method.POST);
           // restRequest.AddHeader("Accept", "application/json");
           // restRequest.RequestFormat = DataFormat.Json;

           // IRestResponse response = restClient.Execute(restRequest);
           // var content = response.Content;

            //var users = JsonConvert.DeserializeObject<User>(content);

            
            


       // }
    }
}
