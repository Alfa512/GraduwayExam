import { Component, OnInit} from '@angular/core';

import { Router } from '@angular/router';

import { TokenService } from '@app/services/token.service';

import { Helpers } from '@app/helpers/helpers';
import './js/login.js';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})

export class LoginComponent implements OnInit {

  constructor(private helpers: Helpers, private router: Router, private tokenService: TokenService) {

  }

  ngOnInit() {



  }

  login(): void {

    let authValues = { "Username": "pablo", "Password": "secret" };

    this.tokenService.auth(authValues).subscribe(token => {

      this.helpers.setToken(token);

      this.router.navigate(['/dashboard']);

    });

  }

} 
