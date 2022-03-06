import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'auth',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then( m => m.AuthPageModule)
  },
  {
    path: 'customer',
    loadChildren: () => import('./customer/customer.module').then( m => m.CustomerPageModule),
    canLoad: [AuthGuard]
  },
  {
    path: 'car',
    loadChildren: () => import('./car/car.module').then( m => m.CarPageModule),
    canLoad: [AuthGuard]
  },
  {
    path: "**",
    redirectTo: 'auth'
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
