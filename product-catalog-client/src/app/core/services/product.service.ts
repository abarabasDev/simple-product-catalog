import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Product } from "../../shared/models/product";
import { catchError, Observable, throwError } from "rxjs";
import { ProductRequest } from "../../shared/models/product-request";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
    private apiUrl = "https://localhost:7100/api/Products";

    constructor(private httpClient: HttpClient) {
    }  

    private handleError(error: HttpErrorResponse){
        let message: string;
        
        if (error.status === 0){
            console.error('Network error:\n', error.error, error.message);
            message = 'Wystąpił błąd połączenia z serwerem. Sprawdź połączenie sieciowe i spróbuj ponownie.'
        }
        else {
            console.error('Server side error:\n', error.error, error.message);
            message = 'Wystąpił błąd po stronie serwera. Spróbuj ponownie.'
        }

        return throwError(() => new Error(message));
    }    

    getProducts(){
        return this.httpClient.get<Product[]>(this.apiUrl).pipe(catchError(this.handleError));
    }

    addProduct(request: ProductRequest): Observable<any> {
        return this.httpClient.post(this.apiUrl, request).pipe(catchError(this.handleError))
    }    
}