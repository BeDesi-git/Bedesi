import { Component } from '@angular/core'
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent{
  isLoggedIn = false;
  username = '';
  isBusinessOwner = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    // Check login status
    this.authService.loggedInStatus$.subscribe((status) => {
      this.isLoggedIn = status;
      if (this.isLoggedIn) {
        this.username = this.authService.getUsername();
        this.isBusinessOwner = this.authService.isUserBusinessOwner();
      }
    });
  }

  logout(): void {
    this.authService.logout(); 
    this.isLoggedIn = false;
    this.username = '';
    this.router.navigate(['/login']);
  }
}
