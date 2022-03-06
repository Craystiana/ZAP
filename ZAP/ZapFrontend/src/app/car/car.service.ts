import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { API_URL, CAR_DATA_URL, CAR_DETAIL_URL, CAR_EDIT_URL, CAR_DELETE_URL, CAR_LIST_URL, RESERVATION_ADD_URL } from 'src/environments/environment';
import { Car } from '../models/car/car';
import { CarData } from '../models/car/carData';
import { CarEdit } from '../models/car/carEdit';
import { CarQuery } from '../models/car/carQuery';
import { Reservation } from '../models/reservation/reservation';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  public getCarData() {
    return this.http.get(API_URL + CAR_DATA_URL).pipe(
      map((data : CarData) => {
        return data;
      })
    )
  }

  public getCarDetails(carId : number) {
    return this.http.get(API_URL + CAR_DETAIL_URL + carId).pipe(
      map((data : Car) => {
        return data;
      })
    )
  }

  public getCar(carId : number) {
    return this.http.get(API_URL + CAR_EDIT_URL + "?carId=" + carId).pipe(
      map((data : CarEdit) => {
        return data;
      })
    )
  }

  public edit(model: CarEdit){
    return this.http.post(API_URL + CAR_EDIT_URL, model).pipe(
      map((result: boolean) =>{
        return result;
      })
    );
  }

  public delete(carId: number){
    debugger;
    return this.http.post(API_URL + CAR_DELETE_URL + carId, null).pipe(
      map((data : boolean) =>{
        return data;
      })
    );
  }

  public getCarList(model: CarQuery){
    return this.http.post(API_URL + CAR_LIST_URL, model).pipe(
      map((result: Car[]) =>{
        return result;
      })
    );
  }
}
