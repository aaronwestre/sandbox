open System
open System.Threading
open Joystick

[<EntryPoint>]
let main argv =
    let mutable running = true
    let readKeyboardInput () =
        if Console.KeyAvailable then
            match Console.ReadKey().Key with
            | ConsoleKey.Escape -> running <- false
            | _ -> ()
        ()
    use raspberryPi = new RaspberryPiGpio()
    while running do
        let controllerState = raspberryPi.readState()
        match controllerState.JoystickState with
        | Up -> Console.WriteLine "Up"
        | Down -> Console.WriteLine "Down"
        | Left -> Console.WriteLine "Left"
        | Right -> Console.WriteLine "Right"
        | Neutral -> ()
        readKeyboardInput()
        Thread.Sleep 20
    0
