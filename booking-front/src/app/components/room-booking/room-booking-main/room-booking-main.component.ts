import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from 'src/app/services/Auth/auth.service';

@Component({
  selector: 'app-room-booking-main',
  templateUrl: './room-booking-main.component.html',
  styleUrls: ['./room-booking-main.component.css']
})
export class RoomBookingMainComponent {
  
  constructor(private jwtHelper: JwtHelperService,  private authService: AuthService, private router: Router) { }
  
  isUserAuthenticated = (): boolean => {
    const token = this.authService.getToken();
    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }
    return false;
  }
  
  logOut = () => {
    this.authService.removeToken()
    this.router.navigate(['/login']);
  }
}
