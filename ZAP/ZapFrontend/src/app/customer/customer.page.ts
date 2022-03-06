import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Customer } from '../models/customer/customer';
import { CustomerService } from './customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.page.html',
  styleUrls: ['./customer.page.scss'],
})
export class CustomerPage implements OnInit {

  public customerList : Customer[];

  constructor(private customerService : CustomerService) { }

  ngOnInit() {
  }

  ionViewWillEnter(){
    this.customerService.getCustomers().pipe(take(1)).subscribe(
      data =>{
        this.customerList = data;
      }
    );
  }
}
