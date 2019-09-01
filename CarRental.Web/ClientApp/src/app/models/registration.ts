export class Registration {
  id: number;
  registrationType: RegistrationType;
  dateTime: string;
  distanceMeter: number;

  constructor(registrationType: RegistrationType, date: string, distanceMeter: number) {
    this.registrationType = registrationType;
    this.dateTime = date;
    this.distanceMeter = distanceMeter;
  }
}

export enum RegistrationType {
  PickUp = 0,
  Return = 1
}
