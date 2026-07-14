using GymSystemDAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
    public class Trainer :GymUser
    {
        //HireDate ==> Created At
        public Speciality Speciality { get; set; }

    }
}
