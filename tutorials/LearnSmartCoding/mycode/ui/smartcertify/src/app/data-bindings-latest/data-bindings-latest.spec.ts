import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataBindingsLatest } from './data-bindings-latest';

describe('DataBindingsLatest', () => {
  let component: DataBindingsLatest;
  let fixture: ComponentFixture<DataBindingsLatest>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DataBindingsLatest],
    }).compileComponents();

    fixture = TestBed.createComponent(DataBindingsLatest);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
