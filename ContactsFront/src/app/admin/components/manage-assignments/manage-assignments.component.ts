import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ProjectAssignment} from "../../../models/project-assignment";
import {AssignmentService} from "../../../services/assignment.service";
import {MatCardModule} from "@angular/material/card";
import {InsertAssignmentComponent} from "./insert-assignment/insert-assignment.component";
import {MatDialog} from "@angular/material/dialog";
import {AuthService} from "../../../services/auth.service";
import {Employee} from "../../../models/employee";
import {Project} from "../../../models/project";
import {ProjectService} from "../../../services/project.service";
import {EmployeeService} from "../../../services/employee.service";
import {ProjectObjectToArrayPipe} from "../../../pipes/project-object-to-array.pipe";

@Component({
  selector: 'app-manage-assignments',
  standalone: true,
  imports: [CommonModule, MatCardModule, ProjectObjectToArrayPipe],
  templateUrl: './manage-assignments.component.html',
  styleUrls: ['./manage-assignments.component.css']
})
export class ManageAssignmentsComponent implements OnInit{
  projects: Project[] = []
  employees: Employee[] = []
  assignments: ProjectAssignment[] = []
  user = this.authService.session;


  constructor(
    private assignmentService: AssignmentService, public dialog: MatDialog, private authService: AuthService,
    private projectService: ProjectService, private employeeService: EmployeeService
  ) {
  }

  ngOnInit() {
      this.onLoadPage()
  }

  onLoadPage() {
    this.projectService.getAllProjectsOfUser(this.user.id).subscribe({
      next: (res) => {
        this.projects = res
      },
      complete: () => console.log('complete', this.projects),
      error: (err) => console.log(err)
    })
  }

  openCreateAssignmentModal() {
    const dialogRef = this.dialog.open(InsertAssignmentComponent, {
      width: '800px',
      autoFocus: false
    })
  }
}
