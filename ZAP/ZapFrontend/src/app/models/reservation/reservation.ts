export class Reservation{
    public carId : number;
    public startDate: Date;
    public endDate: Date;

    public constructor(carId: number, startDate: Date, endDate: Date){
        this.carId = carId;
        this.startDate = startDate;
        this.endDate = endDate;
    }
}