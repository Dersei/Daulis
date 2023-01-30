namespace Daulis.FSharp

open System

module Fluids =

    let Width = 80
    let Height = 25
    let ParticleBufferSize = Width * Height
    let FrameSleep = TimeSpan.FromMilliseconds 100
    let Lookup = @" '`-.|//,\|\_\/#"

    let OutputBufferSize =
        ParticleBufferSize + Height

    let Gravity = 1.0
    let Pressure = 8.0
    let Viscosity = 8.0
