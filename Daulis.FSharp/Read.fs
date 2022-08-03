module Daulis.FSharp.Read

open System
open System.IO
open System.Numerics
open System.Text

let fromFile len (filename: string) =
    let mutable position: Complex =
        Complex.op_Implicit 0

    let particles =
        Array.create len Particle.Default

    let mutable length = 0

    use streamReader =
        new StreamReader(filename, Encoding.ASCII)

    while not streamReader.EndOfStream do
        let chr =
            streamReader.Read() |> Convert.ToChar

        match chr with
        | '\n' ->
            position <- position + 2.0
            position <- Complex.op_Implicit position.Real
        | _ when chr > ' ' ->
            particles[length] <- { particles[length] with Position = position }
            particles[length + 1] <- { particles[length] with Position = position + 1.0 }
            particles[length] <- { particles[length] with IsWall = chr = '#' }
            particles[length + 1] <- { particles[length + 1] with IsWall = chr = '#' }
            length <- length + 2
            position <- position - Complex.ImaginaryOne
        | _ -> position <- position - Complex.ImaginaryOne

    (particles, length)
