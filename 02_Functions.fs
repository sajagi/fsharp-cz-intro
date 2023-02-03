module fsharp_demo.Functions

open System

// 2. FUNKCE

// každá funkce má alespoň jeden parametr a má vždy návratovou hodnotu...
let f_int x y = x + y

// ...i kdyby to měl být unit ()
let f_unit () = printfn "hello!"

// volání funkce bez () (() je unit / tuple)
let x1 = f_int 5 6

// lambda funkce
let f_lambda = fun x y -> x + y


// -----------------------------------
// ---- <BRAINFUCK BEGINS> ----

// high-order funkce jsou samozřejmost
let double x = 2*x
let print_result f x = printfn $"%i{f x}"   // tuple s jedním prvkem je totéž co jeden prvek
print_result double 5

// parciální aplikace / currying
let f x y = x + y
let add1 = f 1

// stejné jako bych napsal
// let add1 y = f 1 y

// :(
let add1ThenDoubleThenPrint0 x =
    printfn $"%i{double (add1 x)}"

// :)
// |> operátor
// let (|>) x f = f x
let add1ThenDoubleThenPrint1 x = x |> add1 |> double |> printfn "%i"

// :)))
// >> operátor
// let (>>) f1 f2 = fun x -> f1 x |> f2
let add1ThenDoubleThenPrint2 = add1 >> double >> printfn "%i"

// vlastní operátory
let (@) x y = System.IO.Path.Combine(x, y)
let path = Environment.CurrentDirectory @ "foo.txt"

// go wild
let (|<><><>|) x y = x - y
let z = 10 |<><><>| 2