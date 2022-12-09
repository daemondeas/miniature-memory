namespace Fsharp.Solutions

open Microsoft.FSharp.Collections

module Day09 =
    type Direction = Up | Down | Left | Right
    type MovementInstruction = { direction: Direction; length: int }
    
    let isAdjacent ((headX, headY): int*int) ((tailX, tailY): int*int) =
        abs (headX - tailX) < 2 && abs (headY - tailY) < 2
        
    let moveTailOneDimension head tail =
        if head > tail then
            tail + 1
        elif head < tail then
            tail - 1
        else
            tail
        
    let moveTail ((headX, headY): int*int) ((tailX, tailY): int*int) =
        (moveTailOneDimension headX tailX, moveTailOneDimension headY tailY)
        
    let moveHead ((x, y): int*int) (direction: Direction) =
        match direction with
        | Up    -> (x, y + 1)
        | Down  -> (x, y - 1)
        | Left  -> (x - 1, y)
        | Right -> (x + 1, y)
        
    let rec performMovement (head: int*int) (tail: int*int) (instruction: MovementInstruction) (tailVisited: (int*int) list) =
        match instruction.length with
        | 0 -> (head, tail, tailVisited)
        | _ ->
            let newHead = moveHead head instruction.direction
            if isAdjacent newHead tail then
                performMovement newHead tail { direction = instruction.direction; length = (instruction.length - 1) } tailVisited
            else
                let newTail = (moveTail newHead tail)
                performMovement newHead newTail { direction = instruction.direction; length = (instruction.length - 1) } (newTail::tailVisited)
                
    let rec performMovements (head: int*int) (tail: int*int) (instructions: MovementInstruction list) (tailVisited: (int*int) list) =
        match instructions with
        | x::xs ->
            let newHead, newTail, newVisited = performMovement head tail x tailVisited
            performMovements newHead newTail xs newVisited
        | []    -> tailVisited
        
    let firstPuzzle (input: MovementInstruction list) =
        performMovements (0, 0) (0, 0) input [(0, 0)] |> List.distinct |> List.length
        
    let rec moveLongTail (head: int*int) (tail: (int*int) list) =
        match tail with
        | x::xs ->
            if isAdjacent head x then
                tail
            else
                let newTailHead = moveTail head x
                newTailHead::(moveLongTail newTailHead xs)
        | []    -> []
        
    let hasTailEndMoved (oldTail: (int*int) list) (newTail: (int*int) list) =
        oldTail[(List.length oldTail) - 1] <> newTail[(List.length oldTail) - 1]
        
    let rec performMovementLongTail (head: int*int) (tail: (int*int) list) (instruction: MovementInstruction) (tailVisited: (int*int) list) =
        match instruction.length with
        | 0 -> (head, tail, tailVisited)
        | _ ->
            let newHead = moveHead head instruction.direction
            let newTail = moveLongTail newHead tail
            if hasTailEndMoved tail newTail then
                performMovementLongTail newHead newTail { direction = instruction.direction; length = (instruction.length - 1) } (newTail[(List.length newTail) - 1]::tailVisited)
            else
                performMovementLongTail newHead newTail { direction = instruction.direction; length = (instruction.length - 1) } tailVisited
                
    let rec performMovementsLongTail (head: int*int) (tail: (int*int) list) (instructions: MovementInstruction list) (tailVisited: (int*int) list) =
        match instructions with
        | x::xs ->
            let newHead, newTail, newVisited = performMovementLongTail head tail x tailVisited
            performMovementsLongTail newHead newTail xs newVisited
        | []    -> tailVisited
        
    let rec generateTail length element =
        match length with
        | 0 -> []
        | _ -> element::(generateTail (length - 1) element)
        
    let secondPuzzle (input: MovementInstruction list) =
        performMovementsLongTail (0, 0) (generateTail 9 (0, 0)) input [(0, 0)] |> List.distinct |> List.length