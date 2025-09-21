using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace PrismCollapse3D.Core
{
    public static class MovementEngine
    {
        // Move all photons in the lattice by their direction and speed
        public static void MovePhotons(List<LatticeNode3D> lattice, float deltaTime)
        {
            foreach (var node in lattice)
            {
                var photons = node.Photons.ToList();
                foreach (var photon in photons)
                {
                    Vector3 oldPos = photon.Position;
                    photon.Position += photon.Direction * photon.Speed * deltaTime;
                    // Check for collision with other nodes
                    foreach (var otherNode in lattice)
                    {
                        if (otherNode == node) continue;
                        if (Vector3.Distance(photon.Position, otherNode.Position) < 0.1f) // threshold for collision
                        {
                            // Move photon to new node
                            node.Photons.Remove(photon);
                            otherNode.Photons.Add(photon);
                            photon.Position = otherNode.Position;
                            break;
                        }
                    }
                }
            }
        }
    }
}
