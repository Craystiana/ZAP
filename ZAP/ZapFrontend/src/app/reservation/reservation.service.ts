import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { API_URL, RESERVATION_ADD_URL, RESERVATION_CAR_LIST, RESERVATION_USER_LIST } from 'src/environments/environment';
import { Reservation } from '../models/reservation/reservation';
import { ReservationDetails } from '../models/reservation/reservationDetails';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private http: HttpClient) { }

  public getCarReservations(carId: number) {
    return this.http.get(API_URL + RESERVATION_CAR_LIST + carId).pipe(
      map((data : ReservationDetails[]) => {
        return data;
      })
    )
  }

  public getUserReservations(userId: number) {
    return this.http.get(API_URL + RESERVATION_USER_LIST + userId).pipe(
      map((data : ReservationDetails[]) => {
        return data;
      })
    )
  }

  public addReservation(model: Reservation){
    return this.http.post(API_URL + RESERVATION_ADD_URL, model).pipe(
      map((data : boolean) => {
        return data;
      })
    )
  }
}
