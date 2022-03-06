import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoadingController, ToastController } from '@ionic/angular';
import { take } from 'rxjs/operators';
import { CustomerDetails } from 'src/app/models/customer/customerDetails';
import { ReservationDetails } from 'src/app/models/reservation/reservationDetails';
import { ReservationService } from 'src/app/reservation/reservation.service';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.page.html',
  styleUrls: ['./detail.page.scss'],
})
export class DetailPage implements OnInit {

  private customerId : number;
  private customer : CustomerDetails;
  private reservations: ReservationDetails[];
  
  constructor(private route: ActivatedRoute,
              private loadingController: LoadingController,
              private toastCtrl : ToastController,
              private customerService : CustomerService,
              private reservationService: ReservationService) { }

  ngOnInit() {
  }

  async ionViewWillEnter(){
    const loading = await this.loadingController.create({
      message: 'Loading customer details.\nPlease wait...',
      backdropDismiss: false
    });

    await loading.present();
    this.route.queryParams.subscribe(params => {
      if (params) {
        this.customerId = params['customerId'];
      }
    });

    this.customerService.getCustomerDetails(this.customerId)
    .pipe(take(1))
    .subscribe(data =>{
      this.customer = data;
    });

    this.reservationService.getUserReservations(this.customerId)
    .pipe(take(1))
    .subscribe(data =>{
      this.reservations = data;
      loading.dismiss();
    })
  }

  onBlacklist(){
    this.customerService.blacklist(this.customerId)
    .pipe(take(1))
    .subscribe(
      data =>{
        if(data === true){
          this.toastCtrl.create({
            message: this.customer.isBlacklisted? 'Customer removed from blacklist successfully' : 'Customer blacklisted successfully',
            duration: 5000,
            position: 'bottom',
            color: 'warning',
            buttons: ['Dismiss']
          }).then((el) => el.present());

          // Update frontend data
          this.customer.isBlacklisted = !this.customer.isBlacklisted;
        } else {
          this.toastCtrl.create({
            message: this.customer.isBlacklisted ? 'Failed to remove customer from blacklist' : 'Failed to blacklist customer',
            duration: 5000,
            position: 'bottom',
            color: 'danger',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
      },
      error =>{
        this.toastCtrl.create({
          message: this.customer.isBlacklisted ? 'Failed to remove customer from blacklist' : 'Failed to blacklist customer',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
      }
    );
  }

}
