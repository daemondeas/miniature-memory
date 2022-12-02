namespace Fsharp.Solutions

module Day02 =
    type Hand = Rock | Paper | Scissors
    type Round = {opponent: Hand; player: Hand}
    
    type Outcome = Loss | Draw | Win
    type HandAndOutcome = {opponent: Hand; outcome: Outcome}

    let handScore hand =
        match hand with
        | Rock     -> 1
        | Paper    -> 2
        | Scissors -> 3
        
    let parseOpponent h =
        match h with
        | 'A' -> Rock
        | 'B' -> Paper
        | 'C' -> Scissors
        
    let parsePlayer h =
        match h with
        | 'X' -> Rock
        | 'Y' -> Paper
        | 'Z' -> Scissors
        
    let parseOutcome o =
        match o with
        | 'X' -> Loss
        | 'Y' -> Draw
        | 'Z' -> Win
        
    let playScore opponent player =
        if opponent = player then
            3
        else
            match opponent with
            | Rock     ->
                match player with
                | Paper -> 6
                | _     -> 0
            | Paper    ->
                match player with
                | Rock -> 0
                | _    -> 6
            | Scissors ->
                match player with
                | Rock -> 6
                | _    -> 0
                
    let roundScore round =
        (handScore round.player) + (playScore round.opponent round.player)
        
    let firstPuzzle (input: Round list) =
        List.map roundScore input |> List.sum
        
    let outcomeFromHand outcome hand =
        match outcome with
        | Loss ->
            match hand with
            | Rock     -> Scissors
            | Paper    -> Rock
            | Scissors -> Paper
        | Draw -> hand
        | Win  ->
            match hand with
            | Rock     -> Paper
            | Paper    -> Scissors
            | Scissors -> Rock
        
    let mapOutcomeToRound (o: HandAndOutcome) =
        { opponent = o.opponent;
            player = (outcomeFromHand o.outcome o.opponent) }
        
    let secondPuzzle (input: HandAndOutcome list) =
        List.map mapOutcomeToRound input |> (List.map roundScore) |> List.sum