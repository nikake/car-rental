import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RentCarComponent } from './content/rent-car/rent-car.component';
import { ReturnCarComponent } from './content/return-car/return-car.component';

const routes: Routes = [
  {
    path: '',
    component: RentCarComponent,
    pathMatch: 'full'
  },
  {
    path: 'retur',
    component: ReturnCarComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
