module reactivex.fsharp.Tests

open Xunit
open System
open System.Reactive
open System.Reactive.Subjects

type MachineNode =
    {
        Name : string;
        Worker : int -> int;
        Input : IObserver<int>;
        Output : IObservable<int>
    }

let connect (upstream : MachineNode) (downstream : MachineNode) = upstream.Output.Subscribe downstream.Input

let createMachine (name : string) =
    let output = new Subject<int>()
    let worker (number : int) = number
    let receiver (number : int) =
        worker number
        |> output.OnNext
    let input = Observer.Create (fun number -> receiver number)
    {Name = name; Worker = worker; Input = input; Output = output}

[<Fact>]
let ``Chaining subjects and observers`` () =
    let a = createMachine "Instigator"
    let b = createMachine "Relayer"
    let c = createMachine "Receiver"
    let wire1 = connect a b
    let wire2 = connect b c
    let mutable observed : seq<int> = Seq.empty
    let observe value = observed <- Seq.append observed (Seq.singleton value)
    let observer = Observer.Create (fun nextValue -> observe nextValue)
    let wire3 = c.Output.Subscribe observer
    let numbers = seq {1..20}
    Seq.iter (fun number -> a.Input.OnNext number) numbers
    Assert.Equal(Seq.length numbers, Seq.length observed);
