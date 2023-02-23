module fsharp_demo.Matching
open System.Text.RegularExpressions

// C# switch statement
// kompilátor si nebude stěžovat, že nejsou všechny možnosti zahrnuty

// switch(weatcher) {
//   case Weather.Sunny:
//     Console.WriteLine(":)");
//     break;
//   case Weather.Cloudy:
//     Console.WriteLine(":(");
//     break;
// }

// C# switch expression
// neumí bloky kódu, pouze výrazy :(

// string repr = weather switch { Weather.Sunny => ":)", Weather.Cloudy => ":(", _ => "?"; }


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