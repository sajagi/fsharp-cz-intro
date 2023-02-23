module fsharp_demo.Fish

// example using fish operator / railway oriented programming

type User = { username: string; active: bool }
type LoggedInUser = { user: User; unread_messages: int }

[<AutoOpen>]
module UserDb =
    // "databáze" uživatelů
    let private users = [
        { username = "guybrush"; active = false }
        { username = "elaine"; active = true }
    ]

    // najdi uživatele podle jeho jména, nebo vrať chybu
    let find_user username =
        match users |> List.tryFind (fun u -> u.username = username) with
        | None -> Error "User was not found"
        | Some user -> Ok user

    // zaloguj uživatele, nebo vrať chybu, pokud není aktivní
    let login_user (user:User) =
        if not user.active then Error "User is not active" else

        // actually log in user here

        Ok { user = user; unread_messages = 10 }

// zaloguj uživatele podle jeho jména; pokud neexistuje nebo není aktivní, vrať chybu
let login_by_username username =
    match find_user username with
    | Ok user ->
        match login_user user with
        | Ok user -> Ok user
        | Error msg -> Error msg
    | Error msg -> Error msg


// předchozí funkce zkráceně
let login_by_username' username =
    match find_user username with
    | Ok user -> login_user user
    | Error error -> Error error

// pomocí Result.bind
// 'bind f inp' evaluates to 'match inp with Error e -> Error e | Ok x -> f x'
let login_by_username'' username =
    username |> find_user |> Result.bind login_user

// bind se často zapisuje pomocí (>=>) operátoru (fish operator, Kleisli operator)
let (>=>) f1 f2 = f1 >> Result.bind f2

let login_by_username''' = UserDb.find_user >=> UserDb.login_user

match login_by_username''' "elaine" with
| Ok user -> printfn $"Welcome, {user.user.username}! You have {user.unread_messages} unread messages!"
| Error msg -> printfn $"An error occured: {msg}"