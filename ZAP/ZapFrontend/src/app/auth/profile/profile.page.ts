import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastController } from '@ionic/angular';
import { first } from 'rxjs/operators';
import { Profile } from 'src/app/models/user/profile';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
})
export class ProfilePage implements OnInit {
  isLoading: boolean;
  profile: Profile;

  constructor(private authService: AuthService, private router: Router, private toastCtrl: ToastController) { }

  ngOnInit() {
    this.authService.getProfile().pipe(first()).subscribe(
      data => {
        this.profile = data;
      }
    )
  }

  ionViewWillEnter(){
    this.authService.getProfile().pipe(first()).subscribe(
      data => {
        this.profile = data;
      }
    )
  }
  
  onProfileEdit(profileForm: NgForm){
    this.isLoading = true;
    var model = new Profile(profileForm.value.firstName,
                            profileForm.value.lastName,
                            profileForm.value.email,
                            profileForm.value.address,
                            profileForm.value.phoneNumber
                            );
                          
    this.authService.editProfile(model).pipe(first()).subscribe(
      data =>{
        if(data==true){
          this.router.navigateByUrl('/auth/profile');
          this.toastCtrl.create({
            message: 'Profile updated',
            duration: 5000,
            position: 'bottom',
            color: 'success',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        
        this.isLoading = false;
      },
      error => {
        this.toastCtrl.create({
          message: 'Edit failed',
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
