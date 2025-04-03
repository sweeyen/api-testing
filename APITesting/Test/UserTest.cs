using APITesting.Attributes;
using APITesting.Helper;
using NUnit.Framework;
using System;

namespace APITesting.Test
{
    [TestFixture]
  
    public class UserTest : IDisposable
    {
        internal string id;
        private string name = "Kenny";

        [Test]
        [Description("Retrieve user listing")]
        public void VerifyUserList()
        {
            var user = new ApiHelper<UserList>();
            var url = user.SetUrl("/api/users?page=2");
            var request = user.SendRequest();
            var response = user.GetResponse(url, request);
            var content = user.GetContent<UserList>(response);

            Assert.That(6, Is.EqualTo(content.Data.Count), "Incorrect user count");

        }

        [Test]
        [Description("Create a new user")]
        public void CreateUser()
        {
            string bodyText = @"{""name"": ""Kenny"",""job"": ""leader""}";
            var user = new ApiHelper<User>();
            var url = user.SetUrl("/api/users");
            var request = user.PostRequestWithBody(bodyText);
            var response = user.GetResponse(url, request);
            User content = user.GetContent<User>(response);
            id = content.Id.ToString();

            Assert.That(name, Is.EqualTo(content.Name), "Invalid user created");    
        }

        [Test]
        [Description("Update user job detail")]
        public void UpdateUser()
        {
            string bodyText = @"{""name"": ""Kenny"",""job"": ""cashier""}";
            var user = new ApiHelper<User>();
            var url = user.SetUrl("/api/users/2");
            var request = user.UpdateRequestWithBody(bodyText);
            var response = user.GetResponse(url, request);
            User content = user.GetContent<User>(response);

            Assert.That(name, Is.EqualTo(content.Name));
            Assert.That("cashier", Is.EqualTo(content.Job), "Job is not {0}", content.Job);
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

            Assert.That("NoContent", Is.EqualTo(response.StatusCode.ToString()) , "User is not deleted");
        }

        public void Dispose()
        {

        }
    }
}
