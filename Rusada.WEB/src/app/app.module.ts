import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbAlertModule, NgbDatepickerModule, NgbModule, NgbTimepickerModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { AircraftListComponent } from './features/aircraft/aircraft-list/aircraft-list.component';
import { AircraftAddComponent } from './features/aircraft/aircraft-add/aircraft-add.component';
import { ReactiveFormsModule } from '@angular/forms';

import { FormsModule } from '@angular/forms';
import { AsyncPipe, DecimalPipe, JsonPipe, NgFor } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ToastsContainer } from './core/components/toasts-container/toasts-container.component';
import { ListFilterPipe } from './core/pipes/ListFilterPipe';
import { AircraftEditComponent } from './features/aircraft/aircraft-edit/aircraft-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    AircraftListComponent,
    AircraftAddComponent,
    ToastsContainer,
    ListFilterPipe,
    AircraftEditComponent

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
    HttpClientModule,
    NgbTypeaheadModule,
    DecimalPipe,
    AsyncPipe,
    NgFor
  ],
  providers: [DecimalPipe],
  bootstrap: [AppComponent],
})
export class AppModule {}
