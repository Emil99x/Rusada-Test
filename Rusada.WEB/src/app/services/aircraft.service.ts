import { Injectable } from '@angular/core';
import { AddAircraftSightRequest, GetAircraftSightRequest } from '../models/add-aircraft-sight-request.model';

import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AircraftService {
  apiBaseUrl: string = '';
  constructor(private http: HttpClient) {}

  addAircraft(model: AddAircraftSightRequest): Observable<void> {
    return this.http
      .post<void>('https://localhost:44396/api/aircraftSighting', model);
  }

  getAircrafts(): Observable<GetAircraftSightRequest[]> {
    return this.http
      .get<GetAircraftSightRequest[]>('https://localhost:44396/api/AircraftSighting');
  }

  deleteAircraft(id:number): Observable<boolean> {
    return this.http
      .delete<boolean>(`https://localhost:44396/api/AircraftSighting?id=${id}`);
  }

  updateAircraft(): Observable<GetAircraftSightRequest[]> {
    return this.http
      .get<GetAircraftSightRequest[]>('https://localhost:44396/api/AircraftSighting');
  }
}
