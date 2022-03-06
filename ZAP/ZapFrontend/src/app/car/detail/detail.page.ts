import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { LoadingController, ToastController } from '@ionic/angular';
import { take } from 'rxjs/operators';
import { AuthService } from 'src/app/auth/auth.service';
import { ReservationType } from 'src/app/common/reservationType';
import { Car } from 'src/app/models/car/car';
import { Reservation } from 'src/app/models/reservation/reservation';
import { ReservationDetails } from 'src/app/models/reservation/reservationDetails';
import { ReservationService } from 'src/app/reservation/reservation.service';
import { CarService } from '../car.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.page.html',
  styleUrls: ['./detail.page.scss'],
})
export class DetailPage implements OnInit {

  private car : Car;
  private carId : number;
  private startDate: Date;
  private endDate : Date;

  public isLoading: boolean;
  private reservations: ReservationDetails[];

  public get reservationType() : typeof ReservationType{
    return ReservationType;
  }

  constructor(private route: ActivatedRoute, 
              private loadingController: LoadingController, 
              private carService : CarService, 
              private router: Router, 
              private toastCtrl : ToastController,
              private authService : AuthService,
              private reservationService: ReservationService) { }

  async ngOnInit() {
    const loading = await this.loadingController.create({
      message: 'Loading car. Please wait...',
      backdropDismiss: false
    });

    await loading.present();

    this.route.queryParams.subscribe(params => {
      if (params) {
        this.carId = params['carId'];
      }
      if(this.carId === undefined){
        loading.dismiss();
        this.router.navigateByUrl("/car");
      }
    });

    this.carService.getCarDetails(this.carId)
    .pipe(take(1))
    .subscribe(
      data => {
        this.car = data;
      }
    );

    this.reservationService.getCarReservations(this.carId)
    .pipe(take(1))
    .subscribe(
      data => {
        this.reservations = data;
        loading.dismiss();
      }
    );  
  }

  onRent(){
    console.log(this.startDate);
    console.log(this.endDate);

    var model = new Reservation(this.car.carId, this.startDate, this.endDate);
    this.reservationService.addReservation(model).pipe(take(1))
    .subscribe(
      data => {
        if(data === true){
          this.toastCtrl.create({
            message: 'Reservation made succesfully',
            duration: 5000,
            position: 'bottom',
            color: 'success',
            buttons: ['Dismiss']
          }).then((el) => el.present());
          this.router.navigateByUrl("/car");
        }else{
          this.toastCtrl.create({
            message: 'Car unavailable for the selected dates, please try again',
            duration: 5000,
            position: 'bottom',
            color: 'warning',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
      },
      error => {
        this.presentError();
      }
    );
  }

  onDelete(){
    this.carService.delete(this.carId)
    .pipe(take(1))
    .subscribe(
      data => {
        if(data === true){
          this.router.navigateByUrl("/car");
          this.toastCtrl.create({
            message: 'Car removed successfully',
            duration: 5000,
            position: 'bottom',
            color: 'warning',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        } else {
         this.presentError();
        }
      },
      error => {
        this.presentError();
      }
    );
  }

  editPage(){
    this.router.navigateByUrl("/car/edit?carId=" + this.carId);
  }

  presentError(){
    this.toastCtrl.create({
      message: 'Failed to remove car',
      duration: 5000,
      position: 'bottom',
      color: 'danger',
      buttons: ['Dismiss']
    }).then((el) => el.present());
  }
}
