# Photonic-Quantum

A modular quantum and photonic simulation/game framework, featuring 3D and 4D (tesseract) visualization, quantum-inspired mechanics, and extensible architecture.

## Project Vision

Photonic-Quantum aims to fuse photonic logic (light interference, diffraction, polarization) with quantum mechanics (superposition, entanglement, probabilistic collapse) in an interactive, visual, and extensible simulation/game environment. The flagship game, Prism Collapse, demonstrates these principles in a 3D/4D world.

## Features

- Quantum and photonic logic (superposition, entanglement, interference)
- 3D and 4D (tesseract/hypercube) visualization in WPF
- Modular, extensible C# architecture
- Animation, movement, collision, and physics systems
- Game loop and animation engine

## Core Implementation

- **Photon, LatticeNode3D, Gates, Wavefunction, CoherenceReservoir, ScoreSystem**: Core quantum and photonic classes (see `Core.cs`)
- **MovementEngine**: 3D movement and collision logic
- **PhysicsEngine**: Quantum physics and propagation
- **AnimationEngine**: Animation system scaffolding
- **GameLoop**: Game loop logic
- **MainWindow.xaml/.cs**: WPF UI, 3D/4D tesseract projection and animation

## 4D Visualization

- Implements a tesseract (hypercube) projection and animation in WPF 3D
- Real-time rotation and rendering of 4D objects

## Build & Run

### Prerequisites

- .NET 9 SDK or later
- Windows with WPF support

### Quick Start

```sh
# Run the 3D/4D visualization game
 dotnet run --project src/PrismCollapse3D/PrismCollapse3D.csproj
```

## Directory Structure

- `src/PrismCollapse3D/` — Main 3D/4D game and visualization
  - `Core.cs`, `MovementEngine.cs`, `PhysicsEngine.cs`, `AnimationEngine.cs`, `GameLoop.cs`, `MainWindow.xaml/.cs`

## Troubleshooting

- If you encounter build errors related to `InitializeComponent` or missing fields, ensure your XAML and code-behind namespaces and class names match, and that your project file does not explicitly include .cs/.xaml files (let the SDK handle it).
- Use `dotnet clean` and `dotnet build` to regenerate auto-generated files if needed.

## License

MIT
