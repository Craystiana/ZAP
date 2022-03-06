import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastController } from '@ionic/angular';
import { AuthService } from '../auth.service';
import { first } from 'rxjs/operators';
import { Register } from 'src/app/models/user/register';

@Component({
  selector: 'app-register-admin',
  templateUrl: './register-admin.page.html',
  styleUrls: ['./register-admin.page.scss'],
})
export class RegisterAdminPage implements OnInit {

  public isLoading: boolean;
  constructor(private authService: AuthService, private router: Router, private toastCtrl: ToastController) { }

  ngOnInit() {
  }

  onRegisterAdmin(registerForm: NgForm){
    this.isLoading = true;
    var model = new Register(registerForm.value.firstName,
                             registerForm.value.lastName,
                             registerForm.value.email,
                             registerForm.value.password,
                             null,
                             null);
                          
    this.authService.registerAdmin(model).pipe(first()).subscribe(
      data =>{
        if(data==true){
          this.router.navigateByUrl('/car');
          this.toastCtrl.create({
            message: 'Admin added succesfully',
            duration: 5000,
            position: 'bottom',
            color: 'success',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        else{
          this.toastCtrl.create({
            message: 'An error occured while adding the admin',
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
          message: 'An error occured while adding the admin',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());
        
        this.isLoading = false;
      }
    )
    
  }

}
