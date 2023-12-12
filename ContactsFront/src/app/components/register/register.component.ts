import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatCardModule} from "@angular/material/card";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {AuthService} from "../../services/auth.service";
import {UserRegister} from "../../models/user-register";
import {UserLogin} from "../../models/user-login";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registrationForm: FormGroup

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) {
    this.registrationForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*\\W).{8,}$')]],
      firstname: ['', [Validators.required, Validators.minLength(4)]],
      lastname: ['', [Validators.required, Validators.minLength(4)]]
    })
  }

  registerUser() {
    const userRegister: UserRegister = {
      username: this.registrationForm.controls['username'].value,
      firstname: this.registrationForm.controls['firstname'].value,
      lastname: this.registrationForm.controls['lastname'].value,
      email: this.registrationForm.controls['email'].value,
      password: this.registrationForm.controls['password'].value,
    }

    this.authService.registerUser(userRegister).subscribe({
      complete: () => this.router.navigateByUrl('/login'),
      next: (res) => {
        const loggedInUser: UserLogin = {
          username: this.registrationForm.controls['username'].value,
          password: this.registrationForm.controls['password'].value,
          keepLoggedIn: false
        }
        localStorage.setItem('loggedInUser', JSON.stringify(loggedInUser))
      },
      error: (err) => console.log(err)
    })
  }
}
