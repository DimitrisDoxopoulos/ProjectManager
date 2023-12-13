import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatCardModule} from "@angular/material/card";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {RouterLink} from "@angular/router";
import {AuthService} from "../../../services/auth.service";

@Component({
  selector: 'app-manage-profile',
  standalone: true,
  imports: [CommonModule, MatCardModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, RouterLink],
  templateUrl: './manage-profile.component.html',
  styleUrls: ['./manage-profile.component.css']
})
export class ManageProfileComponent implements OnInit {
  updateProfileForm: FormGroup
  constructor(private fb: FormBuilder, private authService: AuthService) {
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
    const user = this.authService.session
    this.updateProfileForm.controls['username'].setValue(user.username)
    this.updateProfileForm.controls['email'].setValue(user.email)
    this.updateProfileForm.controls['firstname'].setValue(user.firstname)
    this.updateProfileForm.controls['lastname'].setValue(user.lastname)
  }

  updateUser() {

  }
}
