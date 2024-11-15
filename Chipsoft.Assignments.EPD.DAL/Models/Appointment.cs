using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPD.DAL.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int PhysicianId { get; set; }
        public Physician Physician { get; set; }
    }
}
