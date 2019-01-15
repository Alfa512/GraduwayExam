import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './helpers/canActivateAuthGuard';

import { LoginComponent } from './components/auth/login.component';

import { LogoutComponent } from './components/auth/logout.component';
import { DashboardComponent } from "./components/dashboard/dashboard.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent },

  { path: 'logout', component: LogoutComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
