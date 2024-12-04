import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  password = '';
  showPassword = false;

  constructor(private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  onSubmit() {
    if (this.email.trim() == "" || this.password.trim() == "") {
      return;
    }

    this.authService.login({ email: this.email, password: this.password }).subscribe({
      next: (res) => {
        console.log('User: ' + this.email  + ' logged in!', res);
        this.router.navigate(['/business']);
      },
      error: (err) => {
        console.error('Login failed:', err);
        this.snackBar.open('Login failed! Please check your credentials.', 'Close', {
          duration: 3000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
        });
      }
    });
  }
}
