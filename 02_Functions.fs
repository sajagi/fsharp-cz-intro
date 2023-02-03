module fsharp_demo.Functions

open System

// funkce
module m1 =

    // každá funkce má alespoň jeden parametr a má vždy návratovou hodnotu...
    let add x y = x + y

    // ...i kdyby to měl být unit ()
    let hello () = printfn "hello!"

    // volání funkce bez () (() je unit / tuple)
    let res = add 5 6

    // lambda funkce
    let add' = fun x y -> x + y



// higher-order funkce
module m2 =
    let nums = [1;2;3]
    let square x = x*x
    let squared_nums = List.map square nums

    // parciální aplikace / currying - kuk na signaturu
    let add x y = x + y
    let add20 x = add 20 x
    let add20' = add 20

    let res = add20 10

// operátory |> a >>
module m3 =
    let sin, cos, tan = Math.Sin, Math.Cos, Math.Tan
    // :(
    let calc x = tan (cos (sin x))

    // :)
    // |> pipe operátor
    // let (|>) x f = f x
    let calc' x = x |> sin |> cos |> tan

    // :)))
    // >> composition operátor
    // let (>>) f1 f2 = fun x -> f1 x |> f2
    let calc'' = sin >> cos >> tan

module m4 =
    // vlastní operátory
    let (@) x y = System.IO.Path.Combine(x, y)
    let path = Environment.CurrentDirectory @ "foo.txt"

    // go wild
    let (|<><><>|) x y = $"{x} <><><> {y}"
    let hello_world = "hello" |<><><>| "world"