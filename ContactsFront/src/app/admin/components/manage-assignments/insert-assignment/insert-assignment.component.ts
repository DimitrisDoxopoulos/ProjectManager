import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {AssignmentService} from "../../../../services/assignment.service";
import {ProjectAssignment} from "../../../../models/project-assignment";
import {AuthService} from "../../../../services/auth.service";
import {MatSelectModule} from "@angular/material/select";
import {Project} from "../../../../models/project";
import {Employee} from "../../../../models/employee";
import {EmployeeService} from "../../../../services/employee.service";
import {ProjectService} from "../../../../services/project.service";
import {forkJoin} from "rxjs";
import {MessagesService} from "../../../../services/messages.service";

@Component({
  selector: 'app-insert-assignment',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatSelectModule],
  templateUrl: './insert-assignment.component.html',
  styleUrls: ['./insert-assignment.component.css']
})
export class InsertAssignmentComponent implements OnInit {
  createAssignmentForm: FormGroup
  projects: Project[] = []
  employees: Employee[] = []
  user = this.authService.session;

  constructor(
    private fb: FormBuilder, private assignmentService: AssignmentService,
    private authService: AuthService, private employeeService: EmployeeService,
    private projectService: ProjectService, private messagesService: MessagesService
  ) {
    this.createAssignmentForm = this.fb.group({
      employeeId: ['', Validators.required],
      projectId: ['', Validators.required]
    })
  }

  ngOnInit() {
    const employees$ = this.employeeService.getAllEmployeesOfUser(this.user.id)
    const projects$ = this.projectService.getAllProjectsOfUser(this.user.id)
    forkJoin([employees$, projects$]).subscribe(
      results => {
        this.employees = results[0]
        this.projects = results[1]
      }
    )
  }

  createAssignment() {
    let data: ProjectAssignment = {
      employeeId: this.createAssignmentForm.controls['employeeId'].value,
      projectId: this.createAssignmentForm.controls['projectId'].value,
      userId: this.user.id
    }
    this.assignmentService.assignProject(data).subscribe({
      complete: () => {},
      next: () => {
        this.messagesService.showSuccessMessage('Success!', 'Project assigned successfully')
        this.createAssignmentForm.reset()
        this.createAssignmentForm.markAsUntouched()
        this.createAssignmentForm.markAsPristine()
      },
      error: (error) => this.messagesService.showErrorMessage('Error: ' + error.status, error.message)
    })
  }

}
