import { Byte } from "@angular/compiler/src/util";

export class Car {
    public carId: number;
    public type : string;
    public class : string;
    public brand : string;
    public licensePlate : string;
    public odometer : string;
    public manufactureDate: string;
    public price : number;
    public picture : Byte[];
    public name : string;
}