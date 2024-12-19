import { Routes } from '@angular/router';
import { BusinessDetailsComponent } from "./views/business-details/business-details.component";
import { BusinessListComponent } from "./views/business-list/business-list.component";
import { RegisterComponent } from "./views/register/register.component";
import { LoginComponent } from "./views/login/login.component";
import { ForgotPasswordComponent } from "./views/forgot-password/forgot-password.component";
import { ResetPasswordComponent } from "./views/reset-password/reset-password.component";
import { UpdateProfileComponent } from "./views/update-profile/update-profile.component";
import { ManageBusinessComponent } from "./views/manage-business/manage-business.component";

export const appRoutes: Routes = [
  { path: '', redirectTo: 'business', pathMatch: 'full' },
  { path: 'business', component: BusinessListComponent},
  { path: 'business/:id', component: BusinessDetailsComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'update-profile', component: UpdateProfileComponent },
  { path: 'manage-business', component: ManageBusinessComponent }
]
