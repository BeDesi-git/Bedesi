import { Component, OnInit } from '@angular/core';
import { UserProfileService } from '../../services/user-profile.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css']
})
export class UpdateProfileComponent implements OnInit {
  name: string = '';
  email: string = '';
  contactNumber: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  isBusinessOwner: boolean = false;

  constructor(private userProfileService: UserProfileService,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.authService.isLoggedInStream().subscribe({
      next: (isLoggedIn) => {
        if (!isLoggedIn) {
          this.router.navigate(['/login']); // Redirect to login page if not logged in
          return;
        }
        
        // Load user details
        this.loadUserProfile();
      },
      error: (err) => {
        console.error('Error checking login status', err);
        this.router.navigate(['/login']); // Redirect on error as well
      }
    });
  }

  private loadUserProfile() {
    this.userProfileService.getUserProfile().subscribe({
      next: (response) => {
        if (response.result) {
          this.name = response.result.name;
          this.email = response.result.email;
          this.contactNumber = response.result.contactNumber || '';
          this.isBusinessOwner = response.result.role == "BusinessOwner";
        }
      },
      error: () => {
        console.error('Failed to load user profile.');
      }
    });
  }

  onSubmit() {
    if (this.newPassword !== this.confirmPassword) {
      this.snackBar.open('Passwords do not match!', 'Close', {
        duration: 3000,
      });
      return;
    }

    const updatedProfile = {
      email: this.email,
      name: this.name,
      contactNumber: this.contactNumber,
      password: this.newPassword ? this.newPassword : "",
      isBusinessOwner: this.isBusinessOwner
    };

    this.userProfileService.updateUserProfile(updatedProfile).subscribe({
      next: () => {
        this.snackBar.open('Profile updated successfully!', 'Close', {
          duration: 3000, 
          horizontalPosition: 'center', 
          verticalPosition: 'bottom',  
        });
        this.router.navigate(['/business']);
      },
      error: () => {
        this.snackBar.open('Failed to update profile.', 'Close', {
          duration: 3000,
          horizontalPosition: 'center', 
          verticalPosition: 'bottom',  
        });
      }
    });
  }
}
