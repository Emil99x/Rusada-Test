import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { GetAircraftSightRequest } from 'src/app/models/add-aircraft-sight-request.model';
import { AircraftService } from 'src/app/services/aircraft.service';

@Component({
  selector: 'app-aircraft-list',
  templateUrl: './aircraft-list.component.html',
  styleUrls: ['./aircraft-list.component.css'],
})
export class AircraftListComponent implements OnInit {
  data!: GetAircraftSightRequest[];
  allData!: GetAircraftSightRequest[];
  searchTerm: string ='';
  filter = new FormControl('', { nonNullable: true });

  constructor(private aircraftService: AircraftService) {}


  ngOnInit(): void {
    this.aircraftService.getAircrafts().subscribe({
      next: (res) => {
        this.data = res;
		this.allData = res;
      },
    });
  }

  search(event: any): void {
    this.data = this.allData.filter((val) => {
      return (
        val.make.includes(event.target.value) ||
        val.model.includes(event.target.value) ||
        val.registration.includes(event.target.value)
      );
    });
  }

  edit(data : GetAircraftSightRequest){
    console.log(data);
  }
  remove(data : GetAircraftSightRequest){
    console.log(data);
  }
}
