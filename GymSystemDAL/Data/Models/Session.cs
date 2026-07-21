using GymSystemDAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
    public class Session : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description   { get; set; } = default;

        public int Capacity { get; set; }

        #region Relationship
        public Trainer Trainer { get; set; } = default!;    
        public int TrainerId { get; set; }

        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; } 

        public ICollection<Booking> SessionMembers { get; set; } = default!;
        #endregion
    }
}
