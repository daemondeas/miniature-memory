namespace Fsharp.Solutions

module Day11 =
    type MonkeyTest = { test: bigint -> bool ; successDestination: int ; failDestination : int }
    type Monkey = { items: bigint list ; operation: bigint -> bigint ; test: MonkeyTest ; inspections: int64 }
    
    let throwToMonkey (monkey: Monkey) (item: bigint) =
        {
            items       = monkey.items @ [item]
            operation   = monkey.operation
            test        = monkey.test
            inspections = monkey.inspections
        }
        
    let newWorryLevel (operation: bigint -> bigint) old =
        (operation old) / bigint.Parse("3")
        
    let newWorryLevelWithCustomDivision (divisor: bigint) (operation: bigint -> bigint) old =
        operation old % divisor
        
    let turn (monkey: Monkey) (worryLevelCalculator: (bigint -> bigint) -> bigint -> bigint) =
        ({ items = [] ; operation = monkey.operation ; test = monkey.test ; inspections = monkey.inspections + int64 (List.length monkey.items) }, List.map (worryLevelCalculator monkey.operation) monkey.items)
        
    let rec throwItems (monkeys: Monkey[]) (items: bigint list) (index: int) =
        match items with
        | x::xs ->
            if monkeys[index].test.test x then
                monkeys[monkeys[index].test.successDestination] <- throwToMonkey monkeys[monkeys[index].test.successDestination] x
            else
                monkeys[monkeys[index].test.failDestination] <- throwToMonkey monkeys[monkeys[index].test.failDestination] x
            throwItems monkeys xs index
        | []    -> monkeys
        
    let rec roundHelper (monkeys: Monkey[]) index worryLevelCalculator =
        if index = Array.length monkeys then
            monkeys
        else
            let monkey, items = turn monkeys[index] worryLevelCalculator
            monkeys[index] <- monkey
            let monkeysAfterThrow = throwItems monkeys items index
            roundHelper monkeysAfterThrow (index + 1) worryLevelCalculator
            
    let executeRound monkeys worryLevelCalculator =
        roundHelper monkeys 0 worryLevelCalculator
        
    // let rec printMonkeys monkeys index =
    //     if index = Array.length monkeys then
    //         ()
    //     else
    //         printfn $"Monkey {index}: {monkeys[index].inspections}"
    //         printMonkeys monkeys (index + 1)
        
    let rec rounds monkeys amount worryLevelCalculator =
        match amount with
        | 0 -> monkeys
        | _ -> rounds (executeRound monkeys worryLevelCalculator) (amount - 1) worryLevelCalculator
        
    let calculateMonkeyBusiness (monkeys: Monkey[]) =
        Array.map (fun m -> m.inspections) monkeys |> Array.sortDescending |> (fun ms -> ms[0] * ms[1])
        
    let firstPuzzle (input: Monkey[]) =
        rounds input 20 newWorryLevel |> calculateMonkeyBusiness
        
    let secondPuzzle (input: Monkey[]) (divisor: bigint) =
        rounds input 10000 (newWorryLevelWithCustomDivision divisor) |> calculateMonkeyBusiness