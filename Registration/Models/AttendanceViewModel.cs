using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class AttendanceViewModel
    {
        public string AttendanceId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public Nullable<double> WorkHours { get; set; }
        public Nullable<double> Wage { get; set; }

        public string EmployeeName { get; set; }
       

    }
}