namespace Fsharp.Solutions

open Microsoft.FSharp.Collections

module Day15 =
    let manhattanDistance (a:int*int) (b:int*int) =
        (abs ((fst a) - (fst b))) + (abs ((snd a) - (snd b)))
            
    let getImpossibleOnLine sensor beacon line =
        let distanceToBeacon = manhattanDistance sensor beacon
        let distanceToLine = abs (line - (snd sensor))
        if distanceToLine < distanceToBeacon then
            [(fst sensor) - (distanceToBeacon - distanceToLine)..(fst sensor) + (distanceToBeacon - distanceToLine)] |> List.except [(fst beacon)]
        else
            []
      
    let firstPuzzle (input: ((int*int)*(int*int)) list) =
        List.map (fun s -> getImpossibleOnLine (fst s) (snd s) 2000000) input
        |> List.fold (fun a b -> a@b) []
        |> List.distinct
        |> List.length
        
    let getImpossibleOnRestrictedLine sensor beacon line maxVal =
        let distanceToBeacon = manhattanDistance sensor beacon
        let distanceToLine = abs (line - (snd sensor))
        if distanceToLine < distanceToBeacon then
            let from = max ((fst sensor) - (distanceToBeacon - distanceToLine)) 0
            let til = min ((fst sensor) + (distanceToBeacon - distanceToLine)) maxVal
            [from..til]
        else
            []
            
    let getImpossiblesOnRestrictedLine sensorsAndBeacons line maxVal =
        List.map (fun s -> getImpossibleOnRestrictedLine (fst s) (snd s) line maxVal) sensorsAndBeacons
        |> List.fold (fun a b -> a@b) []
        |> List.distinct
        
    let rec findMissing current maxVal numbers =
        if current > maxVal then
            raise (System.Exception("this really shouldn't happen ;)"))
        else
            if List.contains current numbers then
                findMissing (current + 1) maxVal numbers
            else
                current
        
    let rec findBeacon maxVal current sensorsAndBeacons =
        if current > maxVal then
            raise (System.Exception("this shouldn't happen ;)"))
        else
            let line = getImpossiblesOnRestrictedLine sensorsAndBeacons current maxVal
            if (List.length line) < (maxVal + 1) then
                (findMissing 0 maxVal line, current)
            else
                findBeacon maxVal (current + 1) sensorsAndBeacons
                
    let tuningFrequency beacon =
        printfn $"({fst beacon}, {snd beacon})"
        (fst beacon) * 4000000 + (snd beacon)
        
    let secondPuzzle (input: ((int*int)*(int*int)) list) =
        findBeacon 4000000 0 input |> tuningFrequency