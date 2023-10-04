import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AircraftListComponent } from './features/aircraft/aircraft-list/aircraft-list.component';
import { AircraftAddComponent } from './features/aircraft/aircraft-add/aircraft-add.component';

const routes: Routes = [
  { path: 'aircrafts', component: AircraftListComponent },
  { path: 'aircrafts/add', component: AircraftAddComponent },
  { path: '**', component: AircraftListComponent }
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
