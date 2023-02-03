module fsharp_demo.Values

module m1 =
    // přiřazení hodnoty k identifikátoru (ale stejně tomu říkám "proměnná"), imutabilita
    let (x:int) = 5

    // F# nepoužívá null ani pro referenční typy
    // let (x:string) = null

    // není třeba specifikovat typ (ani se to většinou nedělá), IDE napoví
    let b = true

    // imutabilní, ale dá se předefinovat (uvnitř funkce)
    let f() =
        let x = 5
        let x = "hello"
        printfn $"{x}" // hello

    let ``😀 i toto je identifikátor`` = ()

// arrays, lists, tuples
module m2 =
    // linked list
    let list = [1;2;3]

    // pole
    let array = [|1;2;3|]

    // tuple
    let tuple = (1,2,"hello")

    // IEnumerable<'T>
    let sequence = seq { 1;2;3 }

    // lazy, map, ...


// discriminated union ("sum type")
module m3 =

    // tohle není C# enum
    type Weather = Sunny | Cloudy | Rainy
    let w = Cloudy

    // i generické, s hodnotami
    type Result<'TValue, 'TError> =
    | Value of 'TValue
    | Error of 'TError

    let okValue = Value 5
    let errorValue = Error "oh no"


// record
module m4 =
    type FullName = { first: string; last: string }
    let joel = { first = "Joel"; last = "Coen" }
    let ethan = { joel with first = "Ethan" }


// units of measure
module m5 =
    [<Measure>] type czk
    [<Measure>] type eur

    let price1 = 120<czk>
    let price2 = 10<eur>

    // eur/czk
    let divided = price2 / price1

    // fail
    // let sum = price1 + price2