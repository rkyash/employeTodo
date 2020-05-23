using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTODoTask.Model
{
    [Table("emp_record")]
    public class Employee
    {
        [Key]
        public long id { get; set; }
        public string emp_name { get; set; }
    }

    [Table("emp_task")]
    public class EmployeeTask
    {
        [Key]
        public long id { get; set; }
        public long emp_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public long prioritylevel_id { get; set; }
        public long state_id { get; set; }
        public string estimate { get; set; }
    }

    [Table("priority_level")]
    public class Prioritylevel
    {
        [Key]
        public long id { get; set; }
        public string priority_level { get; set; }
    }

    [Table("statetbl")]
    public class State
    {
        [Key]
        public long id { get; set; }
        public string state_name { get; set; }
    }

}
