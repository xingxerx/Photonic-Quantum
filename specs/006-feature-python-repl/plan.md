# Implementation Plan: Python QuTiP Photonic Quantum REPL

## Architecture
- Use QuTiP Qobj for quantum states and gates
- REPL built with Python's cmd.Cmd
- Modular: gates as functions, extendable for noise/custom gates
- Multi-qubit support via tensor products
- Measurement and collapse using QuTiP
- ASCII visualization for amplitudes and Bloch sphere

## Milestones
- Week 1: REPL skeleton, single-qubit gates, state print
- Week 2: Multi-qubit support, Bell state, measurement
- Week 3: ASCII visualization, error handling, help
- Week 4: Extendability (noise, custom gates), polish, docs

## Sequencing
1. Import QuTiP, set up single-qubit state
2. Implement gate functions (H, X, Y, Z, S)
3. Build REPL loop and command parsing
4. Add multi-qubit support (tensor, Bell)
5. Implement measurement and collapse
6. Add ASCII visualization
7. Error handling, help, quit
8. Polish, test, document
