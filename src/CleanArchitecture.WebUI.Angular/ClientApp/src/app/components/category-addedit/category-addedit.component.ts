import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { RestService } from '../../services/rest.service';
import { Category } from '../../models/category';

@Component({
  selector: 'app-category-addedit',
  templateUrl: './category-addedit.component.html',
  styleUrls: ['./category-addedit.component.css']
})
export class CategoryAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formName: string;
  categoryId: number;
  errorMessage: any;
  existingCategory: Category;

  constructor(private restService: RestService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formName = 'name';

    if (this.avRoute.snapshot.params[idParam]) {
      this.categoryId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        categoryId: 0,
        name: ['', [Validators.required]]
      }
    )
  }

  ngOnInit() {
    if (this.categoryId > 0) {
      this.actionType = 'Edit';
      this.restService.getCategory(this.categoryId)
        .subscribe(data => (
          this.existingCategory = data,
          this.form.controls[this.formName].setValue(data.name)
        ));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      let category: Category = {
        name: this.form.get(this.formName).value
        //products: null
      };

      this.restService.saveCategory(category)
        .subscribe((data) => {
          this.router.navigate(['/categories']);
        });
    }

    if (this.actionType === 'Edit') {
      let category: Category = {
        id: this.existingCategory.id,
        name: this.form.get(this.formName).value,
        //products: null
  
      };
      this.restService.updateCategory(category.id, category)
        .subscribe((data) => {
          this.router.navigate(['/categories']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/categories']);
  }

  get name() { return this.form.get(this.formName); }
}
