using App.Data;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.TestApp.Controllers;
using SIS.WebServer;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;
using System;
using System.Text;

namespace SIS.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            // Get
            serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest => new HomeController(httpRequest).Index(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/login", httpRequest => new UsersController().Login(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/register", httpRequest => new UsersController().Register(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/logout", httpRequest => new UsersController().Logout(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/home", httpRequest => new HomeController(httpRequest).Home(httpRequest));

            // Post
            serverRoutingTable.Add(HttpRequestMethod.Post, "/login", httpRequest => new UsersController().LoginConfirm(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/register", httpRequest => new UsersController().RegisterConfirm(httpRequest));


            Server server = new Server(8000, serverRoutingTable);

            server.Run();

            ;

        }
    }
}
