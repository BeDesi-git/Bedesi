import {Component, OnInit} from '@angular/core'
import { BusinessListService } from '../../services/businesslist.service'
import { Business } from 'src/app/model/business-model';

@Component({
    selector: 'business-list',
    templateUrl: './business-list.component.html'
})
export class BusinessListComponenet implements OnInit{
    businessList:any
    constructor(private businessListService: BusinessListService){

    }
    ngOnInit(){
      //this.businessList = this.businessListService.getBusinessList('');
    }

    filterBusiness(text: string) {
      this.businessList = this.businessListService.getBusinessList(text);
    }
}