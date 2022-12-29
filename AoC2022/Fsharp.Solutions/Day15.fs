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
        
    let getCircle ((x, y): int*int) distance =
        (List.zip [(x - distance)..x] (List.rev [(y - distance)..y])) @
        (List.zip [(x - distance)..x] [y..(y + distance)]) @
        (List.zip [x..(x + distance)] (List.rev [(y - distance)..y])) @
        (List.zip [x..(x + distance)] [y..(y + distance)])
    
    let rec outsidePerimeterOfAll sensorsAndBeacons position =
        match sensorsAndBeacons with
        | b::bs ->
            if (manhattanDistance position (fst b)) <= (manhattanDistance (fst b) (snd b)) then
                false
            else
                outsidePerimeterOfAll bs position
        | []    -> true
    
    let rec findBeaconHelper maxVal remainingSensorsAndBeacons originalSensorsAndBeacons =
        match remainingSensorsAndBeacons with
        | b::bs ->
            let circle = getCircle (fst b) ((manhattanDistance (fst b) (snd b)) + 1) |> List.filter (fun p -> (fst p) >= 0 && (fst p) <= maxVal && (snd p) >= 0 && (snd p) <= maxVal)
            if List.length (List.filter (outsidePerimeterOfAll originalSensorsAndBeacons) circle) > 0 then
                List.filter (outsidePerimeterOfAll originalSensorsAndBeacons) circle |> (fun l -> l[0])
            else
                findBeaconHelper maxVal bs originalSensorsAndBeacons
        | []    -> raise (System.Exception("Oh no!"))
            
    
    let findBeacon maxVal sensorsAndBeacons =
        findBeaconHelper maxVal sensorsAndBeacons sensorsAndBeacons
                
    let tuningFrequency beacon =
        printfn $"({fst beacon}, {snd beacon})"
        (fst beacon) * 4000000 + (snd beacon)
        
    let secondPuzzle (input: ((int*int)*(int*int)) list) =
        findBeacon 4000000 input |> tuningFrequency