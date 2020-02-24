import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RestService } from '../../services/rest.service';
import { Product } from '../../models/product';
import { Category } from '../../models/category';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  constructor(private restService: RestService) { }

  products$: Observable<Product[]>;
  categories$: Observable<Category[]>;
  productId: number;

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.products$ = this.restService.getProducts();
    this.categories$ = this.restService.getCategories();
  }

  delete(name, productId) {
    const ans = confirm('Delete "' + name + '" product? (id: ' + productId + ')');

    if (ans) {
      this.restService.deleteProduct(productId).subscribe((data) => {
        this.loadProducts();
      });
    }
  }
}
