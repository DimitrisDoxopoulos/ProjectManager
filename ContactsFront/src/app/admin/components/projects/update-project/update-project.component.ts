import {Component, Inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MAT_DIALOG_DATA, MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {ProjectService} from "../../../../services/project.service";
import {AuthService} from "../../../../services/auth.service";
import {Project} from "../../../../models/project";
import {ProjectUpdate} from "../../../../models/projectUpdate";

@Component({
  selector: 'app-update-project',
  standalone: true,
    imports: [CommonModule, FormsModule, MatDatepickerModule, MatDialogModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './update-project.component.html',
  styleUrls: ['./update-project.component.css']
})
export class UpdateProjectComponent implements OnInit {
  updateProjectForm: FormGroup

  constructor(
    private fb: FormBuilder, private projectService: ProjectService,
    private authService: AuthService, @Inject(MAT_DIALOG_DATA) public data: Project) {
    this.updateProjectForm = this.fb.group({
      title: ['', [Validators.required]],
      description: ['', [Validators.required]],
      deadline: ['', [Validators.required]]
    })
  }

  ngOnInit() {
    this.onLoadPage()
  }

  onLoadPage() {
    this.updateProjectForm.controls['title'].setValue(this.data.title)
    this.updateProjectForm.controls['description'].setValue(this.data.description)
    this.updateProjectForm.controls['deadline'].setValue((this.data.deadline))
  }

  saveProject() {
    const updatedProject: ProjectUpdate = {
      id: this.data.id,
      title: this.updateProjectForm.controls['title'].value,
      description: this.updateProjectForm.controls['description'].value,
      deadline: this.updateProjectForm.controls['deadline'].value
    }
    this.projectService.updateProject(updatedProject).subscribe({
      complete: () => {},
      next: () => {},
      error: (error) => console.log(error)
    })
  }
}
