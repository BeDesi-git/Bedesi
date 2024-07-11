import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BusinessListComponenet } from './views/business-list/business-list.component';
import { BusinessThumbnailComponent } from './views/business-thumbnail/business-thumbnail.component';
import {BusinessListService} from './services/businesslist.service'
import { BusinessDetailsComponent } from './views/business-details/business-details.component';
import { appRoutes } from './route';
import { NavBarComponent } from './views/nav/nav-bar.component';


@NgModule({
  declarations: [
    AppComponent,
    BusinessListComponenet,
    BusinessThumbnailComponent,
    BusinessDetailsComponent,
    NavBarComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [BusinessListService],
  bootstrap: [AppComponent]
})
export class AppModule { }
