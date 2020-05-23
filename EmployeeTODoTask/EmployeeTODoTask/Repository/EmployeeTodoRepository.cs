
using EmployeeTODoTask.Entites;
using EmployeeTODoTask.Model;
using EmployeeTODoTask.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTODoTask.Repository
{
    public class EmployeeTodoRepository
    {
        private readonly MyDbContext dbContext;
        public EmployeeTodoRepository() { }
        public EmployeeTodoRepository(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PriorityOrStateDTO getPriorityOrStateList()
        {
            PriorityOrStateDTO priorityOrState = new PriorityOrStateDTO();

            try
            {
                var priortDb = dbContext.prioritylevels.Select(x => new PrioritylevelDTO {
                    id = x.id,
                    priority_level = x.priority_level
                }).ToList<PrioritylevelDTO>();               

                var stateDb = dbContext.states.Select(x => new StateDTO
                {
                    id = x.id,
                    state_name = x.state_name
                }).ToList<StateDTO>();

                priorityOrState.prioritylevels = priortDb;
                priorityOrState.states = stateDb;
            }
            catch (Exception ex)
            {

            }

            return priorityOrState;
        }


        public List<EmployeeDTO> getAllEmployee()
        {
            List<EmployeeDTO> emp = new List<EmployeeDTO>();
            try
            {
                var dbObj = dbContext.employees.Select(x => new EmployeeDTO
                {
                    id = x.id,
                    emp_name = x.emp_name
                }).OrderBy(o => o.emp_name).ToList<EmployeeDTO>();
                if (dbObj.Count>0)
                {
                    emp = dbObj;
                }
               
            }
            catch (Exception ex)
            {

            }
            

            return emp;
        }


        public EntityOperationResult saveEmployeeRecord(EmployeeDTO emp)
        {
            EntityOperationResult ret = new EntityOperationResult();

            try
            {
                var dbObj = new Employee();
                dbObj.emp_name = emp.emp_name;

                dbContext.employees.Add(dbObj);
                dbContext.SaveChanges();

                ret.id = dbObj.id;
                ret.isSuccess = true;
                ret.message = "Successfully Save";
            }
            catch (Exception ex)
            {
                ret.id = 0;
                ret.isSuccess = false;
                ret.message = ex.Message;
            }

            return ret;
        }


        public EntityOperationResult updateEmployeeRecord(EmployeeDTO emp)
        {
            EntityOperationResult ret = new EntityOperationResult();

            try
            {
                var dbObj = dbContext.employees.FirstOrDefault(x=>x.id==emp.id);
                if (dbObj!=null)
                {
                    dbObj.emp_name = emp.emp_name;

                    dbContext.SaveChanges();
                    ret.id = dbObj.id;
                    ret.isSuccess = true;
                    ret.message = "Successfully update";
                }
                else
                {
                    ret.id = emp.id;
                    ret.isSuccess = false;
                    ret.message = "Record not found";
                }                
                
            }
            catch (Exception ex)
            {
                ret.id = 0;
                ret.isSuccess = false;
                ret.message = ex.Message;
            }

            return ret;
        }


        public EntityOperationResult deleteEmployeeRecord(long empId)
        {
            EntityOperationResult ret = new EntityOperationResult();

            using(var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var dbEmpObj = dbContext.employees.FirstOrDefault(x => x.id == empId);

                    if (dbEmpObj!=null)
                    {
                        

                        var dbTaskObj = dbContext.employeeTasks.FirstOrDefault(x=>x.emp_id==dbEmpObj.id);

                        if (dbTaskObj!=null)
                        {
                            dbContext.Remove(dbEmpObj);
                        }
                        dbContext.Remove(dbEmpObj);
                        dbContext.SaveChanges();
                        dbContextTransaction.Commit();                        
                        ret.isSuccess = true;
                        ret.message = "Successfully delete";
                    }
                    else
                    {
                        ret.isSuccess = true;
                        ret.message = "Something went worng!!";
                    }
                }
                catch(Exception ex)
                {
                    dbContextTransaction.Rollback();                    
                    ret.isSuccess = false;
                    ret.message = ex.Message;
                }
            }
           

            return ret;
        }


        public List<EmployeeTaskDTO> getTaskByEmpId(long empId)
        {
            List<EmployeeTaskDTO> empTask = new List<EmployeeTaskDTO>();

            try
            {
                empTask = dbContext.employeeTasks.Where(x => x.emp_id == empId).
                    Join(dbContext.prioritylevels, empTask => empTask.prioritylevel_id, prorty => prorty.id, (empTask, prorty) => new
                    {
                        empTask,prorty
                    }).Join(dbContext.states,empPrrty=> empPrrty.empTask.state_id,st=>st.id,(empPrrty,st)=>new EmployeeTaskDTO { 
                        id=empPrrty.empTask.id,
                        title= empPrrty.empTask.title,
                        description = empPrrty.empTask.description,
                        emp_id= empPrrty.empTask.emp_id,
                        estimate= empPrrty.empTask.estimate,
                        prioritylevel= empPrrty.prorty.priority_level,
                        prioritylevel_id=empPrrty.prorty.id,
                        state_id=st.id,
                        state= st.state_name,
                    }).OrderBy(o=>o.prioritylevel_id).ToList<EmployeeTaskDTO>();
               
            }
            catch (Exception ex)
            {

            }

            return empTask;
        }

        public EntityOperationResult addTask(EmployeeTaskDTO empTask)
        {
            EntityOperationResult ret = new EntityOperationResult();

            try
            {
                var dbObj = new EmployeeTask();

                dbObj.title = empTask.title;
                dbObj.emp_id = empTask.emp_id;
                dbObj.description = empTask.description;
                dbObj.estimate = empTask.estimate;
                dbObj.prioritylevel_id = empTask.prioritylevel_id;
                dbObj.state_id = empTask.state_id;

                dbContext.employeeTasks.Add(dbObj);
                dbContext.SaveChanges();
                ret.id = dbObj.emp_id;
                ret.isSuccess = true;
                ret.message = "Successfully Save";


            }
            catch (Exception ex)
            {
                ret.isSuccess = false;
                ret.message = ex.Message;
            }

            return ret;
        }

        public EntityOperationResult updaeTask(EmployeeTaskDTO empTask)
        {
            EntityOperationResult ret = new EntityOperationResult();

            try
            {
                var dbObj = dbContext.employeeTasks.FirstOrDefault(x=>x.id==empTask.id);

                if (dbObj!=null)
                {
                    dbObj.title = empTask.title;
                    dbObj.description = empTask.description;
                    dbObj.estimate = empTask.estimate;
                    dbObj.prioritylevel_id = empTask.prioritylevel_id;
                    dbObj.state_id = empTask.state_id;
                   
                    dbContext.SaveChanges();
                    ret.id = dbObj.emp_id;
                    ret.isSuccess = true;
                    ret.message = "Successfully Save";
                }
                else
                {
                    ret.isSuccess = false;
                    ret.message = "Record not found plz try again";
                }
            }
            catch (Exception ex)
            {
                ret.isSuccess = false;
                ret.message = ex.Message;
            }

            return ret;
        }

        public EntityOperationResult daleteTask(long taskid)
        {
            EntityOperationResult ret = new EntityOperationResult();

            try
            {
                var dbObj = dbContext.employeeTasks.FirstOrDefault(x => x.id == taskid);

                if (dbObj != null)
                {

                    dbContext.Remove(dbObj);
                    dbContext.SaveChanges();                    
                    ret.isSuccess = true;
                    ret.message = "Successfully delete";
                }
                else
                {
                    ret.isSuccess = false;
                    ret.message = "Record not found plz try again";
                }
            }
            catch (Exception ex)
            {
                ret.isSuccess = false;
                ret.message = ex.Message;
            }

            return ret;
        }
    }
}
