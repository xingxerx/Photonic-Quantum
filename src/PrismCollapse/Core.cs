using System;
using System.Collections.Generic;

namespace PrismCollapse.Core
{
    // Represents a quantum of light in the lattice
    public class Photon
    {
        public string Color { get; set; } // e.g., "Red", "Blue", "Green"
        public double Phase { get; set; } // 0-360 degrees
        public string Polarization { get; set; } // e.g., "Horizontal", "Vertical"
        public Guid? EntanglementId { get; set; } // null if not entangled
    }

    // Represents a node in the lattice
    public class LatticeNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<QuantumGate> Gates { get; set; } = new List<QuantumGate>();
        public List<Photon> Photons { get; set; } = new List<Photon>();
    }

    // Abstract base for quantum gates
    public abstract class QuantumGate
    {
        public string Name { get; set; }
        public abstract void Apply(Photon photon, LatticeNode node);
    }

    // Splitter: 50/50 beam splitter
    public class SplitterGate : QuantumGate
    {
        public SplitterGate() { Name = "Splitter"; }
        public override void Apply(Photon photon, LatticeNode node)
        {
            // TODO: Implement 50/50 split logic
        }
    }

    // Phase shifter: adds phase
    public class PhaseShifterGate : QuantumGate
    {
        public double PhaseShift { get; set; } = 90.0; // degrees
        public PhaseShifterGate() { Name = "PhaseShifter"; }
        public override void Apply(Photon photon, LatticeNode node)
        {
            photon.Phase = (photon.Phase + PhaseShift) % 360;
        }
    }

    // Entangler: links two photons
    public class EntanglerGate : QuantumGate
    {
        public EntanglerGate() { Name = "Entangler"; }
        public override void Apply(Photon photon, LatticeNode node)
        {
            // TODO: Implement entanglement logic
        }
    }

    // Polarizer: filters polarization
    public class PolarizerGate : QuantumGate
    {
        public string AllowedPolarization { get; set; } = "Horizontal";
        public PolarizerGate() { Name = "Polarizer"; }
        public override void Apply(Photon photon, LatticeNode node)
        {
            if (photon.Polarization != AllowedPolarization)
            {
                // Remove photon from node if polarization doesn't match
                node.Photons.Remove(photon);
            }
        }
    }

    // Represents the quantum state vector for all photons
    public class Wavefunction
    {
        // TODO: Implement state vector logic
    }

    // Tracks available coherence
    public class CoherenceReservoir
    {
        public double Value { get; set; } = 100.0;
        public void Drain(double amount) => Value = Math.Max(0, Value - amount);
        public void Refill(double amount) => Value = Math.Min(100, Value + amount);
    }

    // Calculates and tracks player score
    public class ScoreSystem
    {
        public int Score { get; set; }
        public void Add(int value) => Score += value;
    }
}
