import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { ProductsComponent } from './components//products/products.component';
import { CategoryComponent } from './components//category/category.component';
import { CategoryAddEditComponent } from './components/category-addedit/category-addedit.component';
import { ProductComponent } from './components/product/product.component';
import { ProductAddEditComponent } from './components/product-addedit/product-addedit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CategoriesComponent,
    ProductsComponent,
    CategoryComponent,
    CategoryAddEditComponent,
    ProductComponent,
    ProductAddEditComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'categories', component: CategoriesComponent },
      { path: 'categories/:id', component: CategoryComponent },
      { path: 'addcategory', component: CategoryAddEditComponent },
      { path: 'categories/edit/:id', component: CategoryAddEditComponent },
      { path: 'products', component: ProductsComponent },
      { path: 'products/:id', component: ProductComponent },
      { path: 'addproduct', component: ProductAddEditComponent },
      { path: 'products/edit/:id', component: ProductAddEditComponent },
      { path: '**', redirectTo: '/' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
