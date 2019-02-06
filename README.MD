# MagentaSim
MagentaSim is a personal educational project to learn about Programming, Databases and Economics.
It is in early development atm.

## What?
MagentaSim is an agent-based model of a market structure. The basic idea is to have a number of autonomous agents to act as [Homo oeconomicus](https://en.wikipedia.org/wiki/Homo_economicus) in a given environment. Economic theories can be simulated on the level of individual agents.

The name "MagentaSim" is a blend of "Market", "Agent" and "Simulation". Magenta is also a color.

## How?
MagentaSim is written in C# and uses an SQLite Database to hold the state of the model and record the simulation.

## Thoughts on Agent-Based Modelling
### Time and the right to go first
In reality time is continuous and agents can act at the same time. To model this time will be represented in steps. Every agent can do one action in one step.

But how to handle situations when in the same step several agents try to do the same action. One agent acts and with his action changes the environment so that the second agent can't act. How to decide which agent can do the action first and which has to deal with an changed environment.

In many agent-based models the agents have a location and can only interact with the environment at their location. This wouldn't make lots of sense for a market model.

If the agents had a fixed order in which they act, the first few agents would get a better deals than the later ones all the time. This can be an interesting effect to study but it contradicts the paradigm [perfect competition](https://en.wikipedia.org/wiki/Perfect_competition) which is important for many market models.

Possible Solutions:
 1. shuffle order randomly every step
 2. rotate order every step (first agent will be the last in thx next step)

There will always be agents who have an advantage by going first but both these ways should prevent this effect to grow over time. Which one works better is to be seen.


## Ideen für die Zukunft
 - Visualisierung der Ergebnisse in Graphen
    - Berechnete Idealergebnisse einblendbar
 - Mehrere Simulation nacheinander automatisch
    - Gesamtübersicht
    - Durschnittsergebnis
 - Replay mit eintsellbarer Geschwindigkeit
 - Start- und Randbedingungen: welche kann man einstellen, wo und wie?
    - dürfen sich Agenten verschulden / was ist ihr Skonto
 - Geld: Jeder verwaltet seinen "Geldbeutel" selbst oder es gibt eine Bank, die Transaktionen abwickelt 
 - Mengenrabatt / Mindestmenge
 - unterschiedliche Waren