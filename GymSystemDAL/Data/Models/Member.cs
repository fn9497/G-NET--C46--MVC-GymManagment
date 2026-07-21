using GymSystemDAL.Data.Models;
using GymSystemDAL.Models;

namespace GymSystem.Models
{
    public class Member :GymUser
    {
        public string ? Photo { get; set; }
        //joindate==CreatedAt
<<<<<<< HEAD
<<<<<<< Updated upstream
=======

        #region Relationship
       public  HealthRecord HealthRecord { get; set; } = default!;   
=======

        #region Relationship
         HealthRecord HealthRecord { get; set; } = default!;   
>>>>>>> dev
        
        public ICollection<Membership> Memberships { get; set; } = default!;

        public ICollection<Booking> MemberSession { get; set; } = default!;
        #endregion
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
>>>>>>> dev
    }
}
