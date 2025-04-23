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
import { FooterBarComponent } from './views/footer/footer-bar.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { AreaAutocomplete } from './views/area-autocomplete/area-autocomplete.component';
import { OutcodeAutocomplete } from './views/outcode-autocomplete/outcode-autocomplete.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { ForgotPasswordComponent } from './views/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './views/reset-password/reset-password.component';
import { UpdateProfileComponent } from './views/update-profile/update-profile.component';
import { ManageBusinessComponent } from './views/manage-business/manage-business.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select'; 
import { MatOptionModule } from '@angular/material/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatGridListModule } from '@angular/material/grid-list';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    BusinessListComponent,
    BusinessThumbnailComponent,
    BusinessDetailsComponent,
    NavBarComponent,
    FooterBarComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    UpdateProfileComponent,
    ManageBusinessComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AreaAutocomplete,
    OutcodeAutocomplete,
    BrowserAnimationsModule, // Required for Material animations
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    MatSnackBarModule,
    MatSelectModule,
    MatOptionModule,
    MatAutocompleteModule,
    MatCheckboxModule,
    MatGridListModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
