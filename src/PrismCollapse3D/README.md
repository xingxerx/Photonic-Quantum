# PrismCollapse3D

A photonic and quantum mechanics-inspired 3D/4D visualization and simulation game, built with C# and WPF.

## Features
- 3D and 4D (tesseract) visualization using WPF Viewport3D
- Quantum-inspired movement, collision, and physics logic
- Modular architecture for easy extension
- Animation and game loop system

## Getting Started

### Prerequisites
- .NET 9 SDK or later
- Windows with WPF support

### Build and Run

1. Clone the repository
2. Open a terminal in the project root
3. Build and run the 3D/4D app:
   ```
   dotnet run --project src/PrismCollapse3D/PrismCollapse3D.csproj
   ```

## Project Structure
- `Core.cs` — Core quantum and photonic logic
- `MovementEngine.cs` — 3D movement and collision
- `PhysicsEngine.cs` — Quantum physics and propagation
- `AnimationEngine.cs` — Animation system
- `GameLoop.cs` — Game loop logic
- `MainWindow.xaml` / `.cs` — WPF UI and 4D visualization

## License
MIT
