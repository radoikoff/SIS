using SIS.HTTP.Requests;
using System;

namespace SIS.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string request = "POST /fff/uhu?Id=5&name=john#fragment HTTP/1.1\r\n"
                           + "Host: localhost:5000\r\n"
                           + "Autorization: bla bla bla 00e0rr40r4\r\n"
                           + "Date: " + DateTime.Now + "\r\n"
                           + "\r\n"
                           + "username=pesho&password=123";



            HttpRequest httpRequest = new HttpRequest(request);

            ;

        }
    }
}
