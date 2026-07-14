using GymSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
    public class HealthRecord :BaseEntity
    {
        public decimal Weight { get; set; } 
        public decimal Height { get; set; }
        public string? Note { get; set; }

        public string BloodType { get; set; } = default;

        #region Relationship
            public Member Member { get; set; } = default!;
        public int memberId { get; set; }
        #endregion
    }
}
