import { Byte } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { NgForm, NumberValueAccessor } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastController } from '@ionic/angular';
import { first, take } from 'rxjs/operators';
import { Car } from 'src/app/models/car/car';
import { CarData } from 'src/app/models/car/carData';
import { CarEdit } from 'src/app/models/car/carEdit';
import { CarService } from '../car.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.page.html',
  styleUrls: ['./edit.page.scss'],
})
export class EditPage implements OnInit {

  private carData : CarData;
  private isLoading: boolean;
  private carId : number;
  private car = new CarEdit(null, null, null, null, null, null, null, null, null, null);
  private pictureBase64 : string;
  private isPictureLoaded = true;

  constructor(private router: Router, private carService : CarService, private toastCtrl: ToastController, private route: ActivatedRoute,) { }

  ngOnInit() {
    this.loadData();
  }

  ionViewWillEnter(){
    this.loadData();
  }

  loadData(){
    this.route.queryParams.subscribe(params => {
      if (params['carId']) {
        this.carId = parseInt(params['carId']);
      }
    });

    this.carService.getCarData().pipe(take(1)).subscribe(
      data => {
        this.carData = data;
      }
    );

    if(this.carId !== undefined){
      this.carService.getCar(this.carId).pipe(take(1)).subscribe(
        data => {
          this.car = data;
        }
      );
    }
  }

  onEdit(editForm: NgForm){
    this.isLoading = true;
    var model = new CarEdit(this.carId,
                            editForm.value.carType,
                            editForm.value.carClass,
                            editForm.value.carBrand,
                            editForm.value.licensePlate,
                            editForm.value.odometer,
                            editForm.value.manufactureDate,
                            editForm.value.price,
                            this.pictureBase64,
                            editForm.value.name);               
    
    this.carService.edit(model).pipe(first()).subscribe(
      data =>{
        if(data==true){
          if(this.carId !== undefined){
            this.router.navigateByUrl('/car/detail?carId=' + this.carId);
          }
          else{
            this.router.navigateByUrl('/car');
          }
          this.toastCtrl.create({
            message: this.carId !== undefined ? 'Car edited succesfully.' : 'Car added succesfully.',
            duration: 5000,
            position: 'bottom',
            color: 'success',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        else{
          this.toastCtrl.create({
            message: 'Something went wrong. Please try again.',
            duration: 5000,
            position: 'bottom',
            color: 'danger',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        this.isLoading = false;
      },
      error => {
        this.toastCtrl.create({
          message: 'Something went wrong. Please try again.',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
        
        this.isLoading = false;
      }
    )                        
  }

  onDocumentUpload(files) {
    const reader = new FileReader();

    reader.readAsDataURL(files.item(0));
    this.isPictureLoaded = false;

    reader.onload = () => {
      this.pictureBase64 = reader.result.toString().split('base64,').pop();
      this.isPictureLoaded = true;
    }
  }

}
