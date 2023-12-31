import {Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {RouterLink} from "@angular/router";
import {EmployeeInsert} from "../../../../models/employeeInsert";
import {AuthService} from "../../../../services/auth.service";
import {EmployeeService} from "../../../../services/employee.service";
import {MessagesService} from "../../../../services/messages.service";

@Component({
  selector: 'app-create-employee',
  standalone: true,
  imports: [CommonModule, MatDialogModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, RouterLink],
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent {
  createEmployee: FormGroup;

  constructor(
    private fb: FormBuilder, private authService: AuthService, private employeeService: EmployeeService,
    private messagesService: MessagesService
  ) {
    this.createEmployee = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', Validators.required],
      companyRole: ['', Validators.required]
    })
  }

  saveEmployee() {
    const user = this.authService.session
    const employee: EmployeeInsert = {
      userId: user.id,
      firstname: this.createEmployee.controls['firstname'].value,
      lastname: this.createEmployee.controls['lastname'].value,
      email: this.createEmployee.controls['email'].value,
      companyRole: this.createEmployee.controls['companyRole'].value
    }

    this.employeeService.insertEmployee(employee).subscribe({
      complete: () => {},
      next: () => {
        this.messagesService.showSuccessMessage("Success!", 'Employee inserted successfully!')
        this.createEmployee.reset()
        this.createEmployee.markAsUntouched()
        this.createEmployee.markAsPristine()
      },
      error: (error) => this.messagesService.showErrorMessage('Error: ' + error.status, error.message)
    })
  }
}
