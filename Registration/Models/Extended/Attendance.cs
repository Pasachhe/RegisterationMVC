using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    [MetadataType(typeof(AttendanceMetaData))]
    public partial class Attendance
    {
        class AttendanceMetaData
        {
            [Required(ErrorMessage = "Attendance are required")]
            public string AttendanceId { get; set; }
            public Nullable<int> EmployeeId { get; set; }
            public Nullable<System.DateTime> Date { get; set; }
            public Nullable<System.DateTime> InTime { get; set; }
            public Nullable<System.DateTime> OutTime { get; set; }
            public Nullable<System.TimeSpan> WorkHours { get; set; }
        }

    }
}