using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
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
            //string request = "POST /fff/uhu?Id=5&name=john#fragment HTTP/1.1\r\n"
            //               + "Host: localhost:5000\r\n"
            //               + "Autorization: bla bla bla 00e0rr40r4\r\n"
            //               + "Date: " + DateTime.Now + "\r\n"
            //               + "\r\n"
            //               + "username=pesho&password=123";



            //HttpRequest httpRequest = new HttpRequest(request);

            //HttpResponse response = new HttpResponse(HttpResponseStatusCode.BadRequest);
            //response.AddHeader(new HttpHeader("Host", "localhost:99999"));
            //response.AddHeader(new HttpHeader("Date", "0343434304343"));

            //response.Content = Encoding.UTF8.GetBytes("<h1>Hello</h1>");
            //Console.WriteLine(Encoding.UTF8.GetString(response.GetBytes()));

            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new HtmlResult("<h1>Hello World!</h1>", HttpResponseStatusCode.OK));

            Server server = new Server(8000, serverRoutingTable);

            server.Run();

            ;

        }
    }
}
