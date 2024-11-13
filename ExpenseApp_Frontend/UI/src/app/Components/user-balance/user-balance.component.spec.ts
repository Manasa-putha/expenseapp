import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserBalanceComponent } from './user-balance.component';

describe('UserBalanceComponent', () => {
  let component: UserBalanceComponent;
  let fixture: ComponentFixture<UserBalanceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserBalanceComponent]
    });
    fixture = TestBed.createComponent(UserBalanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
