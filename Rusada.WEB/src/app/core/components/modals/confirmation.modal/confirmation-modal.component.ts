import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirmation-modal',
  standalone: true,
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.css']
})
export class ConfirmationModalComponent {

  @Input() message! :string ;
  @Output() onSubmit: EventEmitter<any> = new EventEmitter();
	constructor(public activeModal: NgbActiveModal) {}

  submitted (){
    this.onSubmit.emit(null)
    this.activeModal.close();
  }
}
