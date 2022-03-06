export class Register {
    public firstName: string;
    public lastName: string;
    public email: string;
    public password: string;
    public phone: string;
    public address: string;

    public constructor(firstName: string, lastName: string, email: string, password: string, phone: string, address: string){
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.phone = phone;
        this.address = address;
    }

}