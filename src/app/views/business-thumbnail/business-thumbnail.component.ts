import {Component, Input} from '@angular/core'

@Component({
    selector: 'business-thumbnail',
    template:`
    <div [routerLink]="['/business', localbusiness.id]" class="well hoverwell thumbnail">
        <h2>{{localbusiness.name}}</h2>
        <div class="row">
            <div class="col-md-3">
                <img [src]="localbusiness.imageUrl" [alt]="localbusiness.name" class="business-image">
            </div>
            <div class="col-md-5">
                <div>{{localbusiness.shortDescription}}</div>
                <div>{{localbusiness.services}}</div>
                <div>{{localbusiness.website}}</div>
                <div>Review: {{localbusiness.review}} {{localbusiness.reviewCount}}</div>
                <div>
                    <span>Location: {{localbusiness.location.address}}</span>
                    <span>&nbsp;</span>
                    <span>{{localbusiness.location.city}}, {{localbusiness.location.country}}</span>
                </div>
            </div>
        </div>
    </div>
    `,
    styles: [`
        .business-image { height: 100px; }
        `]
})
export class BusinessThumbnailComponent{
    @Input() localbusiness:any
}