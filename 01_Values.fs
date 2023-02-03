module fsharp_demo.Values

// 1. DATA

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


// -----------------------------------
// arrays, lists, tuples
let array = [|1;2;3|]
let tuple = (1,2,"hello")

// linked list
let list = [1;2;3]


// -----------------------------------
// discriminated union (sum type)
// tohle není enum
type Weather = Sunny | Cloudy | Rainy
let w = Cloudy

// i generické, s hodnotami
type Result<'TValue, 'TError> =
| Value of 'TValue
| Error of 'TError

let okValue = Value 5
let errorValue = Error "oh no"


// -----------------------------------
// record
type FullName = { First: string; Last: string }
let name = { First = "Guybrush"; Last = "Threepwood" }
let elaine = { name with First = "Elaine" }


// -----------------------------------
// units of measure
[<Measure>] type czk
[<Measure>] type eur

let price1 = 120<czk>
let price2 = 10<eur>

// eur/czk
let divided = price2 / price1

// fail
// let sum = price1 + price2