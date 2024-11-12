import {Component, OnInit} from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { BusinessService } from 'src/app/services/business.service'


@Component({
    templateUrl: 'business-details.component.html',
    styles: [`
        .container {padding-left:20px; padding-right: 20px; }
        .business-image { height: 100px; }
        `]
})
export class BusinessDetailsComponent implements OnInit{
    business :any
    constructor(private businessService: BusinessService,
        private route:ActivatedRoute
    ){
    }
    ngOnInit(): void {
        this.business = this.businessService.getBusinessDetails(+this.route.snapshot.params['id'])
    }
}