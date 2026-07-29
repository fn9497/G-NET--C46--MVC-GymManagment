using GymSystemDAL.Models;
using GymSystemDAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.TrainerViewModel
{
    public class TrainerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ? Photo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } 
        public string Gender { get; set; }
        public string? Address { get; set; }
        public string? DateOfBirth { get; set; }
        public List<Speciality> Specialities { get; set; }

        public List<Session> Sessions { get; set; }
    }
}
