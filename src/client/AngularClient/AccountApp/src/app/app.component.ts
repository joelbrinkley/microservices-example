import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(public authService: AuthService) {

  }

  ngOnInit() {

 
    if (window.location.hash) {
      this.authService.authorizedCallback();
      this.authService.isAuthorized$.next(true);
    }
    else {
      this.authService.login();
    }  
  }

  logout(){
    this.authService.logout();
  }
}
