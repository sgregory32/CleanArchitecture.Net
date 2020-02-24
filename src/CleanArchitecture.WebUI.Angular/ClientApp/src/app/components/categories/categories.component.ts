import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RestService } from '../../services/rest.service';
import { Category } from '../../models/category';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})

export class CategoriesComponent implements OnInit {

  categories$: Observable<Category[]>;
  categoryId: number;

  constructor(private restService : RestService) { }

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.categories$ = this.restService.getCategories();
  }

  delete(name, categoryId, hasProducts) {
    if (hasProducts) {
      alert('Category has associated products and cannot be deleted!');
    } else {
      const ans = confirm('Delete "' + name + '" category? (id: ' + categoryId + ')');
      if (ans) {
        this.restService.deleteCategory(categoryId).subscribe((data) => {
          this.loadCategories();
        });
      }
    }
  }
}
