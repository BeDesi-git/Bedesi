import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { BusinessListComponent } from './views/business-list/business-list.component';
import { BusinessThumbnailComponent } from './views/business-thumbnail/business-thumbnail.component';
import { BusinessDetailsComponent } from './views/business-details/business-details.component';
import { appRoutes } from './route';
import { NavBarComponent } from './views/nav/nav-bar.component';
import { HttpClientModule } from '@angular/common/http';
import { BusinessService } from './services/business.service';
import { LocationService } from './services/location.service'; 
import { AreaAutocomplete } from './views/area-autocomplete/area-autocomplete.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    BusinessListComponent,
    BusinessThumbnailComponent,
    BusinessDetailsComponent,
    NavBarComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    ReactiveFormsModule,
    AreaAutocomplete,
    BrowserAnimationsModule
  ],
  providers: [BusinessService, LocationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
