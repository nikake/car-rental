import { Component, OnInit, Directive, EventEmitter, Input, Output, QueryList, ViewChildren } from '@angular/core';
import { DatePipe } from '@angular/common';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { Car, CarType, Status } from '../../models/car';
import { Registration, RegistrationType } from '../../models/registration';
import { DataService } from '../../services/data.service';
import { Customer } from '../../models/customer';
import { Booking } from '../../models/booking';

@Component({
  selector: 'app-root',
  templateUrl: './rent-car.component.html',
  styleUrls: ['./rent-car.component.less']
})
export class RentCarComponent implements OnInit {

  constructor(private dataService: DataService, private modalService: NgbModal, private datePipe: DatePipe) { }

  public cars: Car[] = [];
  CarType: string[] = [CarType[0], CarType[1], CarType[2]];
  Status: string[] = [Status[0], Status[1]];
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number = this.cars.length;
  closeResult: string;
  selectedCar: Car;
  selectedDate: { year: number, month: number, day: number }
  selectedIdentityNumber: string;

  ngOnInit() {
    this.dataService.getRentalCars().subscribe((data) => {
      this.cars = data;
      this.collectionSize = this.cars.length;
      console.log(Status[this.cars[0].status]);
      console.log(CarType[this.cars[0].carType]);
    });
  }

  setSelectedCar(car: Car) {
    this.selectedCar = car;
  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      let car = this.selectedCar;
      let tempDate: Date = new Date();
      tempDate.setFullYear(this.selectedDate.year);
      tempDate.setMonth(this.selectedDate.month);
      tempDate.setDate(this.selectedDate.day);

      let date = this.datePipe.transform(tempDate, "yyyy-MM-dd'T'HH:mm:ss.0000000");

      let booking = new Booking(new Customer(this.selectedIdentityNumber), this.selectedCar, new Registration(RegistrationType.PickUp, date, this.selectedCar.distanceMeter), null);
      this.dataService.rentCar(booking).subscribe((data) => {
        this.selectedIdentityNumber = null;
        if (data) {
          car.status = Status.Otillgänglig;
          this.cars = this.cars.filter(function (obj: Car) {
            return obj.id !== car.id;
          });
        }
      });
    }, (reason) => {
        this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        this.selectedCar = null;
    });
  }

  get listCars(): Car[] {
    return this.cars
      .map((country, i) => ({ id: i + 1, ...country }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

}
