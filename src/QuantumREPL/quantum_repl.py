import qutip as qt
import cmd
import numpy as np

class QuantumREPL(cmd.Cmd):
    intro = 'Quantum Emulator REPL. Type help or ? for commands. Type quit to exit.'
    prompt = '> '

    def __init__(self):
        super().__init__()
        self.state = qt.basis(2, 0)  # Start with one qubit |0>
        self.num_qubits = 1

    def do_add_qubit(self, arg):
        """add_qubit: Add a new qubit in |0> state."""
        self.state = qt.tensor(self.state, qt.basis(2, 0))
        self.num_qubits += 1
        print(f'Added qubit. Total qubits: {self.num_qubits}')

    def do_apply(self, arg):
        """apply GATE TARGET_QUBIT: Apply a gate (H, X, Y, Z, S) to a target qubit.
        Example: apply H 0"""
        gate_map = {'H': qt.hadamard_transform(), 'X': qt.sigmax(), 'Y': qt.sigmay(), 'Z': qt.sigmaz(), 'S': qt.phasegate(np.pi/2)}
        
        args = arg.strip().split()
        if len(args) != 2:
            print('Usage: apply GATE TARGET_QUBIT (e.g., apply H 0)')
            return

        gate_name = args[0].upper()
        try:
            target_qubit = int(args[1])
            if not 0 <= target_qubit < self.num_qubits:
                print(f'Error: Target qubit must be between 0 and {self.num_qubits - 1}.')
                return
        except ValueError:
            print('Error: Target qubit must be an integer.')
            return

        if gate_name in gate_map:
            # Create the operator for the full system
            gate_list = [qt.qeye(2)] * self.num_qubits
            gate_list[target_qubit] = gate_map[gate_name]
            op = qt.tensor(gate_list)
            
            self.state = op * self.state
            print(f'Applied {gate_name} to qubit {target_qubit}')
        else:
            print(f'Unknown gate: {gate_name}. Available: H, X, Y, Z, S')

    def do_show(self, arg):
        """show: Print the current state vector and probabilities."""
        print(f'System state ({self.num_qubits} qubits):')
        print(self.state)
        
        amps = self.state.full().flat
        probs = [abs(c)**2 for c in amps]
        
        print('Probabilities:')
        for i, p in enumerate(probs):
            if p > 1e-9: # Only show non-zero probabilities
                # Format the basis state string, e.g., |01⟩ for i=1, num_qubits=2
                basis_state = format(i, f'0{self.num_qubits}b')
                bar = '=' * int(p * 40)
                print(f'|{basis_state}⟩: [{bar:<40}] {p:.3f}')

        if self.num_qubits == 1:
            # Bloch sphere ASCII (simple projection)
            if len(amps) == 2:
                theta = 2 * np.arccos(np.clip(np.abs(amps[0]), 0, 1))
                phi = np.angle(amps[1]) - np.angle(amps[0]) if abs(amps[0]) > 1e-9 else 0
                x = np.sin(theta) * np.cos(phi)
                y = np.sin(theta) * np.sin(phi)
                z = np.cos(theta)
                print('\nBloch vector (qubit 0):')
                print(f'  x: {x:+.2f}  y: {y:+.2f}  z: {z:+.2f}')

    def do_measure(self, arg):
        """measure: Measure all qubits and collapse the state."""
        # Create measurement operators for the full computational basis
        projectors = [qt.ket(format(i, f'0{self.num_qubits}b')).proj() for i in range(2**self.num_qubits)]
        
        outcome, new_state = qt.measurement.measure(self.state, projectors)
        self.state = new_state
        
        measured_basis_state = format(outcome, f'0{self.num_qubits}b')
        print(f'Measured: |{measured_basis_state}⟩. State has collapsed.')

    def do_bell(self, arg):
        """bell: Create a Bell state between the first two qubits."""
        if len(self.qubits) < 2:
            print('Need at least 2 qubits for Bell state. Use add_qubit.')
            return
        h = qt.hadamard_transform()
        # Apply H to qubit 0
        op_h = qt.tensor(h, qt.qeye(2))
        # Apply CNOT with control=0, target=1
        op_cnot = qt.cnot()
        self.state = op_cnot * (op_h * qt.tensor(qt.basis(2,0), qt.basis(2,0)))
        print('Created Bell state |Φ+⟩ = (|00⟩ + |11⟩)/√2')

    def do_quit(self, arg):
        """quit: Exit the REPL."""
        print('Exiting Quantum REPL.')
        return True

    def do_help(self, arg):
        """List available commands."""
        print("\nAvailable commands:")
        print("  add_qubit          - Add a new qubit in the |0> state to the register.")
        print("  apply GATE TARGET  - Apply a quantum gate (H, X, Y, Z, S) to a target qubit index.")
        print("  show               - Display the current quantum state of the register.")
        print("  measure            - Perform a measurement on all qubits, collapsing the state.")
        print("  bell               - (For 2 qubits) Create a standard Bell state.")
        print("  help               - Show this help message.")
        print("  quit               - Exit the emulator.\n")

if __name__ == '__main__':
    QuantumREPL().cmdloop()
