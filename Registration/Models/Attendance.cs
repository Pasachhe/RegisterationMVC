
namespace Registration.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public string AttendanceId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> InTime { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public Nullable<double> WorkHours { get; set; }
        public Nullable<double> Wage { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
