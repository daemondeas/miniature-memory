namespace Fsharp.Solutions

open System.Threading.Tasks.Dataflow
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
        
    let rec fallRockInChamber rock chamber windJets originalWindJets highestPoint =
        match windJets with
        | j::js ->
            let movedRock = moveRock j rock
            if outOfBounds movedRock || collides movedRock chamber then
                let fallenRock = fallRock rock
                if outOfBounds fallenRock || collides fallenRock chamber then
                    let highest = max highestPoint (rockTop rock)
                    (List.fold (fun s r -> Set.add r s) chamber rock, js, highest)
                else
                    fallRockInChamber fallenRock chamber js originalWindJets highestPoint
            else
                let fallenRock = fallRock movedRock
                if outOfBounds fallenRock || collides fallenRock chamber then
                    let highest = max highestPoint (rockTop movedRock)
                    (List.fold (fun s r -> Set.add r s) chamber movedRock, js, highest)
                else
                    fallRockInChamber fallenRock chamber js originalWindJets highestPoint
        | []    -> fallRockInChamber rock chamber originalWindJets originalWindJets highestPoint
        
    let transpose (height: int64) chamber =
        Set.map (fun r -> (fst r, (snd r) - height)) chamber
        
    let tryToBeSmart chamber (height: int64) =
        (Set.filter (fun r -> snd r <= height / 2L) chamber) = (Set.filter (fun r -> snd r > height / 2L) chamber |> transpose (height / 2L))
        
    let rec fallSomeRocks remainingRocks numberOfRocks numberOfRocksSoFar chamber windJets originalWindJets highestPoint =
        if numberOfRocks = numberOfRocksSoFar then
            highestPoint
        else
            match remainingRocks with
            | r::rs ->
                let newChamber, remainingJets, newHighest = fallRockInChamber (initiateRock highestPoint r) chamber windJets originalWindJets highestPoint
                fallSomeRocks rs numberOfRocks (numberOfRocksSoFar + 1L) newChamber remainingJets originalWindJets newHighest
            | []    ->
                if ((List.length windJets) = 0 || (List.length originalWindJets) = (List.length windJets)) && highestPoint % 2L = 0L && tryToBeSmart chamber highestPoint then
                    let factor = numberOfRocks / numberOfRocksSoFar
                    (factor * highestPoint) + (fallSomeRocks rocks (numberOfRocks - (factor * numberOfRocksSoFar)) 0 Set.empty windJets originalWindJets highestPoint)
                else
                    fallSomeRocks rocks numberOfRocks numberOfRocksSoFar chamber windJets originalWindJets highestPoint
            
    let firstPuzzle (input: Direction list) =
        fallSomeRocks rocks 2022 0 Set.empty input input 0
        
    let secondPuzzle (input: Direction list) =
        fallSomeRocks rocks 1000000000000L 0 Set.empty input input 0