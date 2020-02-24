import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { RestService } from '../../services/rest.service';
import { Category } from '../../models/category';


@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  category$: Observable<Category>;
  categoryId: number;
  category: Category;

  constructor(private restService: RestService, private avRoute: ActivatedRoute) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) {
      this.categoryId = this.avRoute.snapshot.params[idParam];
    }
  }

  ngOnInit() {
    this.loadCategory();
  }

  loadCategory() {
    this.category$ = this.restService.getCategory(this.categoryId);
  }
}

