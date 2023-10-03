import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AircraftsComponent } from './aircrafts.component';

describe('AircraftsComponent', () => {
  let component: AircraftsComponent;
  let fixture: ComponentFixture<AircraftsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AircraftsComponent]
    });
    fixture = TestBed.createComponent(AircraftsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
