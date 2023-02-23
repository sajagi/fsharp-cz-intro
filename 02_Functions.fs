module fsharp_demo.Functions

open System

// funkce
module m1 =
    let add x y = x + y

    // volání funkce bez ()
    let res = add 5 6

    // funkce bez parametrů / bez návratové hodnoty používá unit ()
    let hello () = printfn "hello!"

    // lambda funkce
    let add' = fun x y -> x + y

    let add'' =
        let f x y = x + y
        f


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
    let f() =
        let sin, cos, tan = Math.Sin, Math.Cos, Math.Tan

        // :(
        let calc x = tan (cos (sin x))

        let pipe x f = f x

        let sin x = pipe x sin
        let cos x = pipe x cos
        let tan x = pipe x tan

        // moc jsme si nepomohli
        let calc x = pipe (pipe (pipe x sin) cos) tan

        // ale binární operátory jsou infixové! zkusme to tedy přepsat
        let (|>) x f = f x

        // takže...
        let sin x = (|>) x sin

        // ...se dá ekvivaltentně zapsat jako
        let sin x = x |> sin

        // tedy původní zápis můžeme upravit na
        let calc x = (|>) ((|>) ((|>) x sin) cos) tan
        let calc x = (|>) ((|>) (x |> sin) cos) tan
        let calc x = (|>) ((x |> sin) |> cos) tan
        let calc x = ((x |> sin) |> cos) |> tan

        // po odstranění závorek už to vypadá hezky!
        let calc x = x |> sin |> cos |> tan

        // dobrá zpráva - |> je zabudovaný operátor!



        // :)))
        // >> composition operátor
        // let (>>) f1 f2 = fun x -> x |> f1 |> f2
        let calc = sin >> cos >> tan
        ()

module m4 =
    open Microsoft.Extensions.FileSystemGlobbing

    // vlastní operátory
    let (@) x y = System.IO.Path.Combine(x, y)
    let path = Environment.CurrentDirectory @ "foo.txt"


    // disclaimer - nezkoušeno
    let (!!) (glob:string) = Matcher().AddInclude(glob)
    let (++) (files:Matcher) glob = files.AddInclude(glob)
    let (--) (files:Matcher) glob = files.AddExclude(glob)

    let files = !! "**/*.fs" ++ "**/*.fsx" -- "**/packages/**"