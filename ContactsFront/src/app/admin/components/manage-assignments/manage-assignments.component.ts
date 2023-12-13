import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatTableDataSource} from "@angular/material/table";
import {ProjectAssignment} from "../../../models/project-assignment";
import {AssignmentService} from "../../../services/assignment.service";
import {MatCardModule} from "@angular/material/card";

@Component({
  selector: 'app-manage-assignments',
  standalone: true,
  imports: [CommonModule, MatCardModule],
  templateUrl: './manage-assignments.component.html',
  styleUrls: ['./manage-assignments.component.css']
})
export class ManageAssignmentsComponent implements OnInit{
  employees: ProjectAssignment[] = []
  displayColumns: string[] = [];
  assignments: ProjectAssignment[] = []
  dataSource: MatTableDataSource<ProjectAssignment> = new MatTableDataSource<ProjectAssignment>();

  constructor(private assignmentService: AssignmentService) {
  }

  ngOnInit() {
    this.onLoadPage();
  }

  onLoadPage() {
    this.assignmentService.getAllAssignments().subscribe({
      complete: () => {
        this.dataSource = new MatTableDataSource<ProjectAssignment>(this.assignments);
        this.displayColumns = ['firstname', 'lastname', 'email', 'companyRole']
      },
      next: (response) => {
        this.assignments = response as ProjectAssignment[]
      },
      error: (error) => console.log(error)
    })
  }

  openCreateAssignmentModal() {

  }
}
