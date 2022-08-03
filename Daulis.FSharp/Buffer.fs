module Daulis.FSharp.Buffer

open System

let private (|||) (a: char) b = char (int a ||| int b)

let private bufferToAscii (buffer: char []) =
    for i = 0 to Fluids.OutputBufferSize - 2 do
        if (i + 1) % Fluids.Width = 0 then
            buffer[i] <- '\n'
        else
            buffer[i] <- Fluids.Lookup[int buffer[i]]

let update (buffer: char []) (data: Particle [] * int) =
    Array.Clear(buffer, 0, Fluids.OutputBufferSize)
    let particles, _ = data

    for i = 0 to particles.Length - 1 do
        let particle = particles[i]
        let x = int -particle.Position.Imaginary
        let y = int (particle.Position.Real / 2.0)

        if
            not
                (
                    x < 0
                    || x >= Fluids.Width - 1
                    || y < 0
                    || y >= Fluids.Height - 2
                )
        then
            let current = x + Fluids.Width * y
            let right = current + 1
            let below = current + Fluids.Width
            let belowRight = below + 1

            buffer[current] <- buffer[current] ||| char 8
            buffer[right] <- buffer[right] ||| char 4
            buffer[below] <- buffer[below] ||| char 2
            buffer[belowRight] <- buffer[belowRight] ||| char 1

    bufferToAscii buffer
    (buffer, data)

let writeToConsole (data: char [] * (Particle [] * int)) =
    Console.SetCursorPosition(0, 0)
    Console.Write(data |> fst)
