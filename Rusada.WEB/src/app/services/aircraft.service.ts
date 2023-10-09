import { Injectable } from '@angular/core';
import {
  AddAircraftSightRequest,
  GetAircraftSightRequest,
  UpdateAircraftSightRequest,
} from '../models/add-aircraft-sight-request.model';

import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { formatDate } from '../core/util/util';

@Injectable({
  providedIn: 'root',
})
export class AircraftService {
  apiBaseUrl: string = '';
  constructor(private http: HttpClient) {}

  addAircraft(model: AddAircraftSightRequest): Observable<void> {
    return this.http.post<void>(
      'https://localhost:44396/api/aircraftSighting',
      model
    );
  }

  getAircrafts(): Observable<GetAircraftSightRequest[]> {
    return this.http.get<GetAircraftSightRequest[]>(
      'https://localhost:44396/api/AircraftSighting'
    );
  }

  deleteAircraft(id: number): Observable<boolean> {
    return this.http.delete<boolean>(
      `https://localhost:44396/api/AircraftSighting/${id}`
    );
  }

  updateAircraft(
    data: any,
    image: any
  ): Observable<UpdateAircraftSightRequest> {
    const formData = new FormData();
    formData.append('id', data.id);
    formData.append('make', data.make);
    formData.append('model', data.model);
    formData.append('location', data.location);
    formData.append('dateTime', formatDate(data.dateTime));
    formData.append('imagePath', data.imagePath ?? '');
    formData.append('registration', data.registration);

    if (!this.IsNullOrUndefined(image)) {
      formData.append('image', image, image.name);
    } else {
      formData.append('image', null!);
    }
    return this.http.put<UpdateAircraftSightRequest>(
      'https://localhost:44396/api/AircraftSighting',
      formData
    );
  }

  getAircraftById(id: string): Observable<GetAircraftSightRequest> {
    return this.http.get<GetAircraftSightRequest>(
      `https://localhost:44396/api/AircraftSighting/${id}`
    );
  }

  IsNullOrUndefined(value: any) {
    return value === undefined || value === null;
  }


}
