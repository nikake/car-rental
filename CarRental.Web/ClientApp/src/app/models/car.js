"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Car = /** @class */ (function () {
    function Car(id, registrationNumber, status, carType, baseDayPrice, baseKmPrice, distanceMeter) {
        this.id = id;
        this.registrationNumber = registrationNumber;
        this.status = status;
        this.carType = carType;
        this.baseDayPrice = baseDayPrice;
        this.baseKmPrice = baseKmPrice;
        this.distanceMeter = distanceMeter;
    }
    return Car;
}());
exports.Car = Car;
var CarType;
(function (CarType) {
    CarType[CarType["Sm\u00E5bil"] = 0] = "Sm\u00E5bil";
    CarType[CarType["Kombi"] = 1] = "Kombi";
    CarType[CarType["Lastbil"] = 2] = "Lastbil";
})(CarType = exports.CarType || (exports.CarType = {}));
var Status;
(function (Status) {
    Status[Status["Tillg\u00E4nglig"] = 0] = "Tillg\u00E4nglig";
    Status[Status["Otillg\u00E4nglig"] = 1] = "Otillg\u00E4nglig";
})(Status = exports.Status || (exports.Status = {}));
//# sourceMappingURL=car.js.map