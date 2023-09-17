# GraduteThesis_Master
Master Graduate Thesis Materials

## Plan
```mermaid
gantt
    title Timeline for graduate thesis
    dateFormat  YYYY-MM-DD
    section Paper reading
        Initial understand              : a1, 2023-02-05, 15d
        Basic reading                   : a2, after a1, 30d
        Longterm reading                : active, a3, after a2, 60d
    section Paper work
        ERP                             : active, crit, b1, 2023-04-01, 15d
        Initialization done             : milestone, m1, 2023-04-15, 1d
        Paper draft                     : b2, 2023-07-01, 30d
        Paper refine                    : crit, b3, after b2, 15d
        Paper submit                    : milestone, m2, after b3, 1d
        Slides & Poster                 : b4, after b3, 7d
    section Thesis work
        Tech. learning and plan         : c1, after a1, 40d
        Design and deploy               : active, c2, after c1, 30d
        Searching volunteers            : crit, c3, 2023-04-15, 80d
        Iterative exp. and design       : crit, c4, after c2, 80d
        Basic exp. done                 : milestone, m3, 2023-07-20, 1d
        Sup. exp. and anlysis           : c5, 2023-07-01, 30d
```
## Scheduled meeting
Every Thursday or Friday.

## Paper work
Please refer to [the sharelatex site](https://sharelatex.tum.de/project/63a8a5565ac510008631f18f) of TU Munich.

## Project structure
1. The folder `TUMARTest` contains the Unity project for the AR project, which suits our experiment group.  
2. The folder `TUMARBlackTest` contains the Unity project for the AR project, which suits our control group.  
3. The folder `EXP_DATA` contains our digitialized data from the experiment, along with the analysis code.
