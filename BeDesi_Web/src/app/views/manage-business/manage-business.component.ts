import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ManageBusinessService } from '../../services/manage-business.service';
import { Business } from '../../model/business-model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { PostcodeService } from '../../services/postcode.service';
import { postcodeValidator } from 'postcode-validator';



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
  isBusinessNameTaken = false;

  filteredSuggestions: string[] = [];
  isValidPostcode: boolean = true;
  constructor(private manageBusinessService: ManageBusinessService,
    private authService: AuthService,
    private postcodeService: PostcodeService,
    private snackBar: MatSnackBar,
    private cdr: ChangeDetectorRef,
    private router: Router) { }


  ngOnInit() {
    this.loadUserBusiness();
  }

  private loadUserBusiness() {
    if (localStorage.getItem('token')) {
      this.manageBusinessService.getUserBusiness().subscribe({
        next: (response) => {
          if (response.result[0]) {
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
            this.business.agreeToShow = businessDetails.agreeToShow;
            this.business.isOnline = businessDetails.isOnline;
          }
        },
        error: () => {
          console.error('Failed to load user profile.');
        }
      });
    }
  }

  // Add a product to the list
  addProduct(product: string, event?: Event): void {
    if (event) {
      const keyboardEvent = event as KeyboardEvent;
      keyboardEvent.preventDefault();
    }

    // Split by space or comma, trim each, and remove empty strings
    const productsArray = product
      .split(/[\s,]+/)
      .map(p => p.trim())
      .filter(p => p !== '');

    if (productsArray.length === 0) {
      return;
    }

    // Ensure uniqueness before adding to the collection
    productsArray.forEach(p => {
      if (!this.business.keywords?.includes(p)) {
        this.business.keywords?.push(p);
      }
    });

    this.productInput = '';
  }


  // Remove a product from the list
  removeProduct(product: string): void {
    this.business.keywords = this.business.keywords?.filter(p => p !== product);
  }
  //Commented user auto register code
  //addBusiness(): void {
  //  if (this.authService.isLoggedIn()) {
  //    this.sendAddBusinessRequest('');
  //  }
  //  else {
  //    //Auto register user
  //    let data = {
  //      name: this.business.name,
  //      email: this.business.email,
  //      password: '',
  //      isBusinessOwner: true,
  //      isAutoRegister: true
  //    }
  //    this.authService.register(data).subscribe(
  //      response => {
  //        this.sendAddBusinessRequest(response.result.toString());
  //      }
  //    )
  //  }
  //}

  addBusiness(): void {
    this.sendAddBusinessRequest();
  }

  sendAddBusinessRequest() {
    this.manageBusinessService.addBusiness(this.business).subscribe(
      response => {
        console.log('Business added successfully:', response);

        this.business.businessId = response.result;
       
        var msg = 'Business added successfully'
        
        this.snackBar.open(msg, 'Close', {
          duration: 3000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
        });

        this.router.navigate(['/business']);
        
      },
      error => {
        console.error('Error adding business:', error);
      }
    );
  }

  updateBusiness(): void {
    this.manageBusinessService.updateBusiness(this.business).subscribe(
      response => {
        if (response.result) {
          console.log('Business updated successfully:', response);
          this.snackBar.open('Business details saved successfully', 'Close', {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
          });
        }
      },
      error => {
        console.error('Error adding business:', error);
      }
    );
  }

  // Handle postcode selection from autocomplete
  onOutcodeSelected(outcode: string) {
    this.business.postcode = outcode;
    console.log('Selected outcode:', this.business.postcode);
    this.getServesPostcodes();
  }

  getServesPostcodes(): void {
    if (this.business.postcode != '') {
      this.postcodeService.getNearbyPostcodes(this.business.postcode, this.businessArea.radius).subscribe((nearby) => {
        const postcodes = nearby.result.map((item: any) => item.outcode);
        this.business.servesPostcodes = [...new Set(postcodes as string)]; // Get unique outcodes
      });
    }
  }

  checkBusinessName(): void {
    if (!this.business.name) {
      this.isBusinessNameTaken = false;
      return;
    }

    this.manageBusinessService.checkBusinessName(this.business.name).subscribe({
      next: (response) => {
        this.isBusinessNameTaken = response.result;
        this.cdr.detectChanges();
        console.log('Business name taken:', this.isBusinessNameTaken);
      },
      error: () => {
        console.error('Error checking business name.');
        this.isBusinessNameTaken = false;
      }
    });
  }

  moveToOnline(): void {
    if (this.business.isOnline) {
      this.business.servesPostcodes = [];
      this.business.postcode = '';
    }
  }

  onSubmit(form: NgForm): void {
    if (this.isFormValid(form) && !this.isBusinessNameTaken) {
      this.business.businessId == 0 ? this.addBusiness() : this.updateBusiness();
    } else {
      console.error('Form is invalid');
      this.snackBar.open('Error in Business details!!', 'Close', {
        duration: 3000, 
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
      });
    }
  }

  isFormValid(form: NgForm): boolean {
    if (!form?.valid) {
      return false;
    }
    return this.business.isOnline || this.business.postcode !== '';
  }
}
