import { Component } from '@angular/core';
import { Helpers } from '@app/helpers/helpers';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent {

  authentication: boolean;
  constructor(private helper: Helpers) {
    this.authentication = helper.isAuthenticated();
  }

  logout(): void {
    this.helper.logout();
  }

}
