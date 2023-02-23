module fsharp_demo.CoreLib


// option<'T> ("maybe" monad)
module m1 =
    let none = None
    let some = Some "hello"

    let s = match some with None -> "none" | Some v -> v
    let s' = some |> Option.defaultValue "none"
    let s'' = some |> Option.defaultWith (fun () -> failwith "required")


    // chaining
    let getValue o =
        o
        |> Option.orElse (Some "try something")
        |> Option.orElse (Some "try something else")
        |> Option.defaultValue ":("



// linked list<'T>
module m2 =
    let xs = [1;2;3]
    let xs' = [for i in 1..10 do i]
    let xs'' = [
        let square x = x * x
        square 1
        square 2

        yield! [9;16;25]
    ]