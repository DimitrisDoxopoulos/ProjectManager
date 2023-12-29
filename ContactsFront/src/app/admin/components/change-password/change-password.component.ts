import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatCardModule} from "@angular/material/card";
import {MatInputModule} from "@angular/material/input";
import {RouterLink} from "@angular/router";
import {AuthService} from "../../../services/auth.service";
import {UpdatePassword} from "../../../models/update-password";
import {matchpassword} from "../../../validators/matchpassword.validator";
import {MessagesService} from "../../../services/messages.service";

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [CommonModule, MatCardModule, ReactiveFormsModule, MatInputModule, RouterLink],
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm: FormGroup

  constructor(private fb: FormBuilder, private authService: AuthService, private messagesService: MessagesService) {
    this.changePasswordForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*\\W).{8,}$')]],
      passwordRepeat: ['', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*\\W).{8,}$')]],
    }, {validators: matchpassword})
  }

  ngOnInit() {
    this.changePasswordForm.reset()
    this.changePasswordForm.markAsUntouched()
    this.changePasswordForm.markAsUntouched()
  }

  changePassword() {
    let user = this.authService.session;
    const passwordData: UpdatePassword = {
      id: user.id,
      newPassword: this.changePasswordForm.controls['newPassword'].value,
      newPasswordConfirm: this.changePasswordForm.controls['passwordRepeat'].value
    }

    this.authService.changePassword(passwordData).subscribe({
      complete: () => {
        this.changePasswordForm.reset()
        this.changePasswordForm.markAsUntouched()
        this.changePasswordForm.markAsUntouched()
      },
      next: () => {

        this.messagesService.showSuccessMessage('Success!', 'Your password was updated')
      },
      error: (error) => this.messagesService.showErrorMessage('Error: ' + error.status, error.message)
    })
  }
}
