namespace Fsharp.Solutions

open System

module Day10 =
    type InstructionType = NOOP | ADDX
    type Instruction = { iType: InstructionType ; value: int }
    type CPU = { registerX: int ; cycleCount: int }
    
    let executeInstruction (cpu: CPU) (instruction: Instruction) =
        match instruction.iType with
        | NOOP ->
            { registerX = cpu.registerX; cycleCount = (cpu.cycleCount + 1) }
        | ADDX ->
            { registerX = cpu.registerX + instruction.value; cycleCount = (cpu.cycleCount + 2) }
            
    let signalStrength (cpu: CPU) = cpu.registerX * cpu.cycleCount
    
    let instructionAsString (instr: Instruction) =
        match instr.iType with
        | NOOP -> "NOOP"
        | ADDX -> $"ADDX {instr.value}"
    
    let rec executeAndGetStrengths (cpu: CPU) (instructions: Instruction list) (signalStrengths: int list) =
        match instructions with
        | x::xs ->
            if cpu.cycleCount <= 220 && cpu.cycleCount % 40 = 18 && x.iType = ADDX then
                executeAndGetStrengths (executeInstruction cpu x) xs (((signalStrength cpu) + (cpu.registerX * 2))::signalStrengths)
            elif cpu.cycleCount <= 220 && cpu.cycleCount % 40 = 19 then
                executeAndGetStrengths (executeInstruction cpu x) xs (((signalStrength cpu) + cpu.registerX)::signalStrengths)
            else
                executeAndGetStrengths (executeInstruction cpu x) xs signalStrengths
        | []    -> signalStrengths
        
    let firstPuzzle (input: Instruction list) =
        executeAndGetStrengths { registerX = 1 ; cycleCount = 0 } input [] |> List.sum
        
    let drawPixel crt sprite =
        match abs (crt - sprite) with
        | 0 -> "#"
        | 1 -> "#"
        | _ -> "."
        
    let rec executeAndDraw (cpu: CPU) (instructions: Instruction list) (screen: string) =
        match instructions with
        | x::xs ->
            match x.iType with
            | NOOP -> executeAndDraw (executeInstruction cpu x) xs (screen + (drawPixel (cpu.cycleCount % 40) cpu.registerX))
            | ADDX -> executeAndDraw (executeInstruction cpu x) xs (screen + (drawPixel (cpu.cycleCount % 40) cpu.registerX) + (drawPixel ((cpu.cycleCount + 1) % 40) cpu.registerX))
        | []    -> screen
        
    let secondPuzzle (input: Instruction list) =
        executeAndDraw { registerX = 1 ; cycleCount = 0 } input ""
            |> (fun s -> s.ToCharArray())
            |> Array.splitInto 6
            |> Array.map String