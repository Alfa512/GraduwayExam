
import { CanActivate, Router } from '@angular/router';

import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { Helpers } from './helpers';

import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { TokenService } from "@app/services/token.service";

@Injectable()

export class AuthGuard implements CanActivate {

  constructor(private router: Router, private helper: Helpers, private tokenService: TokenService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {

    if (!this.tokenService.isAuthenticated()) {

      this.router.navigate(['/login']);

      return false;
    }
    return true;
  }
}
