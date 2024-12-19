import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ManageBusinessService } from '../../services/manage-business.service';
import { Business } from '../../model/business-model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { PostcodeService } from '../../services/postcode.service';


@Component({
  selector: 'app-manage-business',
  templateUrl: './manage-business.component.html',
  styleUrls: ['./manage-business.component.css']
})
export class ManageBusinessComponent implements OnInit {
  business: Business = new Business();
  businessArea: any = {
    radius: 5, 
    isOnline: false, 
  };
  email: string = '';
  productInput: string = '';
  
  filteredSuggestions: string[] = [];
  constructor(private manageBusinessService: ManageBusinessService,
    private authService: AuthService,
    private postcodeService: PostcodeService,
    private router: Router,
    private snackBar: MatSnackBar) { }


  ngOnInit() {
    this.authService.isLoggedIn().subscribe({
      next: (isLoggedIn) => {
        if (!isLoggedIn) {
          this.router.navigate(['/login']); // Redirect to login page if not logged in
          return;
        }

        // Load user details
        this.loadUserBusiness();
      },
      error: (err) => {
        console.error('Error checking login status', err);
        this.router.navigate(['/login']); // Redirect on error as well
      }
    });
  }

  private loadUserBusiness() {
    this.manageBusinessService.getUserBusiness().subscribe({
      next: (response) => {
        if (response.result) {
          let businessDetails = response.result[0];
          this.business.businessId = businessDetails.businessId;
          this.business.name = businessDetails.name;
          this.business.address = businessDetails.address;
          this.business.postcode = businessDetails.postcode;
          this.business.description = businessDetails.description;
          this.business.contactNumber = businessDetails.contactNumber;
          this.business.email = businessDetails.email;
          this.business.website = businessDetails.website;
          this.business.imageUrl = businessDetails.imageUrl;
          this.business.instaHandle = businessDetails.instaHandle;
          this.business.facebook = businessDetails.facebook;
          this.business.hasLogo = businessDetails.hasLogo;
          this.business.servesPostcodes = businessDetails.servesPostcodes;
          this.business.keywords = businessDetails.keywords;
          this.business.isActive = businessDetails.isActive;
        }
      },
      error: () => {
        console.error('Failed to load user profile.');
      }
    });
  }


  // Add a product to the list
  addProduct(product: string, event?: Event): void {
    if (event) {
      const keyboardEvent = event as KeyboardEvent; 
      keyboardEvent.preventDefault();
    }

    product = product.trim();
    if (!this.business.keywords?.includes(product)) {
      this.business.keywords?.push(product);
    }
    this.productInput = '';
  }

  // Remove a product from the list
  removeProduct(product: string): void {
    this.business.keywords = this.business.keywords?.filter(p => p !== product);
  }

  addBusiness(): void {
    this.manageBusinessService.addBusiness(this.business).subscribe(
      response => {
        console.log('Business added successfully:', response);

        this.business.businessId = response.result;
        this.snackBar.open('Your business is in queued, Visible within 24 hours post-verification!', 'Close', {
          duration: 3000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
        });
      },
      error => {
        console.error('Error adding business:', error);
      }
    );
  }

  updateBusiness(): void {
    this.manageBusinessService.updateBusiness(this.business).subscribe(
      response => {
        console.log('Business updated successfully:', response);

        this.business.businessId = response.result;
        this.snackBar.open('Business details saved successfully', 'Close', {
          duration: 3000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
        });
      },
      error => {
        console.error('Error adding business:', error);
      }
    );
  }

  getServesPostcodes(): void {
    if (this.business.postcode != '') {
      if (this.businessArea.radius == 100) {
        this.business.servesPostcodes = ['online'];
      }
      else {
        this.postcodeService.getNearbyPostcodes(this.business.postcode, this.businessArea.radius).subscribe((nearby) => {
          const postcodes = nearby.result.map((item: any) => item.outcode);
          this.business.servesPostcodes = [...new Set(postcodes as string)]; // Get unique outcodes
        });
      }
    }
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.business.businessId == 0 ? this.addBusiness() : this.updateBusiness();
    } else {
      console.error('Form is invalid');
    }
  }

}
