import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/product';
import { Observable } from 'rxjs';
import { RestService } from '../../services/rest.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  product$: Observable<Product>;
  productId: number;
  product: Product;

  constructor(private restService: RestService, private avRoute: ActivatedRoute) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) {
      this.productId = this.avRoute.snapshot.params[idParam];
    }
  }

  ngOnInit() {
    this.loadProduct();
  }

  loadProduct() {
    this.product$ = this.restService.getProduct(this.productId);
  }
}
