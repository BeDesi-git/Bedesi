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
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { BusinessService } from './services/business.service';
import { LocationService } from './services/location.service';
import { AreaAutocomplete } from './views/area-autocomplete/area-autocomplete.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { ForgotPasswordComponent } from './views/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './views/reset-password/reset-password.component';
import { UpdateProfileComponent } from './views/update-profile/update-profile.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    BusinessListComponent,
    BusinessThumbnailComponent,
    BusinessDetailsComponent,
    NavBarComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    UpdateProfileComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AreaAutocomplete,
    BrowserAnimationsModule, // Required for Material animations
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    MatSnackBarModule
  ],
  providers: [BusinessService, LocationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
