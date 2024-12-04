// reset-password.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  token: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  message: string = '';

  constructor(private route: ActivatedRoute, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    // Get the token from the URL
    this.token = this.route.snapshot.queryParamMap.get('token') || '';
  }

  onSubmit() {
    if (this.newPassword !== this.confirmPassword) {
      this.message = "Passwords do not match.";
      return;
    }

    const resetRequest = {
      token: this.token,
      newPassword: this.newPassword
    };

    this.authService.resetPassword(resetRequest).subscribe({
      next: (res) => {
        this.message = "Password reset successfully!";
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.message = "Error: Could not reset password.";
      }
    });
  }
}
