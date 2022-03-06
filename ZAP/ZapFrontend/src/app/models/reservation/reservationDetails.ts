import { Reservation } from "./reservation";

export class ReservationDetails extends Reservation{
    public reservationId : number;
    public reservationTypeId : number;
    public carName: string;
    public userId: number;
    public userName: string;
    public incident: boolean;
}