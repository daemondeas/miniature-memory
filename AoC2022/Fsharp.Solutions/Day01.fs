namespace Fsharp.Solutions

module Day01 =
    let sumBag (bag: int list) =
        List.fold (+) 0 bag
        
    let firstPuzzle (calorieBags: int list list) =
        List.map sumBag calorieBags
        |> List.max
        
    let secondPuzzle (calorieBags: int list list) =
        List.map sumBag calorieBags
        |> List.sortDescending
        |> (fun (bs: int list) -> bs[0] + bs[1] + bs[2])