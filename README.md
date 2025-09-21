# Photonic-Quantum

A modular quantum and photonic simulation/game framework, featuring 3D and 4D (tesseract) visualization, quantum-inspired mechanics, and extensible architecture.

## Game Concept: Prism Collapse

Prism Collapse is a simulation/game where you explore a quantum photonic lattice. Photons move, interact, and collapse according to quantum rules—demonstrating superposition, entanglement, interference, and probabilistic collapse. The game visualizes these phenomena in 3D and 4D (tesseract) using WPF, with animated ribbons and collapse effects.

### Educational Focus

- All core classes and algorithms are documented for learning and experimentation.
- The codebase is modular, making it easy to extend for new quantum/photonic mechanics or visualizations.
- WPF 3D is used for real-time, interactive graphics.

## Features

- Quantum and photonic logic (superposition, entanglement, interference)
- 3D and 4D (tesseract/hypercube) visualization in WPF
- Modular, extensible C# architecture
- Animation, movement, collision, and physics systems
- Game loop and animation engine
- Lattice simulation and animated ribbons/collapses

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

## WPF 3D Extensions

- The project is designed for easy extension with new WPF 3D visualizations, controls, and shaders.
- Educational comments and modular code help you add new quantum/photonic effects or gameplay.

## Troubleshooting

- If you encounter build errors related to `InitializeComponent` or missing fields, ensure your XAML and code-behind namespaces and class names match, and that your project file does not explicitly include .cs/.xaml files (let the SDK handle it).
- Use `dotnet clean` and `dotnet build` to regenerate auto-generated files if needed.

## License

MIT
