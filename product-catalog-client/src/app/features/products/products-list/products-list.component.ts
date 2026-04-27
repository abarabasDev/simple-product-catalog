import { Component, OnInit, signal } from '@angular/core';
import { ProductService } from '../../../core/services/product.service';
import { Product } from '../../../shared/models/product';
import { RouterLink } from '@angular/router';
import { ProductsTableComponent } from '../products-table/products-table.component';
import { NavbarService } from '../../../core/services/navbar.service';
import { FormsModule, ɵInternalFormsSharedModule } from "@angular/forms";

@Component({
  selector: 'products-list',
  imports: [RouterLink, ProductsTableComponent, ɵInternalFormsSharedModule, FormsModule],
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  productsToShow = signal<Product[]>([]);
  products: Product[] = [];

  searchProductsText = '';

  constructor(private productService: ProductService, private navbarServie: NavbarService) { }

  ngOnInit() {
    this.navbarServie.updateTitle("Katalog produktów");

    this.productService.getProducts().subscribe({
      next: value => { 
        this.products = value
        this.productsToShow.set(this.products); 
      },
      error: err => { 
        alert(err.message) 
      }
    });
  }

  searchProducts() {
    const filterValue = this.searchProductsText.toLowerCase();

    if (filterValue){
      const filteredProducts = this.products
        .filter(x => x.name.toLowerCase().includes(filterValue) 
                  || x.manufacturerCode.toLowerCase().includes(filterValue));

      this.productsToShow.set(filteredProducts);
    }
    else {
      this.productsToShow.set(this.products); 
    }
  }


}
