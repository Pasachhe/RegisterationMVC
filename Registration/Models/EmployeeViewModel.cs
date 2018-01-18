using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class EmployeeViewModel
    {

        public int EmployeeId { get; set; }
        //public string Name { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DesId { get; set; }

        public Nullable<int>Salary{ get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        //Custom attribute
        public string DepartmentName { get; set; }
        public string UserName { get; set; }
        public string DesName { get; set; }
        public bool Remember { get; set; }
        public string SiteName { get; set; }


    }
}