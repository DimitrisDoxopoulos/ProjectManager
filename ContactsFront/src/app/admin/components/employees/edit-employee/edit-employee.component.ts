import {Component, EventEmitter, Inject, OnInit, Output} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {EmployeeService} from "../../../../services/employee.service";
import {EmployeeUpdate} from "../../../../models/employeeUpdate";
import {AuthService} from "../../../../services/auth.service";
import {MessagesService} from "../../../../services/messages.service";

@Component({
  selector: 'app-edit-employee',
  standalone: true,
    imports: [CommonModule, FormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {
  updateEmployeeForm: FormGroup
  @Output() isUpdated: EventEmitter<boolean> = new EventEmitter();

  constructor(
    private fb: FormBuilder, private employeeService: EmployeeService,
    @Inject(MAT_DIALOG_DATA) public data: EmployeeUpdate,
    private authService: AuthService, private messagesService: MessagesService
  ) {
    this.updateEmployeeForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', Validators.required],
      companyRole: ['', Validators.required]
    })
  }

  ngOnInit() {
    this.onPageLoad()
  }

  onPageLoad() {
    this.updateEmployeeForm.controls['firstname'].setValue(this.data.firstname)
    this.updateEmployeeForm.controls['lastname'].setValue(this.data.lastname)
    this.updateEmployeeForm.controls['email'].setValue(this.data.email)
    this.updateEmployeeForm.controls['companyRole'].setValue(this.data.companyRole)
  }

  saveEmployee() {
    const user = this.authService.session
    const updatedEmployee: EmployeeUpdate = {
      id: this.data.id,
      userId: user.id,
      firstname: this.updateEmployeeForm.controls['firstname'].value,
      lastname: this.updateEmployeeForm.controls['lastname'].value,
      email: this.updateEmployeeForm.controls['email'].value,
      companyRole: this.updateEmployeeForm.controls['companyRole'].value,
    }
    this.employeeService.updateEmployee(updatedEmployee).subscribe({
      complete: () => this.isUpdated.emit(true),
      next: () => {},
      error: (error) => this.messagesService.showErrorMessage('Error: ' + error.status, error.message)
    })
  }
}
