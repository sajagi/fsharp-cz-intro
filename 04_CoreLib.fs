module fsharp_demo.CoreLib
// option<'T> ("maybe" monad)
let none = None
let some = Some "hello"

let stringRepr1 = some |> function None -> "none" | Some v -> v
let stringRepr2_1 = some |> Option.defaultValue "none"
let stringRepr2_2 = some |> Option.defaultWith (fun () -> failwith "required")

// linked list<'T>
let xs_0 = [1;2;3]
let xs_1 = [for i in 1..10 do i]
let xs_2 = [
    let square x = x * x
    square 1
    square 2

    yield! [9;16;25]
]

// spousta funkcí v modulu List
let sum = xs_2 |> List.sum

// vypiš sudá čísla
let sumEven = xs_2 |> List.filter (fun x -> x % 2 = 0) |> List.sum

// vlastní funkce
module List =
  let private sqrtf = System.Math.Sqrt

  // :(
  // vytvoří zbytečně dva listy
  let intSqrt1 xs = xs |> List.map float |> List.map sqrtf |> List.map int

  // :(
  // v tom se vyznají jen C# devs
  let intSqrt2 xs = xs |> List.map (fun x -> int(sqrtf(float x)))

  // :)
  let intSqrt3 xs = xs |> List.map (fun x -> x |> float |> sqrtf |> int)

  // :)))
  // let (>>) f1 f2 = fun x -> f1 x |> f2
  let intSqrt4 = List.map (float >> sqrtf >> int)

let sqrted = xs_2 |> List.intSqrt4

// podobně seq (IEnumerable), array