import { Component, OnInit} from '@angular/core';

import { HttpErrorResponse } from '@angular/common/http';

import { TokenService } from '@app/services/token.service';
import { UserService } from '@app/services/user.service';

import { Helpers } from '@app/helpers/helpers';
import { User } from '@app/models/user';

import './js/login.js';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
  

export class LoginComponent implements OnInit {

  constructor(private helpers: Helpers, private tokenService: TokenService, private userService: UserService) {

  }

  user: User;
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
  confirmPassword: string;

  loading: boolean = false;
  showValidation: boolean;
  authError: string;

  ngOnInit() {

  }


  openDialog() {

  }

  close() {
    let modalCloseButton: HTMLElement = document.getElementById('auth-modal-close') as HTMLElement;
    modalCloseButton.click();
  }

  auth(authUser: User): void {
    this.loading = true;
    this.showValidation = false;
    this.authError = '';
    this.tokenService.auth(authUser).subscribe(token => {
      this.loading = false;
      this.showValidation = false;
      this.helpers.setToken(token);
      this.close();
    },
      (error: HttpErrorResponse) => {
        this.loading = false;
        this.showValidation = true;
        this.authError = 'Invalid Username or Password';
    });
    this.authError = this.tokenService.errorMessage;

  }

  logout(): void {
    this.tokenService.logout();
  }

  login(): void {

    this.user = new User();
    this.user.userName = this.userName;
    this.user.password = this.password;

    this.auth(this.user);
  }

  register(): void {

    let user = new User();
    user.firstName = this.firstName;
    user.lastName = this.lastName;
    user.userName = this.userName;
    user.email = this.email;
    user.password = this.password;
    user.confirmPassword = this.confirmPassword;

    this.userService.createUser(user).subscribe(user => {

      this.user = user;
      let authUser = user;
      authUser.password = this.password;
      this.auth(authUser);
    });
  }
} 
