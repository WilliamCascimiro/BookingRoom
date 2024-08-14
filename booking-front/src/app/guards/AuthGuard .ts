import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {
  constructor(private router:Router, private jwtHelper: JwtHelperService){}
  
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)){

      const decodedToken = this.jwtHelper.decodeToken(token);
      const expectedRoles  = next.data['expectedRole'] as Array<string>;
      const userRoles  = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];  // Supondo que a role está armazenada em 'roles'
      if (userRoles && expectedRoles.some(role => userRoles.includes(role))) {
         return true;
      }
    }
    this.router.navigate(["index"]);
    return false;
  }
}