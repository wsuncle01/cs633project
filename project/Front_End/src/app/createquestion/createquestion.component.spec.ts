import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatequestionComponent } from './createquestion.component';

describe('CreatequestionComponent', () => {
  let component: CreatequestionComponent;
  let fixture: ComponentFixture<CreatequestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatequestionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatequestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
