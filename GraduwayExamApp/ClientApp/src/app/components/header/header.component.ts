import { Component, NgModule } from '@angular/core';
import { Helpers } from '@app/helpers/helpers';
import { LoginComponent } from "@app/components/auth/login.component";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent {

  authentication: boolean;

  constructor(private helper: Helpers) {
    this.authentication = helper.isAuthenticated();
    helper.isAuthenticationChanged().subscribe(data => {
      this.authentication = data;

    });
  }

  openModal(): void {
    
  }
  close() {
  }

  logout(): void {
    this.helper.logout();
  }

}
