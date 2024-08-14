import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  userName: string | null = null;

  constructor(private jwtHelper: JwtHelperService, private router: Router) {
    const token = localStorage.getItem('jwt');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      this.userName = decodedToken?.name || 'User'; // Supondo que o nome de usuário está em `unique_name`
    }
  }

  logOut() {
    localStorage.removeItem('jwt');
    this.router.navigate(['/login']); // Redireciona para a página de login após o logout
  }

}
