module fsharp_demo.Matching
open System
open System.Text.RegularExpressions
open Values

// 3. MATCHING

// "základní" pattern matching
let dispList xs =
    match xs with
    | [] -> "prázdné :("
    | [1] -> "jednička!"
    | [_] -> "něco jiného"
    | [x;y] when x = y -> "dva stejné"
    | 1::_ -> "první je jednička a pak ještě zbytek"
    | _ -> "něco jiného"
    |> printfn "%s"

// matching DU - kontrola prekladacem
let dispWeather1 w =
    match w with
    | Cloudy -> ":|"
    | Sunny -> ":)"
    | Rainy -> ":("
    |> printfn "%s"

let dispWeather2 =
    function Cloudy -> ":|" | Sunny -> ":)" | Rainy -> ":("
    >> printfn "%s"

// vlastní pattern matching
let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

let matchVersion v =
    match v with
    | Regex @"[\d]+" [major] -> Version(int major, 0, 0)
    | Regex @"[\d]+\.[\d]+" [major; minor] -> Version(int major, int minor, 0)
    | s -> Version(s)