import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AircraftsComponent } from './aircrafts/aircrafts.component';
import { ErrorComponent } from './error/error.component';
import { AddAircraftComponent } from './aircrafts/add-aircraft/add-aircraft.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'aircrafts', component: AircraftsComponent },
  { path: 'aircrafts/add', component: AddAircraftComponent },
  { path: 'error', component: ErrorComponent },
  { path: '', component: LoginComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
