import { Routes } from '@angular/router'
import { BusinessDetailsComponent } from "./views/business-details/business-details.component";
import { BusinessListComponent } from "./views/business-list/business-list.component";

export const appRoutes:Routes = [
    { path: 'business', component: BusinessListComponent},
    { path: 'business/:id', component: BusinessDetailsComponent   },
    { path: '', redirectTo: 'business', pathMatch: 'full'}
]
