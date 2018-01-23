module Tests

open Dora.DhcpProtocol
open System
open Xunit

[<Fact>]
let ``build Octet2 with 2 bytes`` () =
    buildOctet2 ([|1uy; 2uy|]) |> List.ofSeq
    |> function
    | [o1; o2] when o1 = 1uy && o2 = 2uy -> ()
    | lst -> failwithf "Incorrect octet sequence: %A" lst

[<Fact>]
let ``build Octet2 with less than 2 bytes`` () =
    buildOctet2 ([|1uy|]) |> List.ofSeq
    |> function
    | [o1; o2] when o1 = 1uy && o2 = 0uy -> ()
    | lst -> failwithf "Incorrect octet sequence: %A" lst

[<Fact>]
let ``build Octet2 with more than 2 bytes`` () =
    buildOctet2 ([|1uy; 2uy; 3uy|]) |> List.ofSeq
    |> function
    | [o1; o2] when o1 = 1uy && o2 = 2uy -> ()
    | lst -> failwithf "Incorrect octet sequence: %A" lst

[<Fact>]
let ``build Sname with less than 64 bytes`` () =
    buildSname "testServer" |> List.ofSeq
    |> function
    | arr when arr.Length <> 64 -> failwith "Result should be 64 octets."
    | _ -> ()
