import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { DatePipe, registerLocaleData } from '@angular/common';
import localeSe from '@angular/common/locales/se-SE';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RentCarComponent } from './content/rent-car/rent-car.component';
import { DataService } from './services/data.service';
import { ReturnCarComponent } from './content/return-car/return-car.component';

registerLocaleData(localeSe, 'se-SE')

@NgModule({
  declarations: [
    AppComponent,
    RentCarComponent,
    ReturnCarComponent 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule
  ],
  providers: [DataService, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
