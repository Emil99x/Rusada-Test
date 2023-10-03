import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';


export interface AircraftSight {
  Id: number;
  make: string;
  model: string;
  registeration: string;
  location: string;
  dateTime: Date;
}

const ELEMENT_DATA: AircraftSight[] = [
  {
    Id: 1,
    make: 'Hydrogen',
    model: 'H-1',
    registeration: 'RH-GHTS',
    location: 'qwewr',
    dateTime: new Date(2021, 11, 17, 4, 28, 0),
  },
  {
    Id: 2,
    make: 'Hydrogen-1',
    model: 'H-2',
    registeration: 'RH-GHTS',
    location: 'qwewr',
    dateTime: new Date(2021, 11, 18, 5, 28, 0),
  },
  {
    Id: 3,
    make: 'Hydrogen-2',
    model: 'H-3',
    registeration: 'RH-GHTS',
    location: 'AS',
    dateTime: new Date(2021, 11, 19, 6, 28, 0),
  },
  {
    Id: 4,
    make: 'Hydrogen-2',
    model: 'H-3',
    registeration: 'RH-GHTS',
    location: 'MK',
    dateTime: new Date(2021, 11, 19, 6, 28, 0),
  },
  {
    Id: 5,
    make: 'Hydrogen-2',
    model: 'H-3',
    registeration: 'RH-GHTS',
    location: 'MKD',
    dateTime: new Date(2021, 11, 19, 6, 28, 0),
  },
  {
    Id: 6,
    make: 'Hydrogen-2',
    model: 'H-3',
    registeration: 'RH-GHTS',
    location: 'qwewr',
    dateTime: new Date(2021, 11, 19, 6, 28, 0),
  },
  {
    Id: 7,
    make: 'Hydrogen-2',
    model: 'H-3',
    registeration: 'RH-GHTS',
    location: 'qwewr',
    dateTime: new Date(2021, 11, 19, 6, 28, 0),
  },
  {
    Id: 8,
    make: 'Hydrogen-2',
    model: 'H-3',
    registeration: 'RH-GHTS',
    location: 'qwewr',
    dateTime: new Date(2021, 11, 19, 6, 28, 0),
  },
];

@Component({
  selector: 'app-aircrafts',
  templateUrl: './aircrafts.component.html',
  styleUrls: ['./aircrafts.component.css'],
})
export class AircraftsComponent implements AfterViewInit {
  displayedColumns: string[] = ['make', 'model', 'registeration', 'location'];
  dataSource = new MatTableDataSource<AircraftSight>(ELEMENT_DATA);

  @ViewChild(MatPaginator) paginator: any;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    debugger;
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    console.log(this.dataSource.filter);
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    } 
  }
}
