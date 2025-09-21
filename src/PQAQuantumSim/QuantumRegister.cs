using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace PQAQuantumSim
{
    public class QuantumRegister
    {
        // State vector for N qubits, has 2^N elements.
        // |00...0⟩, |00...1⟩, ..., |11...1⟩
        public Complex[] StateVector { get; private set; }
        public int QubitCount { get; }
        private readonly Qubit[] _qubits;

        public QuantumRegister(params Qubit[] qubits)
        {
            if (qubits.Length == 0)
            {
                throw new ArgumentException("QuantumRegister must have at least one qubit.");
            }
            _qubits = qubits;
            QubitCount = qubits.Length;
            int stateCount = 1 << QubitCount; // 2^N
            StateVector = new Complex[stateCount];

            // Initialize to the tensor product of the individual qubit states.
            // For new qubits, this will be |0...0⟩ state.
            InitializeStateVector();
        }

        private void InitializeStateVector()
        {
            StateVector = new Complex[1 << QubitCount];
            StateVector[0] = Complex.One;
            for (int i = 0; i < QubitCount; i++)
            {
                var tempVector = new Complex[1 << QubitCount];
                int block_size = 1 << (QubitCount - 1 - i);
                for (int j = 0; j < (1 << QubitCount); j++)
                {
                    int block_idx = j / block_size;
                    int in_block_idx = j % block_size;
                    int k = (block_idx / 2) * block_size + in_block_idx;
                    if (block_idx % 2 == 0)
                    {
                        tempVector[j] = StateVector[k] * _qubits[i].Alpha;
                    }
                    else
                    {
                        tempVector[j] = StateVector[k] * _qubits[i].Beta;
                    }
                }
                StateVector = tempVector;
            }
            Normalize();
        }

        public void ApplyGate(QuantumGate gate, int targetQubit)
        {
            // This is a simplified version for single-qubit gates on a register.
            // A full implementation would use tensor products.
            _qubits[targetQubit].ApplyGate(gate);
            // Re-initialize the state vector from the modified qubits.
            InitializeStateVector();
        }

        public void ApplyCNOT(int controlQubit, int targetQubit)
        {
            var cnot = new CNOTGate().Matrix;
            var newStateVector = new Complex[StateVector.Length];
            
            // This is a simplified application assuming qubits are adjacent for matrix math.
            // A proper implementation needs to handle arbitrary qubit positions.
            // For |c⟩|t⟩, the state is αβ -> (α₀₀, α₀₁, α₁₀, α₁₁)
            // This example assumes control=0, target=1 for a 2-qubit register.
            if (QubitCount == 2 && controlQubit == 0 && targetQubit == 1)
            {
                newStateVector[0] = cnot[0, 0] * StateVector[0] + cnot[0, 2] * StateVector[2]; // |00>
                newStateVector[1] = cnot[1, 1] * StateVector[1] + cnot[1, 3] * StateVector[3]; // |01>
                newStateVector[2] = cnot[2, 0] * StateVector[0] + cnot[2, 2] * StateVector[2]; // |10> -> flips with |11>
                newStateVector[3] = cnot[3, 1] * StateVector[1] + cnot[3, 3] * StateVector[3]; // |11> -> flips with |10>
                
                // The CNOT matrix flips the target if control is 1.
                // State |10> becomes |11> and |11> becomes |10>.
                (StateVector[2], StateVector[3]) = (StateVector[3], StateVector[2]);
            }
            else
            {
                // For simplicity, we'll just show a message for non-handled cases.
                Console.WriteLine("Warning: CNOT currently only implemented for a 2-qubit register with control=0, target=1.");
            }
            Normalize();
        }

        private void Normalize()
        {
            double norm = Math.Sqrt(StateVector.Sum(c => c.Magnitude * c.Magnitude));
            for (int i = 0; i < StateVector.Length; i++) StateVector[i] /= norm;
        }
    }
}