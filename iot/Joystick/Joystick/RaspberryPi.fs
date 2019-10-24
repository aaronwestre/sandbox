namespace Joystick

open System
open System.Device.Gpio

type PinPurpose =
    | Power3Volts
    | Power5Volts
    | Ground
    | InputOutput
    | Reserved
    | UartTransmit
    | UartReceive
    | I2C
    | SPI0
    | SPI1

type GpioPin =
    {
        DisplayName : string;
        PhysicalPinNumber : int;
        LogicalPinNumber : int;
        Purposes : PinPurpose list
    }

type GpioPin with
    static member initialize (controller : GpioController) pin =
        controller.OpenPin (pin.LogicalPinNumber, PinMode.Input)
    static member read (controller : GpioController) pin =
        if (controller.Read pin.LogicalPinNumber) = PinValue.High then true else false

type ControllerPins =
    {
        Up : GpioPin;
        Down : GpioPin;
        Left : GpioPin;
        Right : GpioPin;
    }

type JoystickState =
    | Up
    | Down
    | Left
    | Right
    | Neutral

type ControllerState =
    {
        JoystickState : JoystickState
    }

type ControllerState with
    static member read (gpioController : GpioController) (controllerPins : ControllerPins) =
        let readPin = GpioPin.read gpioController
        let joystickState =
            if (readPin controllerPins.Up) then Up else
            if (readPin controllerPins.Down) then Down else 
            if (readPin controllerPins.Left) then Left else
            if (readPin controllerPins.Right) then Right
            else Neutral
        {JoystickState = joystickState}

