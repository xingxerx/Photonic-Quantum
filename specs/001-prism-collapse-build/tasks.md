# Tasks: Prism Collapse 3D

**Input**: Design documents from `/specs/001-prism-collapse-build/`
**Prerequisites**: plan.md (required)

## Execution Flow (main)
```
1. Load plan.md from feature directory
2. Generate tasks by category: setup, tests, core (models, movement, collision, physics), integration, polish
3. Number tasks sequentially (T001, T002...)
4. Mark [P] for parallel tasks (different files, no dependencies)
5. Specify exact file paths
6. Validate task completeness
```

## Phase 3.1: Setup
- [ ] T001 Create 3D project structure in `src/PrismCollapse3D/`
- [ ] T002 Initialize C# project with WPF 3D or Unity dependencies in `src/PrismCollapse3D/`
- [ ] T003 [P] Configure linting and formatting tools for C#

## Phase 3.2: Tests First (TDD)
- [ ] T004 [P] Write unit tests for Photon, LatticeGrid3D, and Gate classes in `src/PrismCollapse3D/Tests/CoreTests.cs`
- [ ] T005 [P] Write integration test for 3D movement and collision in `src/PrismCollapse3D/Tests/MovementTests.cs`
- [ ] T006 [P] Write integration test for quantum propagation and physics in `src/PrismCollapse3D/Tests/PhysicsTests.cs`

## Phase 3.3: Core Implementation
- [ ] T007 [P] Implement Photon, Gate (Splitter, Shifter, Entangler, Polarizer), and LatticeGrid3D models in `src/PrismCollapse3D/Core.cs`
- [ ] T008 [P] Implement 3D movement and collision logic in `src/PrismCollapse3D/MovementEngine.cs`
- [ ] T009 [P] Implement quantum propagation and physics (superposition, entanglement) in `src/PrismCollapse3D/PhysicsEngine.cs`
- [ ] T010 Implement animation system for 3D movement, collision, and quantum effects in `src/PrismCollapse3D/AnimationEngine.cs`
- [ ] T011 Implement game loop and UI in `src/PrismCollapse3D/GameLoop.cs`
- [ ] T012 Implement scoring, coherence reservoir, and PvP basics in `src/PrismCollapse3D/GameLogic.cs`

## Phase 3.4: Integration
- [ ] T013 Integrate all core systems and UI in `src/PrismCollapse3D/MainWindow.xaml.cs`

## Phase 3.5: Polish
- [ ] T014 [P] Add unit tests for edge cases in `src/PrismCollapse3D/Tests/EdgeCaseTests.cs`
- [ ] T015 [P] Optimize performance for large 3D lattices in `src/PrismCollapse3D/Performance.cs`
- [ ] T016 [P] Update documentation in `src/PrismCollapse3D/README.md`

## Parallel Example
```
# Launch T004-T006 together:
Task: "Write unit tests for Photon, LatticeGrid3D, and Gate classes in src/PrismCollapse3D/Tests/CoreTests.cs"
Task: "Write integration test for 3D movement and collision in src/PrismCollapse3D/Tests/MovementTests.cs"
Task: "Write integration test for quantum propagation and physics in src/PrismCollapse3D/Tests/PhysicsTests.cs"

# Launch T007-T009 together after tests fail:
Task: "Implement Photon, Gate, and LatticeGrid3D models in src/PrismCollapse3D/Core.cs"
Task: "Implement 3D movement and collision logic in src/PrismCollapse3D/MovementEngine.cs"
Task: "Implement quantum propagation and physics in src/PrismCollapse3D/PhysicsEngine.cs"
```

## Dependencies
- Setup (T001-T003) before everything
- Tests (T004-T006) before implementation (T007-T012)
- Models before movement/physics
- Core before integration
- Everything before polish

## Notes
- [P] tasks = different files, no dependencies
- Commit after each task
- Avoid: vague tasks, same file conflicts
