import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Employee} from "../../../models/employee";
import {EmployeeService} from "../../../services/employee.service";
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {MatCardModule} from "@angular/material/card";

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

  constructor(private employeeService: EmployeeService) {

  }

  ngOnInit() {
    this.onLoadPage()
  }

  onLoadPage() {
    this.employeeService.getAllEmployees().subscribe({
      complete: () => {
        this.dataSource = new MatTableDataSource<Employee>(this.employees);
        this.displayColumns = ['firstname', 'lastname', 'email', 'companyRole']
      },
      next: (res) => {
        this.employees = res as Employee[]
      },
      error: (err) => console.log(err)
    })
  }

  openCreateEmployeeModal() {

  }
}
