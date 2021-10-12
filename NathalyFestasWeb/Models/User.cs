using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NathalyFestasWeb.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public byte[] PassHash { get; set; }
        public User(string username, string password)
        {
            Username = username;
            var sha = SHA512.Create();
            var pBytes = Encoding.UTF8.GetBytes(password);
            PassHash = sha.ComputeHash(pBytes);
        }
        public bool CheckPass(string password)
        {
            var sha = SHA512.Create();
            var pBytes = Encoding.UTF8.GetBytes(password);
            var hashed = sha.ComputeHash(pBytes);
            for(int i = 0; i < PassHash.Length; i++)
            {
                if (PassHash[i] != hashed[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
