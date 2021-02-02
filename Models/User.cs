using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class User
    {
        public string Username { get; set; }
        public int Id { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] Salt { get; set; }

        public List<Character> MyProperty { get; set; }
    }
}
