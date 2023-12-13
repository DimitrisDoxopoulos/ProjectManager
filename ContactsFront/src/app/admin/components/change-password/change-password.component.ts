import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatCardModule} from "@angular/material/card";
import {MatInputModule} from "@angular/material/input";
import {RouterLink} from "@angular/router";
import {AuthService} from "../../../services/auth.service";
import {UpdatePassword} from "../../../models/update-password";
import {matchpassword} from "../../../validators/matchpassword.validator";

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [CommonModule, MatCardModule, ReactiveFormsModule, MatInputModule, RouterLink],
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  changePasswordForm: FormGroup

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.changePasswordForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*\\W).{8,}$')]],
      passwordRepeat: ['', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*\\W).{8,}$')]],
    }, {validators: matchpassword})
  }

  changePassword() {
    let user = this.authService.session;
    console.log('User', user)
    const passwordData: UpdatePassword = {
      id: user.id,
      newPassword: this.changePasswordForm.controls['newPassword'].value,
      newPasswordConfirm: this.changePasswordForm.controls['passwordRepeat'].value
    }

    this.authService.changePassword(passwordData).subscribe({
      complete: () => {},
      next: () => {},
      error: (error) => console.log(error)
    })
  }
}
