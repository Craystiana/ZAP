import { Customer } from "../customer/customer";

export class Profile extends Customer{
    public address: string;
    public phoneNumber: string;

    public constructor(firstName: string, lastName: string, email:string, address: string, phoneNumber:string){
        super();
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.address = address;
        this.phoneNumber = phoneNumber;
    }
}