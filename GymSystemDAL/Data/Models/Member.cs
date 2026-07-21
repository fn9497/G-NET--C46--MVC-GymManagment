using GymSystemDAL.Models;

namespace GymSystem.Models
{
    public class Member :GymUser
    {
        public string ? Photo { get; set; }
        //joindate==CreatedAt
<<<<<<< Updated upstream
=======

        #region Relationship
       public  HealthRecord HealthRecord { get; set; } = default!;   
        
        public ICollection<Membership> Memberships { get; set; } = default!;

        public ICollection<Booking> MemberSession { get; set; } = default!;
        #endregion
>>>>>>> Stashed changes
    }
}
