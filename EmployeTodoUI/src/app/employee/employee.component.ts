import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from '../../environments/environment'


import { Employee, EmployeeTask, PriorityOrState } from '../models/Employee'
import { from } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss'],
  encapsulation: ViewEncapsulation.None,

})
export class EmployeeComponent implements OnInit {

  employee: Employee;
  emplist = new Array<Employee>();
  employeeTask: EmployeeTask;
  empTasklist = new Array<EmployeeTask>();
  priorityOrState: PriorityOrState;
  public apiUrl: string;

  empId: any;
  empTaskModelTitle: string;
  empModelTitle: string;
  constructor(private http: HttpClient, private modalService: NgbModal) {
    this.apiUrl = environment.apiUrl;
  }

  ngOnInit(): void {
    this.employee = new Employee();
    this.employeeTask = new EmployeeTask();
    this.priorityOrState = new PriorityOrState();
    this.getPriortyOrSate();
    this.getAllEmployee();
  }


  async getPriortyOrSate() {
    this.http.get<any>(this.apiUrl + "employee/priorityOrState", {}).subscribe(res => {
      this.priorityOrState = res;

    }, error => {
      console.log(error);
    })
  }

  async getAllEmployee() {
    this.http.get<any>(this.apiUrl + "employee/getAllEmployee", {}).subscribe(res => {
      this.emplist = res;

      if (this.emplist.length > 0) {
        this.getEmpTask(this.emplist[0]);
      }

    }, error => {
      console.log(error);
    })
  }

  addEmployeeModel(content, item) {
    this.employee = new Employee();
    this.empModelTitle = "";
    if (item != null) {
      this.empModelTitle = "Update Employee";
      this.employee = Object.assign({}, item);
    } else {
      this.empModelTitle = "Add Employee";
    }
    this.modalService.open(content, { size: 'lg' });
  }


  empAction() {
    if (this.employee.id > 0) {
      this.updateEmployee();

    } else {
      this.addEmployee();
    }
  }

  addEmployee() {
    if (this.employee.emp_name == "" || this.employee.emp_name == null) {
      alert("Plz Enter Employee Name");
      return;
    }
    this.http.post<any>(this.apiUrl + "employee/addEmployee", this.employee).subscribe(res => {
      if (res.isSuccess) {
        this.getAllEmployee();
        alert(res.message);
        this.modalService.dismissAll();
      } else {
        alert(res.message);
        this.modalService.dismissAll();
      }
    }, error => {
      console.log(error);
      this.modalService.dismissAll();
    })

  }

  updateEmployee() {

    this.http.put<any>(this.apiUrl + "employee/updateEmployee", this.employee).subscribe(res => {
      if (res.isSuccess) {
        this.getAllEmployee();
        alert(res.message);
        this.modalService.dismissAll();
      } else {
        alert(res.message);
        this.modalService.dismissAll();
      }
    }, error => {
      console.log(error);
      this.modalService.dismissAll();
    })

  }

  deleteEmployee(item) {
    this.http.delete<any>(this.apiUrl + "employee/deleteEmployee/" + item.id).subscribe(res => {
      if (res.isSuccess) {
        this.getAllEmployee();
        this.getAllEmployee();
        alert(res.message);
      } else {
        alert(res.message);
      }
    }, error => {
      console.log(error);
    })

  }


  getEmpTask(empdat) {
    debugger
    this.empId = empdat.id;

    this.http.get<any>(this.apiUrl + "employee/getEmpTaskByEmpId/" + this.empId).subscribe(res => {
      this.empTasklist = res;
    }, error => {
      console.log(error);
    });
  }


  openEmpTaskModel(modelContent, item) {
    this.employeeTask = new EmployeeTask();
    if (item != null) {
      this.empTaskModelTitle = "Update Employee Task";
      this.employeeTask = Object.assign({}, item);
    } else {
      this.empTaskModelTitle = "Add Employee Task";
      this.employeeTask.emp_id = this.empId;
    }

    this.modalService.open(modelContent, { size: 'lg' });
  }

  empTaskAction() {
    if (this.employeeTask.id > 0) {
      this.updateEmpTask();

    } else {
      this.addEmpTask();
    }
  }

  addEmpTask() {
    debugger
    if (this.employeeTask.emp_id <= 0) {
      alert("Plz add Employee");
      this.modalService.dismissAll();
      return;
    }

    if (this.employeeTask.emp_id == undefined) {
      alert("Plz add Employee");
      this.modalService.dismissAll();
      return;

    }

    if (this.employeeTask.state_id <= 0 || this.employeeTask.prioritylevel_id <= 0 || this.employeeTask.title == "" || this.employeeTask.title == null || this.employeeTask.state_id == undefined || this.employeeTask.prioritylevel_id == undefined || this.employeeTask.title == undefined || this.employeeTask.title == null) {
      alert("Plz fill all fields");
      //this.modalService.dismissAll();
      return;

    }

    this.http.post<any>(this.apiUrl + "employee/addEmployeeTask", this.employeeTask).subscribe(res => {
      if (res.isSuccess) {
        this.getEmpTask(res);
        alert(res.message);
        this.modalService.dismissAll();
      } else {
        alert(res.message);
        this.modalService.dismissAll();
      }
    }, error => {
      console.log(error);
      this.modalService.dismissAll();
    })
  }

  updateEmpTask() {
    this.http.put<any>(this.apiUrl + "employee/updateEmployeeTask", this.employeeTask).subscribe(res => {
      if (res.isSuccess) {
        this.getEmpTask(res);
        alert(res.message);
        this.modalService.dismissAll();
      } else {
        alert(res.message);
        this.modalService.dismissAll();
      }
    }, error => {
      console.log(error);
      this.modalService.dismissAll();
    })
  }


  deleteEmployeeTask(item) {
    this.http.delete<any>(this.apiUrl + "employee/deleteEmployeeTask/" + item.id).subscribe(res => {
      if (res.isSuccess) {
        this.getAllEmployee();
        alert(res.message);
      } else {
        alert(res.message);
      }
    }, error => {
      console.log(error);
    })
  }


}
