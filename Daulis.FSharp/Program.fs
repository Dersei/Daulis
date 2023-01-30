open System.Threading
open Daulis.FSharp

let updateDrawing pAndLen buf =
    pAndLen
    |> Particles.updateDynamics
    |> Particles.updatePosition
    |> Buffer.update buf
    |> Buffer.writeToConsole

    Thread.Sleep Fluids.FrameSleep

[<EntryPoint>]
let main args =
    let buf =
        Array.zeroCreate Fluids.OutputBufferSize

    let pAndLen =
        Read.fromFile Fluids.ParticleBufferSize "Input/tanada.txt"

    while true do
        updateDrawing pAndLen buf

    0
