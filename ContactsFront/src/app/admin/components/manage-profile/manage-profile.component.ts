import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatCardModule} from "@angular/material/card";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {RouterLink} from "@angular/router";
import {AuthService} from "../../../services/auth.service";
import {UserUpdate} from "../../../models/user-update";
import {MessagesService} from "../../../services/messages.service";

@Component({
  selector: 'app-manage-profile',
  standalone: true,
  imports: [CommonModule, MatCardModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, RouterLink],
  templateUrl: './manage-profile.component.html',
  styleUrls: ['./manage-profile.component.css']
})
export class ManageProfileComponent implements OnInit {
  updateProfileForm: FormGroup
  user = this.authService.session
  constructor(private fb: FormBuilder, private authService: AuthService, private messagesService: MessagesService) {
    this.updateProfileForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required]],
      firstname: ['', [Validators.required, Validators.minLength(4)]],
      lastname: ['', [Validators.required, Validators.minLength(4)]]
    })
  }
  ngOnInit() {
    this.onLoadPage()
  }

  onLoadPage() {
    this.updateProfileForm.controls['username'].setValue(this.user.username)
    this.updateProfileForm.controls['email'].setValue(this.user.email)
    this.updateProfileForm.controls['firstname'].setValue(this.user.firstname)
    this.updateProfileForm.controls['lastname'].setValue(this.user.lastname)
  }

  updateUser() {
    let updatedUser: UserUpdate = {
      id: this.user.id,
      username: this.updateProfileForm.controls['username'].value,
      firstname: this.updateProfileForm.controls['firstname'].value,
      lastname: this.updateProfileForm.controls['lastname'].value,
      email: this.updateProfileForm.controls['email'].value
    }
    this.authService.updateUser(updatedUser).subscribe({
      next: () => {},
      error: (err) => this.messagesService.showErrorMessage('Error on Update', err),
      complete: () => this.messagesService.showSuccessMessage(
        'Successfully Updated', 'Your information was successfully updated'
      )
    })
  }
}
