namespace Daulis.FSharp

open System.Numerics

type Particle =
    { Position : Complex; IsWall : bool; Density : double; Force : Complex; Velocity : Complex; }
    static member Default = {Position = Complex.Zero; IsWall = false; Density = 0.0; Force = Complex.Zero; Velocity = Complex.Zero;}