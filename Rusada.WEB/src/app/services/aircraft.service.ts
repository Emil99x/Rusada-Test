import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class AircraftService {

  private readonly API_URL = 'https://localhost:44396/'; // TODO : move into env 

  constructor(private httpClient: HttpClient) {}
}
