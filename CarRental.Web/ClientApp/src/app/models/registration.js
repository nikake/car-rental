"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Registration = /** @class */ (function () {
    function Registration(registrationType, date, distanceMeter) {
        this.registrationType = registrationType;
        this.dateTime = date;
        this.distanceMeter = distanceMeter;
    }
    return Registration;
}());
exports.Registration = Registration;
var RegistrationType;
(function (RegistrationType) {
    RegistrationType[RegistrationType["PickUp"] = 0] = "PickUp";
    RegistrationType[RegistrationType["Return"] = 1] = "Return";
})(RegistrationType = exports.RegistrationType || (exports.RegistrationType = {}));
//# sourceMappingURL=registration.js.map