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
  templateUrl: './return-car.component.html',
  styleUrls: ['./return-car.component.less']
})
export class ReturnCarComponent implements OnInit {

  constructor(private dataService: DataService, private modalService: NgbModal, private datePipe: DatePipe) { }

  public bookings: Booking[] = [];
  CarType: string[] = [CarType[0], CarType[1], CarType[2]];
  Status: string[] = [Status[0], Status[1]];
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number = this.bookings.length;
  closeResult: string;
  selectedBooking: Booking;
  selectedDate: { year: number, month: number, day: number }
  selectedIdentityNumber: string;
  selectedBookingPrice: number;
  distanceMeter: number;
  time = { hour: 12, minute: 0 };

  ngOnInit() {
    this.dataService.getBookings().subscribe((data) => {
      this.bookings = data;
    });
  }

  setSelectedBooking(booking: Booking) {
    this.selectedBooking = booking;
  }

  open(content, priceContent) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      let tempDate: Date = new Date();
      tempDate.setFullYear(this.selectedDate.year);
      tempDate.setMonth(this.selectedDate.month);
      tempDate.setDate(this.selectedDate.day);
      let date = this.datePipe.transform(tempDate, "yyyy-MM-dd'T'HH:mm:ss.0000000");

      this.selectedBooking.returnRegistration = new Registration(RegistrationType.Return, date, this.selectedBooking.car.distanceMeter);
      this.dataService.returnCar(this.selectedBooking).subscribe((data) => {
        this.selectedDate = { year: 0, month: 0, day: 0 };
        this.selectedIdentityNumber = null;
        if (data) {
          let self = this;
          this.bookings = this.bookings.filter(function (obj: Booking) {
            return obj.id !== self.selectedBooking.id;
          });
          this.dataService.getPrice(this.selectedBooking).subscribe((data) => {
            this.selectedBookingPrice = data;
            this.modalService.open(priceContent, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {

            }, (reason) => {
              this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
              this.selectedBooking = null;
            });
          });
        }
      });
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
      this.selectedBooking = null;
    });
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
