module Daulis.FSharp.Particles

open System.Numerics

let private kernel distance =
    Complex.op_Implicit (distance / 2.0 - 1.0)
    * (distance / 2.0 - 1.0)

let private pairwiseSpline updateNearby (data : Particle[] * int) =
    let particles, length = data
    for i = 0 to length - 1 do
        for j = 0 to length - 1 do
            let delta =
                particles[i].Position - particles[j].Position

            let distance = Complex.Abs delta

            if (distance <= 2.0) then
                particles[i] <- updateNearby delta distance particles[i] particles[j]

    data

let private updateDensity _ distance (fromP: Particle) (_: Particle) =
    { fromP with Density = fromP.Density + kernel(distance).Magnitude }

let private updateForce (delta: Complex) distance (fromP: Particle) (toP: Particle) =
    let force =
        (fromP.Density + toP.Density - 3.0)
        * delta
        * Fluids.Pressure
        - (fromP.Velocity - toP.Velocity) * Fluids.Viscosity

    { fromP with Force = fromP.Force + (force / fromP.Density) * kernel(distance) }

let private initParticles (data : Particle[] * int) =
    let particles, length = data
    for i = 0 to length - 1 do
        particles[i] <- { particles[i] with
                            Force = Complex.op_Implicit Fluids.Gravity
                            Density = if particles[i].IsWall then 9 else 0 }
    data

let updateDynamics (data : Particle[] * int) =
    data
        |> initParticles
        |> pairwiseSpline updateDensity
        |> pairwiseSpline updateForce

let updatePosition (data : Particle[] * int) =
    let particles, length = data
    for i = 0 to length - 1 do
        if not particles[i].IsWall then
            particles[i] <- { particles[i] with Velocity = particles[i].Velocity + particles[i].Force / 10.0; }
            particles[i] <- { particles[i] with Position = particles[i].Position + particles[i].Velocity }
    data