using System;
using System.Numerics;
using System.Collections.Generic;


namespace PQAQuantumSim {
    class Program {
        static void Main(string[] args) {
            // Single-qubit circuit
            var qubit = new Qubit();
            Console.WriteLine($"Initial state: {qubit.StateString()}");
            var gates = new List<QuantumGate> {
                new HadamardGate(),
                new PauliXGate(),
                new PauliYGate(),
                new PauliZGate(),
                new PhaseGate(Math.PI/4)
            };
            int step = 1;
            foreach (var gate in gates) {
                qubit.ApplyGate(gate);
                Console.WriteLine($"After gate {step++} ({gate.Name}): {qubit.StateString()}");
            }
            int measured = Measure(qubit);
            Console.WriteLine($"Measured: {measured}\n");

            // Two-qubit entanglement (Bell state)
            var q0 = new Qubit();
            var q1 = new Qubit();
            q0.ClassicalBitLabel = "control";
            q1.ClassicalBitLabel = "target";
            q0.ApplyGate(new HadamardGate());
            TwoQubitGates.CNOT(q0, q1);
            Console.WriteLine($"Bell state after H and CNOT:");
            Console.WriteLine($"  Q0: {q0.StateString()}\n  Q1: {q1.StateString()}");
            int m0 = Measure(q0);
            int m1 = Measure(q1);
            Console.WriteLine($"Measured: Q0={m0}, Q1={m1}");

            // Classical control: if Q0==1, flip Q1
            if (m0 == 1) {
                q1.ApplyGate(new PauliXGate());
                Console.WriteLine($"Classical control: Q0==1, flipped Q1: {q1.StateString()}");
            } else {
                Console.WriteLine($"Classical control: Q0==0, Q1 unchanged: {q1.StateString()}");
            }
        }

        private static readonly Random s_random = new Random();

        static int Measure(Qubit qubit) {
            double p0 = Math.Pow(qubit.Alpha.Magnitude, 2);
            double r = s_random.NextDouble();
            int bit = (r < p0) ? 0 : 1;
            qubit.CollapseTo(bit);
            return bit;
        }
    }
}
