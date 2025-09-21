using UnityEngine;
using Unity.Mathematics;

namespace PQA.Quantum {
    public abstract class QuantumGate : MonoBehaviour {
        public abstract complex[,] Matrix { get; }
        public virtual void Animate(Qubit qubit) {}
    }
}
