import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbAlertModule, NgbDatepickerModule, NgbModule, NgbTimepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { AircraftListComponent } from './features/aircraft/aircraft-list/aircraft-list.component';
import { AircraftAddComponent } from './features/aircraft/aircraft-add/aircraft-add.component';
import { ReactiveFormsModule } from '@angular/forms';


import { FormsModule } from '@angular/forms';
import { JsonPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    AircraftListComponent,
    AircraftAddComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    NgbDatepickerModule,
    NgbTimepickerModule ,
    NgbAlertModule,
    FormsModule,
    JsonPipe,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
