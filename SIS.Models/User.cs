﻿using System;

namespace App.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
