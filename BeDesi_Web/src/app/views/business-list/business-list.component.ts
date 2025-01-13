import { Component, OnInit } from '@angular/core'
import { BusinessService } from 'src/app/services/business.service';
import { Business } from 'src/app/model/business-model';

@Component({
  selector: 'business-list',
  templateUrl: './business-list.component.html'
})

export class BusinessListComponent {
  businessList: any;
  selectedLocation: string = '';
  constructor(private businessService: BusinessService) {
  }

  // Handle postcode selection from autocomplete
  onLocationSelected(area: string) {
    this.selectedLocation = area;
    console.log('Selected postcode:', this.selectedLocation);
  }

  // Method to search and filter businesses based on 'text'
  searchBusiness(searchText: string) {
    if (searchText == '' || this.selectedLocation == '') {
      return;
    }
    this.businessService.searchBusinesses(searchText, this.selectedLocation).subscribe({
      next: (data: any) => {
        if (data && data.result) {
          let filteredBusinesses = data.result;

          filteredBusinesses.forEach((business: Business) => {
            if(business.hasLogo){
              business.imageUrl = 'assets/images/thumbnails/thumbnail_'+ business.businessId +'.jpg';
            }
            else{
              business.imageUrl = 'assets/images/thumbnails/default.png';
            }

            if (business.instaHandle) {
              business.instaHandle = 'https://www.instagram.com/' + business.instaHandle;
            }

            if (business.facebook) {
              business.facebook = 'https://www.facebook.com/' + business.facebook;
            }
          });
          this.businessList = filteredBusinesses;
        } else {
          console.log('No businesses found.');
        }
      },
      error: (e) => { console.error('Error fetching businesses:', e) }
    });
    this.businessService.closeAutocomplete();
  }
}
