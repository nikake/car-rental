export class Car {
  id: number;
  registrationNumber: string;
  status: Status;
  carType: CarType;
  baseDayPrice: number;
  baseKmPrice: number;
  distanceMeter: number;

  constructor(id: number, registrationNumber: string, status: Status, carType: CarType, baseDayPrice: number, baseKmPrice: number, distanceMeter: number) {
    this.id = id;
    this.registrationNumber = registrationNumber;
    this.status = status;
    this.carType = carType;
    this.baseDayPrice = baseDayPrice;
    this.baseKmPrice = baseKmPrice;
    this.distanceMeter = distanceMeter;
  }
}

export enum CarType {
  "Småbil" = 0,
  "Kombi" = 1,
  "Lastbil" = 2
}

export enum Status {
  "Tillgänglig" = 0, 
  "Otillgänglig" = 1
}
