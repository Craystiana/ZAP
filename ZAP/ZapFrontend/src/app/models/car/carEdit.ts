import { Byte } from "@angular/compiler/src/util";

export class CarEdit{
    public carId : number;
    public type : number;
    public class : number;
    public brand : number;
    public licensePlate : string;
    public odometer : number;
    public manufactureDate: Date;
    public price : number;
    public picture : string;
    public name : string;

    public constructor(carId: number,
                       carType: number, 
                       carClass: number, 
                       carBrand : number, 
                       licensePlate : string, 
                       odometer : number, 
                       manufactureDate : Date, 
                       price : number, 
                       photo : string, 
                       name : string) {
        this.carId = carId;                   
        this.type = carType;
        this.class = carClass;
        this.brand = carBrand;
        this.licensePlate = licensePlate;
        this.odometer = odometer;
        this.manufactureDate = manufactureDate;
        this.price = price;
        this.picture = photo;
        this.name = name;
    }
}