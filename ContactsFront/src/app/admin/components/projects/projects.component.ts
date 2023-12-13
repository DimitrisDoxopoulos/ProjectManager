import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {Project} from "../../../models/project";
import {ProjectService} from "../../../services/project.service";
import {MatCardModule} from "@angular/material/card";

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

  constructor(private projectService: ProjectService) {

  }

  ngOnInit() {
    this.onLoadPage()
  }

  onLoadPage() {
    this.projectService.getAllProjects().subscribe({
      complete: () => {
        console.log(this.projects)
        this.dataSource = new MatTableDataSource<Project>(this.projects);
        this.displayColumns = ['title', 'description', 'deadline']
      },
      next: (res) => {
        this.projects = res as Project[]
      },
      error: (err) => console.log(err)
    })
  }

  openCreateProjectModal() {

  }
}
