import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth/auth.service';
import { FileTab } from './common/fileTab';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})

export class AppComponent {
  constructor(private authService : AuthService, private router : Router) {
  }

  public get fileTab() : typeof FileTab{
    return FileTab;
  }

  onClick(fileTab: FileTab){
    switch(fileTab){  
      case FileTab.Customers:
        this.router.navigateByUrl('/customer');
        break;

      case FileTab.AddAdmin:
        this.router.navigateByUrl('/auth/register-admin');
        break;

      case FileTab.Profile:
        this.router.navigateByUrl('/auth/profile');
        break;
      
      case FileTab.Cars:
        this.router.navigateByUrl('/car');
        break;  
    }
  }

  onLogout(){
    this.authService.logout();
    this.router.navigateByUrl('/auth');
  }
}
