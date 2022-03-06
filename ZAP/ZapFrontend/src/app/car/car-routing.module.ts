import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CarPage } from './car.page';

const routes: Routes = [
  {
    path: '',
    component: CarPage
  },
  {
    path: 'detail',
    loadChildren: () => import('./detail/detail.module').then( m => m.DetailPageModule)
  },
  {
    path: 'edit',
    loadChildren: () => import('./edit/edit.module').then( m => m.EditPageModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CarPageRoutingModule {}