module RaspberryPi =
    let pins =
        [
            {DisplayName = "3.3V Power"; PhysicalPinNumber = 1; LogicalPinNumber = 0; Purposes = [Power3Volts]};
            {DisplayName = "5V Power"; PhysicalPinNumber = 2; LogicalPinNumber = 0; Purposes = [Power5Volts]};
            {DisplayName = "GPIO 2"; PhysicalPinNumber = 3; LogicalPinNumber = 2; Purposes = [InputOutput; I2C]};
            {DisplayName = "5V Power"; PhysicalPinNumber = 4; LogicalPinNumber = 0; Purposes = [Power5Volts]};
            {DisplayName = "GPIO 3"; PhysicalPinNumber = 5; LogicalPinNumber = 3; Purposes = [InputOutput; I2C]};
            {DisplayName = "Ground"; PhysicalPinNumber = 6; LogicalPinNumber = 0; Purposes = [Ground]};
            {DisplayName = "GPIO 4"; PhysicalPinNumber = 7; LogicalPinNumber = 4; Purposes = [InputOutput]};
            {DisplayName = "UART Transmit"; PhysicalPinNumber = 8; LogicalPinNumber = 0; Purposes = [UartTransmit]};
            {DisplayName = "Ground"; PhysicalPinNumber = 9; LogicalPinNumber = 0; Purposes = [Ground]};
            {DisplayName = "UART Receive"; PhysicalPinNumber = 10; LogicalPinNumber = 0; Purposes = [UartReceive]};
            {DisplayName = "GPIO 17"; PhysicalPinNumber = 11; LogicalPinNumber = 17; Purposes = [InputOutput]};
            {DisplayName = "GPIO 18"; PhysicalPinNumber = 12; LogicalPinNumber = 18; Purposes = [InputOutput]};
            {DisplayName = "GPIO 27"; PhysicalPinNumber = 13; LogicalPinNumber = 27; Purposes = [InputOutput]};
            {DisplayName = "Ground"; PhysicalPinNumber = 14; LogicalPinNumber = 0; Purposes = [Ground]};
            {DisplayName = "GPIO 22"; PhysicalPinNumber = 15; LogicalPinNumber = 22; Purposes = [InputOutput]};
            {DisplayName = "GPIO 23"; PhysicalPinNumber = 16; LogicalPinNumber = 23; Purposes = [InputOutput]};
            {DisplayName = "3.3V Power"; PhysicalPinNumber = 17; LogicalPinNumber = 0; Purposes = [Power3Volts]};
            {DisplayName = "GPIO 24"; PhysicalPinNumber = 18; LogicalPinNumber = 24; Purposes = [InputOutput]};
            {DisplayName = "GPIO 10"; PhysicalPinNumber = 19; LogicalPinNumber = 10; Purposes = [InputOutput; SPI0]};
            {DisplayName = "Ground"; PhysicalPinNumber = 20; LogicalPinNumber = 0; Purposes = [Ground]};
            {DisplayName = "GPIO 9"; PhysicalPinNumber = 21; LogicalPinNumber = 9; Purposes = [InputOutput; SPI0]};
            {DisplayName = "GPIO 25"; PhysicalPinNumber = 22; LogicalPinNumber = 25; Purposes = [InputOutput]};
            {DisplayName = "GPIO 11"; PhysicalPinNumber = 23; LogicalPinNumber = 11; Purposes = [InputOutput; SPI0]};
            {DisplayName = "GPIO 8"; PhysicalPinNumber = 24; LogicalPinNumber = 8; Purposes = [InputOutput; SPI0]};
            {DisplayName = "Ground"; PhysicalPinNumber = 25; LogicalPinNumber = 0; Purposes = [Ground]};
            {DisplayName = "GPIO 7"; PhysicalPinNumber = 26; LogicalPinNumber = 7; Purposes = [InputOutput; SPI0]};
            {DisplayName = "Reserved"; PhysicalPinNumber = 27; LogicalPinNumber = 0; Purposes = [Reserved]};
            {DisplayName = "Reserved"; PhysicalPinNumber = 28; LogicalPinNumber = 0; Purposes = [Reserved]};
            {DisplayName = "GPIO 5"; PhysicalPinNumber = 29; LogicalPinNumber = 5; Purposes = [InputOutput]};
            {DisplayName = "Ground"; PhysicalPinNumber = 30; LogicalPinNumber = 0; Purposes = [Ground]};
            {DisplayName = "GPIO 6"; PhysicalPinNumber = 31; LogicalPinNumber = 6; Purposes = [InputOutput]};
            {DisplayName = "GPIO 12"; PhysicalPinNumber = 32; LogicalPinNumber = 12; Purposes = [InputOutput]};
            {DisplayName = "GPIO 13"; PhysicalPinNumber = 33; LogicalPinNumber = 13; Purposes = [InputOutput]};
            {DisplayName = "Ground"; PhysicalPinNumber = 34; LogicalPinNumber = 0; Purposes = [Ground]};
            {DisplayName = "GPIO 19"; PhysicalPinNumber = 35; LogicalPinNumber = 19; Purposes = [InputOutput; SPI1]};
            {DisplayName = "GPIO 16"; PhysicalPinNumber = 36; LogicalPinNumber = 16; Purposes = [InputOutput; SPI1]};
            {DisplayName = "GPIO 26"; PhysicalPinNumber = 37; LogicalPinNumber = 26; Purposes = [InputOutput]};
            {DisplayName = "GPIO 20"; PhysicalPinNumber = 38; LogicalPinNumber = 20; Purposes = [InputOutput; SPI1]};
            {DisplayName = "Ground"; PhysicalPinNumber = 39; LogicalPinNumber = 0; Purposes = [Ground]};
            {DisplayName = "GPIO 21"; PhysicalPinNumber = 40; LogicalPinNumber = 21; Purposes = [InputOutput; SPI1]};
        ]
    let listPins purpose =
        List.filter (fun pin -> List.contains purpose pin.Purposes) pins
    let findInputPin pinNumber =
        List.find (fun pin -> pin.LogicalPinNumber = pinNumber && (List.contains InputOutput pin.Purposes)) pins

type RaspberryPiGpio() =
    let gpioController = new GpioController()
    let controllerPins =
        {
            Up = RaspberryPi.findInputPin 4
            Down = RaspberryPi.findInputPin 17
            Left = RaspberryPi.findInputPin 27
            Right = RaspberryPi.findInputPin 22
        }
    do
        GpioPin.initialize gpioController controllerPins.Up
        GpioPin.initialize gpioController controllerPins.Down
        GpioPin.initialize gpioController controllerPins.Left
        GpioPin.initialize gpioController controllerPins.Right
    member this.readState () =
        ControllerState.read gpioController controllerPins
    interface IDisposable with
        member this.Dispose() = gpioController.Dispose()
