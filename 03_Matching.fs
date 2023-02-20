module fsharp_demo.Matching
open System
open System.Text.RegularExpressions

// C# switch statement example
// switch(e) {
//  case Enum.Foo: VoidFunkce("Foo"); break;
//  case Enum.Bar: VoidFunkce("Bar"); break;
// }

// C# switch expression example
// var x = s switch { "a" => 1, "b" => 2, _ => 0; }


// list matching
module m1 =
    let list_description xs =
        match xs with
        | [] -> "prázdné :("
        | [1] -> "jednička!"
        | [_] -> "něco jiného"
        | [x;y] when x = y -> "dva stejné"
        | 1::_ -> "první je jednička a pak ještě zbytek"
        | _ -> "něco jiného"

// DU matching
module m2 =
    type Weather = Sunny | Cloudy | Rainy

    let weather_icon w =
        match w with
        | Cloudy -> ":|"
        | Sunny -> ":)"
        | Rainy -> ":("

    let weather_icon' = function Cloudy -> ":|" | Sunny -> ":)" | Rainy -> ":("

// vlastní pattern matching
module m3 =
    let (|Regex|_|) pattern input =
        let m = Regex.Match(input, pattern)
        if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
        else None

    let parse_net s =
        // disclaimer: nezkoušeno :)
        match s with
        | Regex @"net(\d)\.?(\d)" [major; minor] -> $".NET Framework {major}.{minor}"
        | Regex @"net(\d)\.?(\d)\.?(\d)" [major; minor; build] -> $".NET Framework {major}.{minor}.{build}"
        | Regex @"netcoreapp(\d)\.(\d)" [major; minor] -> $".NET Core {major}.{minor}"
        | _ -> "nějaký jiný"