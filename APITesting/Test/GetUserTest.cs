using APITesting.Attributes;
using APITesting.Helper;
using NUnit.Framework;
using System;

namespace APITesting.Test
{
    public class GetUserTest
    {
        internal string id;

        [Test]
        [Description("Retrieve user listing")]
        public void VerifyUserList()
        {
            var user = new ApiHelper<UserList>();
            var url = user.SetUrl("/api/users?page=2");
            var request = user.SendRequest();
            var response = user.GetResponse(url, request);
            var content = user.GetContent<UserList>(response);

            Assert.AreEqual(6, content.Data.Count, "Total User is not {0}",6);

        }

        [Test]
        [Description("Create a new user")]
        public void CreateUser()
        {
            var name = "Kenny";
            string bodyText = @"{""name"": ""Kenny"",""job"": ""leader""}";
            var user = new ApiHelper<User>();
            var url = user.SetUrl("/api/users");
            var request = user.PostRequestWithBody(bodyText);
            var response = user.GetResponse(url, request);
            User content = user.GetContent<User>(response);
            id = content.Id.ToString();
            
            Assert.AreEqual(name, content.Name, "Name is not {0}", name);            
        }

        [Test]
        [Description("Update user job detail")]
        public void UpdateUser()
        {
            var name = "Kenny";
            string bodyText = @"{""name"": ""Kenny"",""job"": ""cashier""}";
            var user = new ApiHelper<User>();
            var url = user.SetUrl("/api/users/2");
            var request = user.UpdateRequestWithBody(bodyText);
            var response = user.GetResponse(url, request);
            User content = user.GetContent<User>(response);

            Assert.AreEqual(name, content.Name, "Name is not {0}", name);
            Assert.AreEqual("cashier", content.Job, "Job is not {0}", name);
        }

        [Test]
        [Description("Delete a user")]
        public void DeleteUser()
        {
            var user = new ApiHelper<User>();

            var url = user.SetUrl($"/api/users/{id}");
            var request = user.DeleteRequest();
            var response = user.GetResponse(url, request);
            User content = user.GetContent<User>(response);

            Assert.AreEqual("NoContent",response.StatusCode.ToString() , "User is not deleted");
        }

    }
}
