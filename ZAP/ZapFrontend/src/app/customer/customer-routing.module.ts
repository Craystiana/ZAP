import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomerPage } from './customer.page';

const routes: Routes = [
  {
    path: '',
    component: CustomerPage
  },
  {
    path: 'detail',
    loadChildren: () => import('./detail/detail.module').then( m => m.DetailPageModule)
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerPageRoutingModule {}
