import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AircraftListComponent } from './aircraft-list.component';

describe('AircraftListComponent', () => {
  let component: AircraftListComponent;
  let fixture: ComponentFixture<AircraftListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AircraftListComponent]
    });
    fixture = TestBed.createComponent(AircraftListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
