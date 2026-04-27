import { Component, effect, input } from '@angular/core';
import { Product } from '../../../shared/models/product';
import { Quantity } from '../../../shared/enums/quantity';

@Component({
  selector: 'products-table',
  templateUrl: './products-table.component.html',
  styleUrls: ['./products-table.component.css']
})
export class ProductsTableComponent {
  productsInput = input<Product[]>([]);
  products: Product[] = [];

  sortColumnName: string = '';
  isAsc: boolean = false;

  constructor(){
    effect(() => {
      const inputValue = this.productsInput()

      if (inputValue.length !== Quantity.Zero){
        
        this.products = inputValue;

        this.sortColumnName = '';
        this.isAsc = false;        
        this.sortOverColumnData('name');
      }
    })    
  }

  sortOverColumnData(columnName: string) {
    this.isAsc = this.sortColumnName === columnName ? !this.isAsc : true;
    this.sortColumnName = columnName;

    this.products.sort((a: any, b: any) => {
      const firstValue = a[columnName];
      const secondValue = b[columnName];

      return this.compareData(firstValue, secondValue);
    })
  }
  
  compareData(firstValue: any, secondValue: any): number{
      if (typeof firstValue === 'string' && typeof secondValue === 'string') {
        const localeComparison = firstValue.localeCompare(secondValue, 'pl', { sensitivity: 'base' });
        return this.isAsc ? localeComparison : -localeComparison;
      }      

      if (firstValue > secondValue) return this.isAsc ? Quantity.One : Quantity.MinusOne;
      if (firstValue < secondValue) return this.isAsc ? Quantity.MinusOne : Quantity.One;
      return Quantity.Zero;
  }
}
