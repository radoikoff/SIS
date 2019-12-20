using App.Data;
using App.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.TestApp.Controllers
{
    public class UsersController : BaseController
    {
        //Get
        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }

        //Post
        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new AppDbContext())
            {
                string username = httpRequest.FormData["username"].ToString();
                string password = httpRequest.FormData["password"].ToString();

                User user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    return this.Redirect("/login");
                }

                httpRequest.Session.AddParameter("username", user.Username);
            }

            return this.Redirect("/home");
        }

        //Get
        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }

        //Post
        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var context = new AppDbContext())
            {
                string username = httpRequest.FormData["username"].ToString();
                string password = httpRequest.FormData["password"].ToString();
                string confirmPassword = httpRequest.FormData["confirmPassword"].ToString();

                if (password != confirmPassword)
                {
                    return this.Redirect("/register");
                }

                User user = new User(username, password);

                context.Users.Add(user);
                context.SaveChanges();
            }

            return this.Redirect("/login");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();

            return this.Redirect("/");
        }
    }
}
