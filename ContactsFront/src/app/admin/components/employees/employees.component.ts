import {Component, Inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Employee} from "../../../models/employee";
import {EmployeeService} from "../../../services/employee.service";
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {MatCardModule} from "@angular/material/card";
import {MatDialog} from "@angular/material/dialog";
import {CreateEmployeeComponent} from "./create-employee/create-employee.component";
import {EditEmployeeComponent} from "./edit-employee/edit-employee.component";

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule],
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  employees: Employee[] = []
  displayColumns: string[] = [];
  dataSource: MatTableDataSource<Employee> = new MatTableDataSource<Employee>();

  constructor(private employeeService: EmployeeService, public dialog: MatDialog) {

  }

  ngOnInit() {
    this.onLoadPage()
  }

  onLoadPage() {
    this.employeeService.getAllEmployees().subscribe({
      complete: () => {
        this.dataSource = new MatTableDataSource<Employee>(this.employees);
        this.displayColumns = ['firstname', 'lastname', 'email', 'companyRole', 'actions']
      },
      next: (res) => {
        this.employees = res as Employee[]
      },
      error: (err) => console.log(err)
    })
  }

  openCreateEmployeeModal() {
    const dialogRef = this.dialog.open(CreateEmployeeComponent, {
      width: '800px',
      autoFocus: false
    })
  }

  openEditModal(employee: Employee) {
    const dialogRef = this.dialog.open(EditEmployeeComponent, {
      data: employee,
      width: '800px',
      autoFocus: false
    })
  }

  deleteEmployee(employee: Employee) {
    if (confirm(`Delete employee ${employee.firstname} ${employee.lastname}?`)) {
      this.employeeService.deleteEmployee(employee.slug).subscribe({
        complete: () => {},
        next: () => {},
        error: (error) => console.log(error)
      })
    }
  }
}
