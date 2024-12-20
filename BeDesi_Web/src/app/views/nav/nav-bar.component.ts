import { Component } from '@angular/core'
import { AuthService } from '../../services/auth.service';


@Component({
    selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent{
  isLoggedIn = false;
  username = '';

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    // Check login status
    this.authService.loggedInStatus$.subscribe((status) => {
      this.isLoggedIn = status;
      if (this.isLoggedIn) {
        this.username = this.authService.getUsername(); // Retrieve username
      }
    });
  }

  logout(): void {
    this.authService.logout(); // Perform logout
    this.isLoggedIn = false;
    this.username = '';
  }
}
