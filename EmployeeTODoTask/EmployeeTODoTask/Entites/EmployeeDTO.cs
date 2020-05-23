using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTODoTask.Entites
{
    public class EmployeeDTO
    {
        public long id { get; set; }
        public string emp_name { get; set; }
    }

    public class EmployeeTaskDTO
    {
        public long id { get; set; }
        public long emp_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string prioritylevel { get; set; }
        public long prioritylevel_id { get; set; }
        public string state { get; set; }
        public long state_id { get; set; }
        public string estimate { get; set; }
    }
    

    public class PriorityOrStateDTO
    {
        public List<PrioritylevelDTO> prioritylevels { get; set; }
        public List<StateDTO> states { get; set; }
    }

    public class PrioritylevelDTO
    {        
        public long id { get; set; }
        public string priority_level { get; set; }
    }
    
    public class StateDTO
    {       
        public long id { get; set; }
        public string state_name { get; set; }
    }

    public class EntityOperationResult
    {
        public long id { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }

    }
}
