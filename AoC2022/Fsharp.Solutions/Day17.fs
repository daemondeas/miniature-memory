namespace Fsharp.Solutions

open System.Collections.Generic
open Microsoft.FSharp.Collections

module Day17 =
    type Direction = Left | Right
    
    let (rocks: (int64*int64) list list) =
        [[(0, 0); (1, 0); (2, 0); (3, 0)];
         [(1, 0); (0, 1); (1, 1); (2, 1); (1, 2)];
         [(0, 0); (1, 0); (2, 0); (2, 1); (2, 2)];
         [(0, 0); (0, 1); (0, 2); (0, 3)];
         [(0, 0); (1, 0); (0, 1); (1, 1)]]
        
    let initiateRock (highestPoint: int64) (rock: (int64*int64) list) =
        List.map (fun r -> ((fst r) + 3L), ((snd r) + 4L + highestPoint)) rock
        
    let movementFunction direction =
        match direction with
        | Left  -> (fun r -> (((fst r) - 1L), snd r))
        | Right -> (fun r -> (((fst r) + 1L), snd r))
        
    let moveRock direction rock =
        List.map (movementFunction direction) rock
        
    let fallRock rock =
        List.map (fun r -> ((fst r), (snd r) - 1L)) rock
        
    let outOfBounds rock =
        List.fold (fun b r -> b || (fst r) < 1L || (fst r) > 7L || (snd r) = 0L) false rock
        
    let collides rock chamber =
        List.fold (fun b r -> b || Set.contains r chamber) false rock
        
    let rockTop rock =
        List.map snd rock |> List.max
        
    let zeroIfEmpty setYo =
        if Set.count setYo = 0 then
            None
        else
            Some setYo
            
    let highestInColumn col =
        match col with
        | Some c -> Set.maxElement c
        | None   -> 0L
    
    let getTop chamber =
        let ones = Set.filter (fun s -> (fst s) = 1L) chamber |> Set.map snd |> zeroIfEmpty |> highestInColumn
        let twos = Set.filter (fun s -> (fst s) = 2L) chamber |> Set.map snd |> zeroIfEmpty |> highestInColumn
        let threes = Set.filter (fun s -> (fst s) = 3L) chamber |> Set.map snd |> zeroIfEmpty |> highestInColumn
        let fours = Set.filter (fun s -> (fst s) = 4L) chamber |> Set.map snd |> zeroIfEmpty |> highestInColumn
        let fives = Set.filter (fun s -> (fst s) = 5L) chamber |> Set.map snd |> zeroIfEmpty |> highestInColumn
        let sixes = Set.filter (fun s -> (fst s) = 6L) chamber |> Set.map snd |> zeroIfEmpty |> highestInColumn
        let sevens = Set.filter (fun s -> (fst s) = 7L) chamber |> Set.map snd |> zeroIfEmpty |> highestInColumn
        let lowest = List.min [ones; twos; threes; fours; fives; sixes; sevens]
        ((ones - lowest), (twos - lowest), (threes - lowest), (fours - lowest), (fives - lowest), (sixes - lowest), (sevens - lowest))
        
    let rec fallRockInChamber rock chamber windJets originalWindJets highestPoint =
        match windJets with
        | j::js ->
            let movedRock = moveRock j rock
            if outOfBounds movedRock || collides movedRock chamber then
                let fallenRock = fallRock rock
                if outOfBounds fallenRock || collides fallenRock chamber then
                    let highest = max highestPoint (rockTop rock)
                    let newChamber = List.fold (fun s r -> Set.add r s) chamber rock
                    (newChamber, js, highest, (getTop newChamber))
                else
                    fallRockInChamber fallenRock chamber js originalWindJets highestPoint
            else
                let fallenRock = fallRock movedRock
                if outOfBounds fallenRock || collides fallenRock chamber then
                    let highest = max highestPoint (rockTop movedRock)
                    let newChamber = List.fold (fun s r -> Set.add r s) chamber movedRock
                    (newChamber, js, highest, (getTop newChamber))
                else
                    fallRockInChamber fallenRock chamber js originalWindJets highestPoint
        | []    -> fallRockInChamber rock chamber originalWindJets originalWindJets highestPoint
        
    let rec fallSomeRocks remainingRocks numberOfRocks numberOfRocksSoFar chamber windJets originalWindJets highestPoint (cycleDict: Dictionary<int64*int64*(int64*int64*int64*int64*int64*int64*int64), int64*int64>) cycleCheck =
        if numberOfRocks = numberOfRocksSoFar then
            highestPoint
        else
            match remainingRocks with
            | r::rs ->
                let newChamber, remainingJets, newHighest, x = fallRockInChamber (initiateRock highestPoint r) chamber windJets originalWindJets highestPoint
                if cycleDict.ContainsKey ((List.length remainingJets), (List.length remainingRocks), x) then
                    let oldHeight, oldRocks = cycleDict[((List.length remainingJets), (List.length remainingRocks), x)]
                    let cycleHeight = newHighest - oldHeight
                    let cycleRocks = numberOfRocksSoFar - oldRocks
                    let amountOfCycles = (numberOfRocks - numberOfRocksSoFar) / cycleRocks
                    let newRocksSoFar = numberOfRocksSoFar + amountOfCycles * cycleRocks + 1L
                    let addedHeight = amountOfCycles * cycleHeight
                    let newHeight = newHighest + addedHeight
                    let movedChamber = Set.map (fun r -> ((fst r), ((snd r) + addedHeight))) newChamber
                    fallSomeRocks rs numberOfRocks newRocksSoFar movedChamber remainingJets originalWindJets newHeight (Dictionary<int64*int64*(int64*int64*int64*int64*int64*int64*int64), int64*int64>()) false
                else
                    if cycleCheck then
                        cycleDict[((List.length remainingJets), (List.length remainingRocks), x)] <- (newHighest, numberOfRocksSoFar)
                    fallSomeRocks rs numberOfRocks (numberOfRocksSoFar + 1L) newChamber remainingJets originalWindJets newHighest cycleDict cycleCheck
            | []    -> fallSomeRocks rocks numberOfRocks numberOfRocksSoFar chamber windJets originalWindJets highestPoint cycleDict cycleCheck
                
    let firstPuzzle (input: Direction list) =
        fallSomeRocks rocks 2022 0 Set.empty input input 0 (Dictionary<int64*int64*(int64*int64*int64*int64*int64*int64*int64), int64*int64>()) true
        
    let secondPuzzle (input: Direction list) =
        fallSomeRocks rocks 1000000000000L 0 Set.empty input input 0 (Dictionary<int64*int64*(int64*int64*int64*int64*int64*int64*int64), int64*int64>()) true