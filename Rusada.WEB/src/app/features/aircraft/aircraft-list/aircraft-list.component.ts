import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { ConfirmationModalComponent } from 'src/app/core/components/modals/confirmation.modal/confirmation-modal.component';
import { GetAircraftSightRequest } from 'src/app/models/add-aircraft-sight-request.model';
import { AircraftService } from 'src/app/services/aircraft.service';
import { ToastService } from 'src/app/services/toast-service';

@Component({
  selector: 'app-aircraft-list',
  templateUrl: './aircraft-list.component.html',
  styleUrls: ['./aircraft-list.component.css'],
})
export class AircraftListComponent implements OnInit {
  data!: GetAircraftSightRequest[];
  allData!: GetAircraftSightRequest[];

  searchTerm: string = '';
  filter = new FormControl('', { nonNullable: true });

  constructor(
    private aircraftService: AircraftService,
    private modalService: NgbModal,
    private toastService : ToastService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAircraftData();
  }

  getAircraftData(){
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

  edit(data: GetAircraftSightRequest) {
    this.router.navigate(['/aircrafts/edit',data.id]);
  }

  delete(id: any) {
    this.aircraftService.deleteAircraft(id).subscribe({
      next: (res) => {
      this.getAircraftData();
       this.toastService.show('success', { classname: 'bg-success text-light', delay: 2000 }); 
      },error: (res) =>{
        this.toastService.show('error occured', { classname: 'bg-danger text-light', delay: 2000 });
      }
    })
  }

  remove(data: GetAircraftSightRequest) {

    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.message = 'Are you really want to delete this';
    modalRef.componentInstance.onSubmit.subscribe((event : any) => this.delete(data.id))
  }
}
