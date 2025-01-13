import { Component } from '@angular/core';
import { Router } from '@angular/router'; // Import Router for navigation
import { AuthService } from '../../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  name = '';
  email = '';
  password = '';
  confirmPassword = '';
  isBusinessOwner: boolean = false;

  constructor(private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  onSubmit() {
    if (this.password !== this.confirmPassword) {
      this.snackBar.open('Password do not match', 'Close', {
        duration: 3000,
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
      });
      return;
    }

    let data = {
      name: this.name,
      email: this.email,
      password: this.password,
      isBusinessOwner: this.isBusinessOwner,
      isAutoRegister: false
    }
    this.authService
      .register(data)
      .subscribe({
        next: (res) => {
          if (res.hasError) {
            this.snackBar.open(res.errorMessage, 'Close', {
              duration: 3000,
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
            });
          }
          else { 
            console.log('User: ' + this.name + ' registered!', res);
            this.router.navigate(['/business']);
          }
        },
        error: (err) => {
          console.error(err);
          
        },
      });
  }

  // Navigate to the login page
  navigateToLogin() {
    this.router.navigate(['/login']);
  }
}
