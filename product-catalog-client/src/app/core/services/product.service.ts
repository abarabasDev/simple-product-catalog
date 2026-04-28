import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Product } from "../../shared/models/product";
import { catchError, Observable, throwError } from "rxjs";
import { ProductRequest } from "../../shared/models/product-request";
import { Quantity } from "../../shared/enums/quantity";

type ValidationErrors = Record<string, string[]>;

@Injectable({
  providedIn: 'root'
})
export class ProductService {
    private apiUrl = "https://localhost:7100/api/Products";

    constructor(private httpClient: HttpClient) {
    }  

    private handleError(error: HttpErrorResponse){
        let message: string = '';

        if (error.status === Quantity.Zero){
            console.error('Network error:\n', error.error, error.message);
            message = 'Wystąpił błąd połączenia z serwerem. Sprawdź połączenie sieciowe i spróbuj ponownie.'
        }
        else if (error.status === Quantity.FourHundred){
            console.error('Bad request:\n', error.error, error.message);

            const errors: unknown = error?.error?.errors;
            if (errors && typeof errors === "object"){
                message = 'Wystąpiły błędy w formularzu:\n';
                Object.values(errors as ValidationErrors).flat().forEach(msg => {
                    message += ` - ${msg}\n`
                })
            }
            else{
                message = "Wystąpił nieoczekiwany błąd.";
            }
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