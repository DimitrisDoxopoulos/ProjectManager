import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertAssignmentComponent } from './insert-assignment.component';

describe('InsertAssignmentComponent', () => {
  let component: InsertAssignmentComponent;
  let fixture: ComponentFixture<InsertAssignmentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [InsertAssignmentComponent]
    });
    fixture = TestBed.createComponent(InsertAssignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
