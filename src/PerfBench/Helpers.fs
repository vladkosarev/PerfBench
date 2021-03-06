﻿module PerfBench.Helpers

open Types

let failIfNeeded message e =
                 match e with
                 | _, _, Succeeded(_, t) -> t
                 | _, _, Failed(_, t) -> t
                 | _, _, Executing -> failwith message

let getAverage list = 
    match list with
    | [] -> 0.0
    | l -> 
        l |> List.averageBy (fun e -> failIfNeeded "Trying to average on Executing" e)

let getMax list = 
    match list with
    | [] -> 0.0
    | l -> 
        let maxElement = 
            l |> List.maxBy (fun e -> failIfNeeded "Trying to getMax on Executing" e)
        failIfNeeded "Trying to getMax on Executing" maxElement

let printStats state time = 
    let (successfulDrones, failedDrones) = 
        state |> List.partition (fun e -> 
                     match e with
                     | _, _, Succeeded(_, _) -> true
                     | _ -> false)
    
    let successfulAverage = getAverage successfulDrones
    let failedAverage = getAverage failedDrones
    let successfulMax = getMax successfulDrones
    let failedMax = getMax failedDrones
    printfn ""
    printfn "Total: %d drones" state.Length
    printfn "Processing Time: %f " time
    printfn ""
    printfn "Succeeded: %d drones" successfulDrones.Length
    printfn "Average processing time is %f" successfulAverage
    printfn "Max processing time is %f" successfulMax
    printfn ""
    printfn "Failed: %d drones" failedDrones.Length
    printfn "Average processing time for failed drones is %f" failedAverage
    printfn "Max processing time for failed drones is %f" failedMax
