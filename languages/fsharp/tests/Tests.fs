module Tests

open Xunit
open reactive

[<Fact>]
let ``Zero equals zero`` () =
    let zero = 0
    Assert.Equal(0, zero)

