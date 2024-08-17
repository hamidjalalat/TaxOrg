
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Anemic.Entities
{
    public class User : IdentityUser, ISoftDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public string OrganizationalUnit { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public byte[]? SecretKey { get; set; }
        public System.DateTime OTPExpirationTime { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}