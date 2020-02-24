import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductAddEditComponent } from './product-addedit.component';

describe('ProductAddeditComponent', () => {
  let component: ProductAddeditComponent;
  let fixture: ComponentFixture<ProductAddeditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductAddeditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductAddeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
