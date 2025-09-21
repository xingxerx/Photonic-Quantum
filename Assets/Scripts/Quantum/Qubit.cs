using UnityEngine;
using Unity.Mathematics;

namespace PQA.Quantum {
    public class Qubit : MonoBehaviour {
        public complex StateAlpha = new complex(1, 0);
        public complex StateBeta = new complex(0, 0);

        public void ApplyGate(QuantumGate gate) {
            var newAlpha = gate.Matrix[0,0] * StateAlpha + gate.Matrix[0,1] * StateBeta;
            var newBeta = gate.Matrix[1,0] * StateAlpha + gate.Matrix[1,1] * StateBeta;
            StateAlpha = newAlpha; StateBeta = newBeta;
            Normalize();
        }

        private void Normalize() {
            var norm = math.sqrt(math.abs(StateAlpha * math.conj(StateAlpha)) + math.abs(StateBeta * math.conj(StateBeta)));
            StateAlpha /= norm; StateBeta /= norm;
        }
    }
}
