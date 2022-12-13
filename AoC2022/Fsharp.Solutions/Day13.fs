namespace Fsharp.Solutions

open System

module Day13 =
    type Listish = { elements: Choice<int, Listish> list }
    
    let wrapInt i = { elements = [Choice1Of2 i] }
    
    let inOrder firstNumber secondNumber =
        if firstNumber = secondNumber then
            None
        else
            Some (firstNumber < secondNumber)
    
    let rec isInOrder (firstPacket: Choice<int, Listish> list) (secondPacket: Choice<int, Listish> list) =
        match firstPacket with
        | x::xs ->
            match secondPacket with
            | y::ys ->
                match x with
                | Choice1Of2 i ->
                    match y with
                    | Choice1Of2 j ->
                        match (inOrder i j) with
                        | Some b -> Some b
                        | None -> isInOrder xs ys
                    | Choice2Of2 _ -> isInOrder ((Choice2Of2 (wrapInt i))::xs) secondPacket
                | Choice2Of2 p ->
                    match y with
                    | Choice1Of2 j -> isInOrder firstPacket ((Choice2Of2 (wrapInt j))::ys)
                    | Choice2Of2 q ->
                        match (isInOrder p.elements q.elements) with
                        | Some b -> Some b
                        | None -> isInOrder xs ys
            | []    -> Some false
        | []    ->
            match secondPacket with
            | _::_ -> Some true
            | []   -> None
        
    let rec firstPuzzleHelper (packetPairs: (Listish*Listish) list) (index: int) (result: int list) =
        match packetPairs with
        | p::ps ->
            match (isInOrder (fst p).elements (snd p).elements) with
            | Some true  -> firstPuzzleHelper ps (index + 1) (index::result)
            | Some false -> firstPuzzleHelper ps (index + 1) result
            | None       -> raise (Exception("couldn't compare packets..."))
        | []    -> result
        
    let rec printList printing original =
        match printing with
        | x::xs ->
            printfn $"{x}"
            printList xs original
        | []    -> original
        
    let printListHelper prl =
        printList prl prl
        
    let firstPuzzle (input: (Listish*Listish) list) =
        firstPuzzleHelper input 1 [] |> printListHelper |> List.sum