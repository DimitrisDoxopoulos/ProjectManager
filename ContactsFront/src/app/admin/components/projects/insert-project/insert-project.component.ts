import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {ProjectService} from "../../../../services/project.service";
import {ProjectInsert} from "../../../../models/projectInsert";
import {MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {AuthService} from "../../../../services/auth.service";
import {MessagesService} from "../../../../services/messages.service";

@Component({
  selector: 'app-insert-project',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatDatepickerModule],
  templateUrl: './insert-project.component.html',
  styleUrls: ['./insert-project.component.css']
})
export class InsertProjectComponent {
  insertProjectForm: FormGroup

  constructor(
    private fb: FormBuilder, private projectService: ProjectService, private authService: AuthService,
    private messagesService: MessagesService
  ) {
    this.insertProjectForm = this.fb.group({
      title: ['', [Validators.required]],
      description: ['', [Validators.required]],
      deadline: ['', [Validators.required]]
    })
  }

  saveProject() {
    const user = this.authService.session;
    const project: ProjectInsert = {
      userId: user.id,
      title: this.insertProjectForm.controls['title'].value,
      description: this.insertProjectForm.controls['description'].value,
      deadline: this.insertProjectForm.controls['deadline'].value
    }

    this.projectService.insertProject(project).subscribe({
      complete: () => {},
      next: () => {
        this.messagesService.showSuccessMessage("Success!", 'Project inserted successfully!')
        this.insertProjectForm.reset()
        this.insertProjectForm.markAsUntouched()
        this.insertProjectForm.markAsPristine()
      },
      error: (error) => console.log(error)
    })
  }
}
