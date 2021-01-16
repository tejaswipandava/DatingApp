using System;

namespace API.Entities
{
    public class appUser
    {
        public int id { get; set; }
        public string UserName { get; set; }

        public Byte[] PasswordSalt { get; set; }
        public Byte[] passwordHash { get; set; }
    }
}