using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // there is one to many relationship between App user and Photo Class as one user can have many photos
        // we need to fully define the relationship as we dont want AppUserID colum in the Photos table to be nullable and weant cascade delete means if we delete the user photo also should delete for it we use EF conventions
        public ICollection<Photo> Photos { get; set; }
        // // as we not had any method to calulate Age we need to use Extension method to extend the functionality of the DateTime Class
        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }

  }
}