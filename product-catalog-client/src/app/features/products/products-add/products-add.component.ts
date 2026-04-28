import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { ProductRequest } from '../../../shared/models/product-request';
import { FormValidationMessageComponent } from "../../../shared/components/form-validation-message/form-validation-message.component";
import { NavbarService } from '../../../core/services/navbar.service';

@Component({
  selector: 'products-add',
  imports: [ReactiveFormsModule, RouterLink, FormValidationMessageComponent],
  templateUrl: './products-add.component.html',
  styleUrls: ['./products-add.component.css']
})
export class ProductsAddComponent implements OnInit {

  constructor(private productService: ProductService, private router: Router, private navbarServie: NavbarService) { }

  ngOnInit() {
    this.navbarServie.updateTitle("Dodaj produkt");
  }

  newProductForm = new FormGroup({
    name: new FormControl('', Validators.required),
    manufacturerCode: new FormControl('', [Validators.required, Validators.minLength(10)]),
    price: new FormControl<number | null>(null, Validators.required),
    quantity: new FormControl<number | null>(null, [Validators.required, Validators.min(1)]),
  });

  submitForm() {
    const formValue = this.newProductForm.value;

    this.productService.addProduct(formValue as ProductRequest).subscribe({
      next: value => {
        console.log(value);
        this.router.navigate(['/']);
      },
      error: err => {
        alert(err.message);
      }
    });
  } 
}
