namespace Fsharp.Solutions

open System.Collections.Generic

module Day22 =
    type Direction = Up | Right | Down | Left
    type Turn = LeftTurn | RightTurn
    type Tile = Open | Wall
    
    let getNewDirection direction turn =
        if direction = Up && turn = RightTurn || direction = Down && turn = LeftTurn then
            Right
        elif direction = Right && turn = RightTurn || direction = Left && turn = LeftTurn then
            Down
        elif direction = Down && turn = RightTurn || direction = Up && turn = LeftTurn then
            Left
        else
            Up
            
    let getNextPosition (xLimits: Dictionary<int, int*int>) (yLimits: Dictionary<int, int*int>) direction ((x, y): int*int) =
        match direction with
        | Up ->
            if y = fst (yLimits[x]) then
                (x, (snd (yLimits[x])))
            else
                (x, (y - 1))
        | Down ->
            if y = snd (yLimits[x]) then
                (x, (fst (yLimits[x])))
            else
                (x, (y + 1))
        | Left ->
            if x = fst (xLimits[y]) then
                ((snd (xLimits[y])), y)
            else
                ((x - 1), y)
        | Right ->
            if x = snd (xLimits[y]) then
                ((fst (xLimits[y])), y)
            else
                ((x + 1), y)
                
    let rec move (map: Dictionary<int*int, Tile>) xLimits yLimits direction position steps =
        match steps with
        | 0 -> position
        | _ ->
            let nextPosition = getNextPosition xLimits yLimits direction position
            if map[nextPosition] = Wall then
                position
            else
                move map xLimits yLimits direction nextPosition (steps - 1)
                
    let rec moves map xLimits yLimits direction position instructions =
        match instructions with
        | x::xs ->
            match x with
            | Choice1Of2 t -> moves map xLimits yLimits (getNewDirection direction t) position xs
            | Choice2Of2 i -> moves map xLimits yLimits direction (move map xLimits yLimits direction position i) xs
        | []    -> (position, direction)
        
    let directionPoints direction =
        match direction with
        | Right -> 0
        | Down  -> 1
        | Left  -> 2
        | Up    -> 3
    
    let firstPuzzle (map: Dictionary<int*int, Tile>) (xLimits: Dictionary<int, int*int>) (yLimits: Dictionary<int, int*int>) (instructions: Choice<Turn, int> list) =
        let (x, y), d = moves map xLimits yLimits Right ((fst (xLimits[1])), 1) instructions
        y * 1000 + x * 4 + (directionPoints d)
        
    let isOnRightOrDownEdge sideSize n =
        n % sideSize = 0
        
    let isOnLeftOrUpEdge sideSize n =
        n % sideSize = 1
        
    let getCubeSideTestData sideSize ((x, y): int*int) =
        if y <= sideSize then
            1
        elif x <= sideSize then
            2
        elif x > sideSize && x <= sideSize * 2 then
            3
        elif y > sideSize && y <= sideSize * 2 then
            4
        elif x > sideSize * 3 then
            6
        else
            5
        
    let getNextPositionAndDirectionOnCubeTestData sideSize direction ((x, y): int*int) =
        match direction with
        | Right ->
            if isOnRightOrDownEdge sideSize x then
                match (getCubeSideTestData sideSize (x, y)) with
                | 1 -> (((sideSize * 4), (sideSize * 3 - y)), Left)
                | 2 -> (((x + 1), y), direction)
                | 3 -> (((x + 1), y), direction)
                | 4 ->
                    if y % sideSize = 0 then
                        (((sideSize * 3 + 1 +  - (y % sideSize)), (sideSize * 2 + 1)), Down)
                    else
                        (((sideSize * 4 + 1 +  - (y % sideSize)), (sideSize * 2 + 1)), Down)
                | 5 -> (((x + 1), y), direction)
                | 6 ->
                    if y = sideSize * 3 then
                        (((sideSize * 3), 1), Left)
                    else
                        (((sideSize * 3), (sideSize + 1 - (y % sideSize))), Left)
            else
                (((x + 1), y), direction)
        | Left ->
            if isOnLeftOrUpEdge sideSize x then
                match (getCubeSideTestData sideSize (x, y)) with
                | 1 -> (((sideSize + y), (sideSize + 1)), Down)
                | 2 ->
                    if y = sideSize * 2 then
                        (((sideSize * 3 + 1), (sideSize * 3)), Up)
                    else
                        (((sideSize * 4 + 1 - (y % sideSize)), (sideSize * 3)), Up)
                | 3 -> (((x - 1), y), direction)
                | 4 -> (((x - 1), y), direction)
                | 5 ->
                    if y = sideSize * 3 then
                        (((sideSize + 1), (sideSize * 2)), Up)
                    else
                        (((sideSize * 2 + 1 - (y % sideSize)), (sideSize * 2)), Up)
                | 6 -> (((x - 1), y), direction)
            else
                (((x - 1), y), direction)
        | Down ->
            if isOnRightOrDownEdge sideSize y then
                match (getCubeSideTestData sideSize (x, y)) with
                | 1 -> ((x, (y + 1)), direction)
                | 2 -> (((sideSize * 3 + 1 - x), (sideSize * 3)), Up)
                | 3 ->
                    if x = sideSize * 2 then
                        (((sideSize * 2 + 1), (sideSize * 2 + 1)), Right)
                    else
                        (((sideSize * 2 + 1), (sideSize * 3 + 1 - (x % sideSize))), Right)
                | 4 -> ((x, (y + 1)), direction)
                | 5 ->
                    if x = sideSize * 3 then
                        ((1, (sideSize * 2)), Up)
                    else
                        (((sideSize + 1 - (x % sideSize)), (sideSize * 2)), Up)
                | 6 ->
                    if x = sideSize * 4 then
                        ((1, (sideSize + 1)), Right)
                    else
                        ((1, (sideSize * 2 + 1 - (x % sideSize))), Right)
            else
                ((x, (y + 1)), direction)
        | Up ->
            if isOnLeftOrUpEdge sideSize y then
                match (getCubeSideTestData sideSize (x, y)) with
                | 1 ->
                    if x = sideSize * 3 then
                        ((1, (sideSize + 1)), Down)
                    else
                        (((sideSize + 1 - (x % sideSize)), (sideSize + 1)), Down)
                | 2 -> (((sideSize * 3 + 1 - x), 1), Down)
                | 3 ->
                    if x = sideSize * 2 then
                        (((sideSize * 2 + 1), sideSize), Right)
                    else
                        (((sideSize * 2 + 1), (x % sideSize)), Right)
                | 4 -> ((x, (y - 1)), direction)
                | 5 -> ((x, (y - 1)), direction)
                | 6 ->
                    if x = sideSize * 4 then
                        (((sideSize * 3), (sideSize + 1)), Left)
                    else
                        (((sideSize * 3), (sideSize * 2 + 1 - (x % sideSize))), Left)
            else
                ((x, (y - 1)), direction)
        
    let getCubeSide sideSize ((x, y): int*int) =
        if x > sideSize * 2 then
            2
        elif y <= sideSize then
            1
        elif y > sideSize && y <= sideSize * 2 then
            3
        elif y > sideSize * 3 then
            6
        elif x <= sideSize then
            4
        else
            5
        
    let getNextPositionAndDirectionOnCube sideSize direction ((x, y): int*int) =
        match direction with
        | Right ->
            if isOnRightOrDownEdge sideSize x then
                match (getCubeSide sideSize (x, y)) with
                | 1 -> (((x + 1), y), direction)
                | 2 -> (((sideSize * 2), (sideSize * 3 + 1 - y)), Left)
                | 3 ->
                    if y % sideSize = 0 then
                        (((sideSize * 3), sideSize), Up)
                    else
                        (((sideSize * 2 + y % sideSize), sideSize), Up)
                | 4 -> (((x + 1), y), direction)
                | 5 ->
                    if y % sideSize = 0 then
                        (((sideSize * 3), 1), Left)
                    else
                        (((sideSize * 3), (sideSize + 1 - (y % sideSize))), Left)
                | 6 ->
                    if y % sideSize = 0 then
                        (((sideSize * 2), sideSize * 3), Up)
                    else
                        (((sideSize + y % sideSize), sideSize * 3), Up)
            else
                (((x + 1), y), direction)
        | Left ->
            if isOnLeftOrUpEdge sideSize x then
                match (getCubeSide sideSize (x, y)) with
                | 1 -> ((1, (sideSize * 3 - y)), Right)
                | 2 -> (((x - 1), y), direction)
                | 3 ->
                    if y % sideSize = 0 then
                        ((sideSize, (sideSize * 2 + 1)), Down)
                    else
                        (((sideSize + 1 - (y % sideSize)), (sideSize * 2 + 1)), Down)
                | 4 ->
                    if y % sideSize = 0 then
                        (((sideSize + 1), 1), Right)
                    else
                        (((sideSize + 1), (sideSize + 1 - (y % sideSize))), Right)
                | 5 -> (((x - 1), y), direction)
                | 6 ->
                    if y % sideSize = 0 then
                        (((sideSize * 2), 1), Down)
                    else
                        (((sideSize + (y % sideSize)), 1), Down)
            else
                (((x - 1), y), direction)
        | Down ->
            if isOnRightOrDownEdge sideSize y then
                match (getCubeSide sideSize (x, y)) with
                | 1 -> ((x, (y + 1)), direction)
                | 2 ->
                    if x % sideSize = 0 then
                        (((sideSize * 2), (sideSize * 2)), Left)
                    else
                        (((sideSize * 2), (sideSize + (x % sideSize))), Left)
                | 3 -> ((x, (y + 1)), direction)
                | 4 -> ((x, (y + 1)), direction)
                | 5 ->
                    if x % sideSize = 0 then
                        ((sideSize, (sideSize * 4)), Left)
                    else
                        ((sideSize, (sideSize * 3 + (x % sideSize))), Left)
                | 6 ->
                    if x % sideSize = 0 then
                        (((sideSize * 3), 1), Down)
                    else
                        (((sideSize * 2 + (x % sideSize)), 1), Down)
            else
                ((x, (y + 1)), direction)
        | Up ->
            if isOnLeftOrUpEdge sideSize y then
                match (getCubeSide sideSize (x, y)) with
                | 1 ->
                    if x % sideSize = 0 then
                        ((1, (sideSize * 4)), Right)
                    else
                        ((1, (sideSize * 3 + (x % sideSize))), Right)
                | 2 ->
                    if x % sideSize = 0 then
                        ((sideSize, (sideSize * 4)), Up)
                    else
                        (((x % sideSize), (sideSize * 3)), Up)
                | 3 -> ((x, (y - 1)), direction)
                | 4 ->
                    if x % sideSize = 0 then
                        (((sideSize + 1), (sideSize * 2)), Right)
                    else
                        (((sideSize + 1), (sideSize + (x % sideSize))), Right)
                | 5 -> ((x, (y - 1)), direction)
                | 6 -> ((x, (y - 1)), direction)
            else
                ((x, (y - 1)), direction)
                
    let rec moveOnCube (map: Dictionary<int*int, Tile>) sideSize direction position steps =
        match steps with
        | 0 -> (position, direction)
        | _ ->
            let nextPosition, nextDirection = getNextPositionAndDirectionOnCube sideSize direction position
            if map[nextPosition] = Wall then
                (position, direction)
            else
                moveOnCube map sideSize nextDirection nextPosition (steps - 1)
                
    let rec movesOnCube map sideSize direction position instructions =
        match instructions with
        | x::xs ->
            match x with
            | Choice1Of2 t -> movesOnCube map sideSize (getNewDirection direction t) position xs
            | Choice2Of2 i ->
                let nextPosition, nextDirection = moveOnCube map sideSize direction position i
                movesOnCube map sideSize nextDirection nextPosition xs
        | []    -> (position, direction)
        
    let secondPuzzle (map: Dictionary<int*int, Tile>) (xLimits: Dictionary<int, int*int>) (instructions: Choice<Turn, int> list) =
        let (x, y), d = movesOnCube map 50 Right ((fst (xLimits[1])), 1) instructions
        y * 1000 + x * 4 + (directionPoints d)