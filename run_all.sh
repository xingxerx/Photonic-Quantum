#!/bin/bash
cd ../Photonic-Quantum
source venv/bin/activate
pip install -e ./spec-kit
pip install qutip
python src/QuantumREPL/quantum_repl.py