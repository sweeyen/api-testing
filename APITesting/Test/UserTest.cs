using System.Net;
using APITesting.Attributes;
using APITesting.Helper;
using NUnit.Framework;

namespace APITesting.Test
{
    [TestFixture]
    public class UserTest
    {
        private string id;
        private string name = "Kenny";
        private ApiHelper<User> user;
        private ApiHelper<UserList> userList;

        [TearDown]
        public void TearDown()
        {
            if (user != null)
            {
                user.DisposeRestClient();
                user = null;
            }
            if (userList != null)
            {
                userList.DisposeRestClient();
                userList = null;
            }
        }

        [Test]
        [Description("Retrieve user listing")]
        public void VerifyUserList()
        {
            userList = new ApiHelper<UserList>();
            var url = userList.SetUrl("/api/users?page=2");
            var request = userList.SendRequest();
            var response = userList.GetResponse(url, request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response body :" + $"{response.Content}");
            var content = userList.GetContent<UserList>(response);

            Assert.That(6, Is.EqualTo(content.Data.Count), "Incorrect user count");
        }

        [Test]
        [Description("Create a new user")]
        public void CreateUser()
        {
            user = new ApiHelper<User>();
            string bodyText = @"{""name"": ""Kenny"",""job"": ""leader""}";
            var url = user.SetUrl("/api/users");
            var request = user.PostRequestWithBody(bodyText);
            var response = user.GetResponse(url, request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created),"Response body :"+$"{response.Content}");
            User content = user.GetContent<User>(response);
            id = content.Id.ToString();

            Assert.That(name, Is.EqualTo(content.Name), "Invalid user created");
        }

        [Test]
        [Description("Update user job detail")]
        public void UpdateUser()
        {
            user = new ApiHelper<User>();
            string bodyText = @"{""name"": ""Kenny"",""job"": ""cashier""}";
            var url = user.SetUrl("/api/users/2");
            var request = user.UpdateRequestWithBody(bodyText);
            var response = user.GetResponse(url, request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response body :" + $"{response.Content}");
            User content = user.GetContent<User>(response);

            Assert.That(name, Is.EqualTo(content.Name));
            Assert.That("cashier", Is.EqualTo(content.Job), "Job is not {0}", content.Job);
        }

        [Test]
        [Description("Delete a user")]
        public void DeleteUser()
        {
            user = new ApiHelper<User>();
            var url = user.SetUrl($"/api/users/{id}");
            var request = user.DeleteRequest();
            var response = user.GetResponse(url, request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent), "Response body :" + $"{response.Content}");
            User content = user.GetContent<User>(response);
            Assert.That("NoContent", Is.EqualTo(response.StatusCode.ToString()), "User is not deleted");
        }
    }
}
