import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AircraftListComponent } from './features/aircraft/aircraft-list/aircraft-list.component';
import { AircraftAddComponent } from './features/aircraft/aircraft-add/aircraft-add.component';
import { AircraftEditComponent } from './features/aircraft/aircraft-edit/aircraft-edit.component';
import { LoginComponent } from './core/components/login/login.component';
import { authGuard } from './services/auth.guard';

const routes: Routes = [
  { path: 'aircrafts', component: AircraftListComponent , canActivate:[authGuard]},
  { path: 'aircrafts/add', component: AircraftAddComponent, canActivate:[authGuard] },
  { path: 'aircrafts/edit/:id', component: AircraftEditComponent , canActivate:[authGuard]},
  { path: 'login', component: LoginComponent },
  { path: '', component: LoginComponent },
  { path: '**', component: LoginComponent }
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
