import { Car } from './car';
import { Customer } from './customer';
import { Registration } from './registration';

export class Booking {
  id: number;
  customer: Customer;
  car: Car;
  pickUpRegistration: Registration;
  returnRegistration: Registration;

  constructor(customer: Customer, car: Car, pickUpRegistration: Registration, returnRegistration: Registration) {
    this.customer = customer;
    this.car = car;
    this.pickUpRegistration = pickUpRegistration;
    this.returnRegistration = returnRegistration;
  }
}
