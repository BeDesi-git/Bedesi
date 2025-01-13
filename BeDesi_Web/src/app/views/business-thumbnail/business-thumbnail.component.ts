import { Component, Input } from '@angular/core';
import { faPhone, faEnvelope, faGlobe } from '@fortawesome/free-solid-svg-icons'; 
import { faFacebook, faInstagram } from '@fortawesome/free-brands-svg-icons';

@Component({
    selector: 'business-thumbnail',
    templateUrl:'./business-thumbnail.component.html',
    styleUrls: ['./business-thumbnail.component.css']
})
export class BusinessThumbnailComponent{
  @Input() localbusiness: any;
  faPhone = faPhone;
  faEnvelope = faEnvelope;
  faWebsite = faGlobe;
  faFacebook = faFacebook;
  faInstagram = faInstagram;

  showContactNumber = false;
  showEmail = false;

  openLink(url: string): void {
    window.open(url, '_blank', 'noopener noreferrer');
  }
}

