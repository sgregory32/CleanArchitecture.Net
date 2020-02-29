import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { RestService } from '../../services/rest.service';
import { Product } from '../../models/product';
import { Category } from '../../models/category';

@Component({
  selector: 'app-product-addedit',
  templateUrl: './product-addedit.component.html',
  styleUrls: ['./product-addedit.component.css']
})

export class ProductAddEditComponent implements OnInit {

  form: FormGroup;
  actionType: string;
  formName: string;
  formDescription: string;
  formCategoryId;
  productId: number;
  errorMessage: any;
  existingProduct: Product;
  categories$: any;

  constructor(private restService: RestService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formName = 'name';
    this.formDescription = 'description';
    this.formCategoryId = 'categoryId';
    this.categories$ = this.restService.getCategories();

    if (this.avRoute.snapshot.params[idParam]) {
      this.productId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        productId: 0,
        name: ['', [Validators.required]],
        description: ['', [Validators.required]],
        categoryId: ['', [Validators.required]]
      }
    )
  }

  ngOnInit() {
    if (this.productId > 0) {
      this.actionType = 'Edit';
      this.restService.getProduct(this.productId)
        .subscribe(data => (
          this.existingProduct = data,
          this.form.controls[this.formName].setValue(data.name),
          this.form.controls[this.formDescription].setValue(data.description),
          this.form.controls[this.formCategoryId].setValue(data.categoryId)
        ));
    } else {
      this.form.controls[this.formCategoryId].setValue(null);
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      const product: Product = {
        name: this.form.get(this.formName).value,
        description: this.form.get(this.formDescription).value,
        categoryId: Number(this.form.get(this.formCategoryId).value),
        category: null
      };

      this.restService.saveProduct(product)
        .subscribe(() => {
          this.router.navigate(['/products']);
        });
    }

    if (this.actionType === 'Edit') {
      const product: Product = {
        id: this.existingProduct.id,
        name: this.form.get(this.formName).value,
        description: this.form.get(this.formDescription).value,
        categoryId: Number(this.form.get(this.formCategoryId).value),
        category: null
      };

      this.restService.updateProduct(product.id, product)
        .subscribe(() => {
          this.router.navigate(['/products']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/products']);
  }

  get name() { return this.form.get(this.formName); }
  get description() { return this.form.get(this.formDescription); }
  get categoryId() { return this.form.get(this.formCategoryId); }
}
