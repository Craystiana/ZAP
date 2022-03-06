import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { API_URL, CUSTOMER_BLACKLIST_URL, CUSTOMER_DETAILS_URL, CUSTOMER_URL } from 'src/environments/environment';
import { Customer } from '../models/customer/customer';
import { CustomerDetails } from '../models/customer/customerDetails';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  public getCustomers(){
    return this.http.get(API_URL + CUSTOMER_URL).pipe(
      map((data : Customer[]) => {
        return data;
      })
    );
  }

  public getCustomerDetails(customerId: number){
    return this.http.get(API_URL + CUSTOMER_DETAILS_URL + customerId).pipe(
      map((data : CustomerDetails) => {
        return data;
      })
    );
  }

  public blacklist(customerId: number){
    return this.http.post(API_URL + CUSTOMER_BLACKLIST_URL + customerId, null).pipe(
      map((data : boolean) =>{
        return data;
      })
    );
  }
}
