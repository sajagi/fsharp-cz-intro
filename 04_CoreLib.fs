module fsharp_demo.CoreLib


// option<'T> ("maybe" monad)
module m1 =
    let none = None
    let some = Some "hello"

    let s = some |> function None -> "none" | Some v -> v
    let s' = some |> Option.defaultValue "none"
    let s'' = some |> Option.defaultWith (fun () -> failwith "required")



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

    // spousta funkcí v modulu List
    let sum = xs |> List.sum

    // sečti sudá čísla
    let sum_even = xs |> List.filter (fun x -> x % 2 = 0) |> List.sum

    // vlastní funkce
    module List =
      let private sqrtf = System.Math.Sqrt

      // :(
      // vytvoří zbytečně dva listy
      let intSqrt xs = xs |> List.map float |> List.map sqrtf |> List.map int

      // :(
      // v tom se vyznají jen C# devs
      let intSqrt' xs = xs |> List.map (fun x -> int(sqrtf(float x)))

      // :)
      let intSqrt'' xs = xs |> List.map (fun x -> x |> float |> sqrtf |> int)

      // :)))
      // let (>>) f1 f2 = fun x -> f1 x |> f2
      let intSqrt''' = List.map (float >> sqrtf >> int)

    let sqrted = xs |> List.intSqrt

    // podobně seq (IEnumerable), array