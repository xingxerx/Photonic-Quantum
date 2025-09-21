# Implementation Plan: Photonic Quantum Animation (PQA)

## Architecture

- **Unity Project Structure:**
  - `Assets/Scripts/Quantum/` for quantum logic (Qubit, Gate, etc.)
  - `Assets/Scripts/Photon/` for photon animation and shaders
  - `Assets/Scripts/UI/` for circuit builder and controls
  - `Tools/spec-kit/` for Python CLI integration
- **Quantum Logic:**
  - C# classes for Qubit (state vector), Gate operations (matrix math)
- **Animation:**
  - Timeline-driven photon paths, interference via custom shaders
- **UI:**
  - Canvas-based drag-and-drop circuit builder, real-time state display
- **Export:**
  - Unity Recorder for GIF/MP4 output
- **Spec-Kit Integration:**
  - Editor script to run spec-kit CLI from Unity menu

## Milestones

- **Week 1:** Project setup, basic qubit state display
- **Week 2:** Core gates (Hadamard, CNOT, phase), simple animations
- **Week 3:** Photonic visuals (ray tracing, polarization)
- **Week 4:** UI integration, export, spec-kit hooks
- **Week 5:** Tests, polish, GitHub release

## Sequencing

1. Repo/Unity setup, spec-kit install
2. Quantum logic core (Qubit, Gate base)
3. Gate implementations and animations
4. Photon visuals and shaders
5. UI and circuit builder
6. Export module
7. Spec-kit Unity integration
8. Testing and documentation
