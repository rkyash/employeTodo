using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTODoTask.Entites;
using EmployeeTODoTask.Model.Context;
using EmployeeTODoTask.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeTODoTask.Controllers
{    
    [Route("api/employee")]
    public class EmployeeToDoController : ControllerBase
    {
        private readonly EmployeeTodoRepository todoRepository;
        public EmployeeToDoController(MyDbContext dbContext)
        {
            todoRepository = new EmployeeTodoRepository(dbContext);
        }

        [HttpGet("priorityOrState")]
        public IActionResult GetPriorityOrStates()
        {
            PriorityOrStateDTO priorityOrState = new PriorityOrStateDTO();
            priorityOrState= todoRepository.getPriorityOrStateList();

            if (priorityOrState!=null)
            {
                return Ok(priorityOrState);
            }
            else
            {
                return NotFound();
            }

           
        }




        [HttpGet("getAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            List<EmployeeDTO> empList = new List<EmployeeDTO>();
            empList = todoRepository.getAllEmployee();

            if (empList != null)
            {
                return Ok(empList);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost("addEmployee")]
        public IActionResult AddEmployee([FromBody]EmployeeDTO employee)
        {
            EntityOperationResult ret = new EntityOperationResult();
            if (employee==null)
            {
                ret.isSuccess = false;
                ret.message = "Json object null Plz try again";
            }
            ret = todoRepository.saveEmployeeRecord(employee);

            if (ret.isSuccess)
            {
                return Ok(ret);
            }
            else
            {
                return Ok(ret);
            }
        }



        [HttpPut("updateEmployee")]
        public IActionResult updateEmployee([FromBody]EmployeeDTO employee)
        {
            EntityOperationResult ret = new EntityOperationResult();
            if (employee == null)
            {
                ret.isSuccess = false;
                ret.message = "Json object null Plz try again";
            }
            ret = todoRepository.updateEmployeeRecord(employee);

            if (ret.isSuccess)
            {
                return Ok(ret);
            }
            else
            {
                return Ok(ret);
            }
        }


        [HttpDelete("deleteEmployee/{empId}")]
        public IActionResult deleteEmployee(long empId)
        {
            EntityOperationResult ret = new EntityOperationResult();
            if (empId== 0)
            {
                ret.isSuccess = false;
                ret.message = "somthig went worng";
            }
            ret = todoRepository.deleteEmployeeRecord(empId);

            if (ret.isSuccess)
            {
                return Ok(ret);
            }
            else
            {
                return Ok(ret);
            }
        }




        [HttpGet("getEmpTaskByEmpId/{empid}")]
        public IActionResult GetEmpTaskByEmpId(long empid)
        {
            List<EmployeeTaskDTO> taskList = new List<EmployeeTaskDTO>();
            taskList = todoRepository.getTaskByEmpId(empid);

            if (taskList != null)
            {
                return Ok(taskList);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("addEmployeeTask")]
        public IActionResult addEmployeeTask([FromBody]EmployeeTaskDTO empTask)
        {
            EntityOperationResult ret = new EntityOperationResult();
            if (empTask == null || empTask.emp_id<=0)
            {
                ret.isSuccess = false;
                ret.message = "Json object null Plz try again";
                return Ok(ret);
            }
            ret = todoRepository.addTask(empTask);

            if (ret.isSuccess)
            {
                return Ok(ret);
            }
            else
            {
                return Ok(ret);
            }
        }

        [HttpPut("updateEmployeeTask")]
        public IActionResult updateEmployeeTask([FromBody]EmployeeTaskDTO empTask)
        {
            EntityOperationResult ret = new EntityOperationResult();
            if (empTask == null || empTask.emp_id <= 0)
            {
                ret.isSuccess = false;
                ret.message = "Json object null Plz try again";
                return Ok(ret);
            }
            ret = todoRepository.updaeTask(empTask);

            if (ret.isSuccess)
            {
                return Ok(ret);
            }
            else
            {
                return Ok(ret);
            }
        }


        [HttpDelete("deleteEmployeeTask/{taskid}")]
        public IActionResult deleteEmployeeTask(long taskid)
        {
            EntityOperationResult ret = new EntityOperationResult();
            if ( taskid <= 0)
            {
                ret.isSuccess = false;
                ret.message = "Json object null Plz try again";
            }
            ret = todoRepository.daleteTask(taskid);

            if (ret.isSuccess)
            {
                return Ok(ret);
            }
            else
            {
                return Ok(ret);
            }
        }

    }
}
