namespace Fsharp.Solutions

module Day08 =
    let rowFromColumn (matrix: 'a list list) (columnIndex: int) =
        List.map (fun (x: 'a list) -> x[columnIndex]) matrix
        
    let isVisibleInRow (row: int list) index =
        List.forall (fun t -> t < row[index]) (row[..index - 1]) ||
        List.forall (fun t -> t < row[index]) (row[index + 1..])
        
    let isVisible (forest: int list list) (x: int) (y: int) =
        isVisibleInRow (forest[y]) x || isVisibleInRow (rowFromColumn forest x) y
        
    let rec countVisible (forest: int list list) (x: int) (y: int) (width: int) (height: int) (amount: int) =
        if y = height && x = width then
            amount + 1
        elif x = width then
            countVisible forest 0 (y + 1) width height (amount + 1)
        elif y = 0 || y = height || x = 0 || isVisible forest x y then
            countVisible forest (x + 1) y width height (amount + 1)
        else
            countVisible forest (x + 1) y width height amount
            
    let firstPuzzle (input: int list list) =
        countVisible input 0 0 ((List.length input[0]) - 1) ((List.length input) - 1) 0
        
    let rec visibleInDirection row height amount =
        match row with
        | x::xs ->
            if x >= height then
                amount + 1
            else
                visibleInDirection xs height (amount + 1)
        | []    -> amount
        
    let getScenicScore (forest: int list list) x y =
        (visibleInDirection (List.rev (forest[y][..x - 1])) (forest[y][x]) 0)
            * (visibleInDirection (forest[y][x + 1..]) (forest[y][x]) 0)
            * (visibleInDirection (List.rev ((rowFromColumn forest x)[..y - 1])) (forest[y][x]) 0)
            * (visibleInDirection ((rowFromColumn forest x)[y + 1..]) (forest[y][x]) 0)
        
    let rec getHighestScenicScore (forest: int list list) (x: int) (y: int) (width: int) (height: int) (highest: int) =
        if y = height && x = width then
            max (getScenicScore forest x y) highest
        elif x = width then
            getHighestScenicScore forest 0 (y + 1) width height (max (getScenicScore forest x y) highest)
        else
            getHighestScenicScore forest (x + 1) y width height (max (getScenicScore forest x y) highest)
        
    let secondPuzzle (input: int list list) =
        getHighestScenicScore input 0 0 ((List.length input[0]) - 1) ((List.length input) - 1) 0