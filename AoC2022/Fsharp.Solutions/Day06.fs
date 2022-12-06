namespace Fsharp.Solutions

open Microsoft.FSharp.Collections

module Day06 =
    let rec getPositionOfFirstDistinctPart (buffer: char list) length position =
        if (List.distinct (buffer[0..(length - 1)]) |> List.length) = length then
            position
        else
            getPositionOfFirstDistinctPart (List.tail buffer) length (position + 1)
        
    let firstPuzzle (input: char list) =
        getPositionOfFirstDistinctPart input 4 4
        
    let secondPuzzle (input: char list) =
        getPositionOfFirstDistinctPart input 14 14