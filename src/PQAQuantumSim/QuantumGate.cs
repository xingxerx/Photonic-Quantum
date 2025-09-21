using System.Numerics;

namespace PQAQuantumSim {
    public abstract class QuantumGate {
        public abstract Complex[,] Matrix { get; }
        public virtual string Name => GetType().Name;
    }

    public class HadamardGate : QuantumGate {
        private static readonly Complex s = 1.0 / System.Math.Sqrt(2);
        public override Complex[,] Matrix => new Complex[,] {
            { s, s },
            { s, -s }
        };
        public override string Name => "Hadamard";
    }

    public class PauliXGate : QuantumGate {
        public override Complex[,] Matrix => new Complex[,] {
            { 0, 1 },
            { 1, 0 }
        };
        public override string Name => "Pauli-X";
    }

    public class PhaseGate : QuantumGate {
        private readonly Complex phase;
        public PhaseGate(double theta) { phase = Complex.FromPolarCoordinates(1, theta); }
        public override Complex[,] Matrix => new Complex[,] {
            { 1, 0 },
            { 0, phase }
        };
        public override string Name => $"Phase({phase.Phase})";
    }

    public class PauliYGate : QuantumGate {
        public override Complex[,] Matrix => new Complex[,] {
            { 0, -Complex.ImaginaryOne },
            { Complex.ImaginaryOne, 0 }
        };
        public override string Name => "Pauli-Y";
    }

    public class PauliZGate : QuantumGate {
        public override Complex[,] Matrix => new Complex[,] {
            { 1, 0 },
            { 0, -1 }
        };
        public override string Name => "Pauli-Z";
    }

    public class CNOTGate : QuantumGate {
        // CNOT is a 4x4 matrix for 2-qubit systems
        public override Complex[,] Matrix => new Complex[,] {
            { 1, 0, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 0, 0, 1 },
            { 0, 0, 1, 0 }
        };
        public override string Name => "CNOT";
    }
}
