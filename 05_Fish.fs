module fsharp_demo.Fish

// example using fish operator / railway oriented programming

type User = { username: string; active: bool }
type LoggedInUser = { user: User; unread_messages: int }

[<AutoOpen>]
module UserDb =
    let private users = [
        { username = "guybrush"; active = false }
        { username = "elaine"; active = true }
    ]

    let find_user username =
        match users |> List.tryFind (fun u -> u.username = username) with
        | None -> Error "User was not found"
        | Some user -> Ok user

    let login_user (user:User) =
        if not user.active then Error "User is not active" else
        // actually log in user here
        Ok { user = user; unread_messages = 10 }

// verbose
let login_by_username username =
    match find_user username with
    | Ok user ->
        match login_user user with
        | Ok user -> Ok user
        | Error msg -> Error msg
    | Error msg -> Error msg

let login_by_username' username =
    match find_user username with
    | Ok user -> login_user user
    | Error error -> Error error

let login_by_username'' username =
    username |> find_user |> Result.bind login_user

let (>=>) f1 f2 = f1 >> Result.bind f2
let login_by_username''' = UserDb.find_user >=> UserDb.login_user

let isLoggedIn = login_by_username "elaine"

match isLoggedIn with
| Ok user -> printfn $"Welcome, {user.user.username}! You have {user.unread_messages} unread messages!"
| Error msg -> printfn $"An error occured: {msg}"