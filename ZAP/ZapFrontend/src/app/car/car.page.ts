import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PopoverController, ToastController } from '@ionic/angular';
import { first, take } from 'rxjs/operators';
import { AuthService } from '../auth/auth.service';
import { SortType } from '../common/sortType';
import { Car } from '../models/car/car';
import { CarData } from '../models/car/carData';
import { CarQuery } from '../models/car/carQuery';
import { CarService } from './car.service';

@Component({
  selector: 'app-car',
  templateUrl: './car.page.html',
  styleUrls: ['./car.page.scss'],
})
export class CarPage implements OnInit {
  private carData: CarData;
  private searchTerm: string;
  private carType: number[];
  private carClass: number[];
  private carBrand: number[];
  private sortBy: number;

  public carList: Car[];
  public isLoading = false;

  public get sortType() : typeof SortType{
    return SortType;
  }

  constructor(private carService: CarService, private popoverController: PopoverController, private toastCtrl: ToastController, private router: Router, private authService : AuthService) { }

  ngOnInit() {
    this.fetchStaticData();
  }

  ionViewWillEnter(){
    this.fetchStaticData();
    this.getCarList();
  }

  getCarList(){
    this.isLoading = true;

    var model = new CarQuery(this.carType,
                             this.carClass,
                             this.carBrand,
                             this.sortBy,
                             this.searchTerm);

    this.carService.getCarList(model).pipe(first()).subscribe(
      data =>{
        this.carList = data;
        this.isLoading = false;
      },
      error => {
        this.toastCtrl.create({
          message: 'Unable to get the car list',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
        
        this.isLoading = false;
    });
  }

  fetchStaticData(){
    this.carService.getCarData().pipe(take(1)).subscribe(
      data => {
        this.carData = data;
      }
    );
  }

  addPage(){
    this.router.navigateByUrl("/car/edit");
  }
}
