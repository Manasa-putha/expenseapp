import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllexpensesComponent } from './allexpenses.component';

describe('AllexpensesComponent', () => {
  let component: AllexpensesComponent;
  let fixture: ComponentFixture<AllexpensesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AllexpensesComponent]
    });
    fixture = TestBed.createComponent(AllexpensesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
