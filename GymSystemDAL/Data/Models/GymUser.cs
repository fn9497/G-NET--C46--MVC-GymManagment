using GymSystemDAL.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
    public class GymUser : BaseEntity
    {
        public string Name { get; set; } = default;
        public string Email { get; set; } = default;
        public string Phone { get; set; } = default;
        public DateTime DataOfBirth { get; set; }
        public Address Address { get; set; } = default;
        public Gender Gender { get; set; } 
    }
    [Owned]
    public class Address
    {   public string Street { get; set; } = default;
        public string City { get; set; } = default;
        public int BuildingNumber { get; set; } 
    }
}
