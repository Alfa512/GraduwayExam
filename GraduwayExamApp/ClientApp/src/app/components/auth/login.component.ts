import { Component, OnInit} from '@angular/core';


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

  ngOnInit() {

  }


  openDialog() {

  }

  close() {
    let modalCloseButton: HTMLElement = document.getElementById('auth-modal-close') as HTMLElement;
    modalCloseButton.click();
  }

  auth(authUser: User): void {
    this.tokenService.auth(authUser).subscribe(token => {

      this.helpers.setToken(token);
      this.close();
    });
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
