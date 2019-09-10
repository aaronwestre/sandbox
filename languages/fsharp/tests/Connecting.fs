module tests.Connecting

open Xunit
open System.Reactive
open System.Reactive.Subjects

[<Fact>]
let ``Basic connection`` () =
    let subject = new Subject<int>()
    let mutable observedValue = 0
    let observer = Observer.Create (fun nextValue -> observedValue <- nextValue)
    let subscription = subject.Subscribe observer
    subject.OnNext 999
    Assert.Equal(999, observedValue)