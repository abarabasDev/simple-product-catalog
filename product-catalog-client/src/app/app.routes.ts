import { Routes } from '@angular/router';
import { ProductsListComponent } from './features/products/products-list/products-list.component';
import { ProductsAddComponent } from './features/products/products-add/products-add.component';

export const routes: Routes = [
    {
        path: '',
        component: ProductsListComponent
    },
    {
        path: 'add-product',
        component: ProductsAddComponent
    }       
];
