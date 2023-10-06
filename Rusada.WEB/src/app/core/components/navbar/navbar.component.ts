import { Component, OnInit } from '@angular/core';
import { decodeJWTToken } from '../../util/util';
import { Router } from '@angular/router';
declare var handleSignout: any;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  userProfile: any;
  constructor(private router: Router) {}
  
  ngOnInit() {
    var token = sessionStorage.getItem('loggedInUser');
    if (token) {
      this.userProfile = decodeJWTToken(token);
    }
  }

  signOut() {
    handleSignout();
    sessionStorage.removeItem('loggedInUser');
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }
}
