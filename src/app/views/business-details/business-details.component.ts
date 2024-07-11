import {Component, OnInit} from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { BusinessListService } from 'src/app/services/businesslist.service'


@Component({
    templateUrl: 'business-details.component.html',
    styles: [`
        .container {padding-left:20px; padding-right: 20px; }
        .business-image { height: 100px; }
        `]
})
export class BusinessDetailsComponent implements OnInit{
    business :any
    constructor(private businessListService: BusinessListService,
        private route:ActivatedRoute
    ){
    }
    ngOnInit(): void {
        this.business = this.businessListService.getBusiness(+this.route.snapshot.params['id'])
    }
}