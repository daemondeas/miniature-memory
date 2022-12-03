namespace Fsharp.Solutions

open Microsoft.FSharp.Collections

module Day03 =
    let rec FindDouble a b =
        match a with
        | x::xs ->
            if List.contains x b then
                x
            else
                FindDouble xs b
        | []    -> raise (System.ArgumentException("Lists must have a double"))
        
    let splitAndFind l =
        let a, b = List.splitAt ((List.length l) / 2) l
        FindDouble a b
        
    let firstPuzzle (input: int list list) =
        List.map splitAndFind input |> List.sum
        
    let rec FindTriple a b c =
        match a with
        | x::xs ->
            if List.contains x b && List.contains x c then
                x
            else
                FindTriple xs b c
        | []    -> raise (System.ArgumentException("Lists must have a triple"))
        
    let FindBadges l =
        List.splitInto ((List.length l) / 3) l
        |> List.map (fun ls -> FindTriple ls[0] ls[1] ls[2])
        
    let secondPuzzle (input: int list list) =
        FindBadges input |> List.sum