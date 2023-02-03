module fsharp_demo.Fish

// example using fish operator / railway oriented programming

type User = { username: string; active: bool }
type LoggedInUser = { user: User; unreadMessages: int }

[<AutoOpen>]
module UserDb =
    let private users = [
        { username = "guybrush"; active = false }
        { username = "elaine"; active = true }
    ]

    let findUser username =
        match users |> List.tryFind (fun u -> u.username = username) with
        | None -> Error "User was not found"
        | Some user -> Ok user

    let loginUser (user:User) =
        if not user.active then Error "User is not active" else
        // actually log in user here
        Ok { user = user; unreadMessages = 10 }

// verbose
let loginByUsername1 username =
    match findUser username with
    | Ok user ->
        match loginUser user with
        | Ok user -> Ok user
        | Error msg -> Error msg
    | Error msg -> Error msg

let loginByUsername2 username =
    match findUser username with
    | Ok user -> loginUser user
    | Error error -> Error error

let loginByUsername3 username =
    username |> findUser |> Result.bind loginUser

let (>=>) f1 f2 = f1 >> Result.bind f2
let loginByUsername4 = UserDb.findUser >=> UserDb.loginUser

let isLoggedIn = loginByUsername4 "elaine"

match isLoggedIn with
| Ok user -> printfn $"Welcome, {user.user.username}! You have {user.unreadMessages} unread messages!"
| Error msg -> printfn $"An error occured: {msg}"