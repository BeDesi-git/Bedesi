// forgot-password.component.ts
import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  email: string = '';
  message: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit() {
    this.authService.forgotPassword({ email: this.email }).subscribe({
      next: (res) => {
        this.message = "A reset link has been sent to your email.";
      },
      error: (err) => {
        this.message = "Error: Could not send reset email.";
      },
    });
  }
}
