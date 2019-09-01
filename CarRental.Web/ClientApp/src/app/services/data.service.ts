import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError  } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Car } from '../models/car';
import { Booking } from '../models/booking';
import { Customer } from '../models/customer';

const BASE_URL: string = 'https://localhost:44325/api/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private httpClient: HttpClient) { }

  public getCustomer(identityNumber: string) {
    const options = identityNumber ?
      { params: new HttpParams().set('identityNumber', identityNumber) } : {};
    return this.httpClient.get<Customer>(BASE_URL + 'customer', options);
  }

  public getRentalCars(): Observable<Car[]> {
    return this.httpClient.get<Car[]>(BASE_URL + 'cars');
  }

  public getBookings(): Observable<Booking[]> {
    return this.httpClient.get<Booking[]>(BASE_URL + 'booking');
  }

  public rentCar(car: Booking): Observable<Booking> {
    return this.httpClient.post<Booking>(BASE_URL + 'booking/add', car, httpOptions);
  }

  public returnCar(booking: Booking): Observable<Booking> {
    return this.httpClient.post<Booking>(BASE_URL + 'booking/update', booking, httpOptions);
  }

  public getPrice(booking: Booking): Observable<number>{
    return this.httpClient.post<number>(BASE_URL + 'booking/price', booking, httpOptions);
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
}
