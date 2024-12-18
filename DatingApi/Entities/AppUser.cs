﻿using DatingApi.Interfaces;

namespace DatingApi.Entities
{
    public class AppUser : IMultiName
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string? ArName { get; set; }
        public string? EnName { get; set; }
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public required string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public required string Gender { get; set; }
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interests { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge();
        //}
    }
}
