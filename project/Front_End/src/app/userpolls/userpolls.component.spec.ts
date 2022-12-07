import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserpollsComponent } from './userpolls.component';

describe('UserpollsComponent', () => {
  let component: UserpollsComponent;
  let fixture: ComponentFixture<UserpollsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserpollsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserpollsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
