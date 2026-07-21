using GymSystemDAL.Data.Models;
using GymSystemDAL.Models;

namespace GymSystem.Models
{
    public class Member :GymUser
    {
        public string ? Photo { get; set; }
        //joindate==CreatedAt
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< Updated upstream
=======

        #region Relationship
       public  HealthRecord HealthRecord { get; set; } = default!;   
=======

        #region Relationship
         HealthRecord HealthRecord { get; set; } = default!;   
>>>>>>> dev
=======

        #region Relationship
       public  HealthRecord HealthRecord { get; set; } = default!;   
>>>>>>> backup-before-merge
        
        public ICollection<Membership> Memberships { get; set; } = default!;

        public ICollection<Booking> MemberSession { get; set; } = default!;
        #endregion
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
>>>>>>> dev
=======

>>>>>>> backup-before-merge
    }
}
