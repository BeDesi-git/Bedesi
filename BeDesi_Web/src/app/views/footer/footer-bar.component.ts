import { Component } from '@angular/core'
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-footer-bar',
  templateUrl: './footer-bar.component.html',
  styleUrls: ['./footer-bar.component.css']
})
export class FooterBarComponent{
  openTermsPopup() {
    const isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
    const url = '/assets/terms-popup.html';

    if (isMobile) {
      // Open in the same window (full-screen effect)
      window.location.href = url;
    } else {
      // Open in a popup window for desktop users
      const windowFeatures = 'width=800,height=600,scrollbars=yes,resizable=yes';
      window.open(url, '_blank', windowFeatures);
    }
  }
}
