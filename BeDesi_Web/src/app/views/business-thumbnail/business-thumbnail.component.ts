import {Component, Input} from '@angular/core'

@Component({
    selector: 'business-thumbnail',
    templateUrl:'./business-thumbnail.component.html',
    styles: [`
        .business-image { height: 100px; }
        `]
})
export class BusinessThumbnailComponent{
    @Input() localbusiness:any
}