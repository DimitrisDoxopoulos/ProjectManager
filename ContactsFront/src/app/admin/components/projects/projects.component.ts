import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {Project} from "../../../models/project";
import {ProjectService} from "../../../services/project.service";
import {MatCardModule} from "@angular/material/card";
import {InsertProjectComponent} from "./insert-project/insert-project.component";
import {MatDialog} from "@angular/material/dialog";
import {UpdateProjectComponent} from "./update-project/update-project.component";

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule],
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  employees: Project[] = []
  displayColumns: string[] = [];
  projects: Project[] = [];
  dataSource: MatTableDataSource<Project> = new MatTableDataSource<Project>();

  constructor(private projectService: ProjectService, public dialog: MatDialog) {

  }

  ngOnInit() {
    this.onLoadPage()
  }

  onLoadPage() {
    this.projectService.getAllProjects().subscribe({
      complete: () => {
        console.log(this.projects)
        this.dataSource = new MatTableDataSource<Project>(this.projects);
        this.displayColumns = ['title', 'description', 'deadline', 'actions']
      },
      next: (res) => {
        this.projects = res as Project[]
      },
      error: (err) => console.log(err)
    })
  }

  openCreateProjectModal() {
    const dialogRef = this.dialog.open(InsertProjectComponent, {
      width: '800px',
      autoFocus: false
    })
  }

  openEditModal(project: Project) {
    const dialogRef = this.dialog.open(UpdateProjectComponent, {
      data: project,
      width: '800px',
      autoFocus: false
    })
  }

  deleteProject(project: Project) {
    if (confirm(`Delete project ${project.title}?`)) {
      this.projectService.deleteProject(project.slug).subscribe({
        complete: () => {},
        next: () => {},
        error: (error) => console.log(error)
      })
    }
  }
}
