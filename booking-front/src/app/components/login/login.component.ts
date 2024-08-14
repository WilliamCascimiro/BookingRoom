import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticatedResponse } from 'src/app/interfaces/Auth/AuthenticatedResponse';
import { LoginModel } from 'src/app/interfaces/Auth/LoginModel ';
import { AuthService } from 'src/app/services/Auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
    invalidLogin: boolean | undefined;
    credentials: LoginModel = {email:'', password:''};
    constructor(private router: Router, private authService: AuthService) { }
   
    login = ( form: NgForm) => {
      if (form.valid) {
        this.authService.AuthUser(this.credentials).subscribe({
            next: (response: AuthenticatedResponse) => {
            this.authService.storeToken(response.token)
            this.invalidLogin = false; 
            this.router.navigate(["/index"]);
            },
            error: (err: HttpErrorResponse) => this.invalidLogin = true
        });
      }
    }
  }
