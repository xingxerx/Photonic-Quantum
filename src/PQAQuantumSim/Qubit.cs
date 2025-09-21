using System.Numerics;

namespace PQAQuantumSim {
    public class Qubit {
        public Complex Alpha { get; private set; } = new Complex(1, 0);
        public Complex Beta { get; private set; } = new Complex(0, 0);
        public string ClassicalBitLabel { get; set; } = "";

        public Qubit(Complex? alpha = null, Complex? beta = null) {
            if (alpha != null && beta != null) {
                Alpha = alpha.Value;
                Beta = beta.Value;
                Normalize();
            }
        }

        public void ApplyGate(QuantumGate gate) {
            var m = gate.Matrix;
            var newAlpha = m[0,0] * Alpha + m[0,1] * Beta;
            var newBeta = m[1,0] * Alpha + m[1,1] * Beta;
            Alpha = newAlpha;
            Beta = newBeta;
            Normalize();
        }

        private void Normalize() {
            var norm = System.Math.Sqrt((Alpha * Complex.Conjugate(Alpha)).Real + (Beta * Complex.Conjugate(Beta)).Real);
            Alpha /= norm;
            Beta /= norm;
        }

        public void CollapseTo(int bit) {
            if (bit == 0) {
                Alpha = Complex.One;
                Beta = Complex.Zero;
            } else {
                Alpha = Complex.Zero;
                Beta = Complex.One;
            }
        }

        public string StateString() {
            return $"|ψ⟩ = {Alpha} |0⟩ + {Beta} |1⟩" + (string.IsNullOrEmpty(ClassicalBitLabel) ? "" : $" [{ClassicalBitLabel}]");
        }

        public override string ToString() {
            return StateString();
        }
    }
}
