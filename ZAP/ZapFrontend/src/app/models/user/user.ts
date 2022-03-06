import { UserRole } from "../../common/userRole";

export class User {
    private _firstName: string;
    private _lastName: string;
    private _role: UserRole;
    private _token: string;

    constructor(JSON: any){
        this._firstName = JSON.FirstName;
        this._lastName = JSON.LastName;
        this._role = JSON.role;
    }

    get role(){
        return this._role;
    }

    get firstName(){
        return this._firstName;
    }

    get lastName(){
        return this._lastName;
    }

    get token(){
        return this._token;
    }
}