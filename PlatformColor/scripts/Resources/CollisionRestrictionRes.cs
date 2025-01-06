using Godot;

namespace PlatFormColor.scripts.Resources
{
    public abstract partial class CollisionRestrictionRes<S, T> : Resource where T : PhysicsBody2D where S : PhysicsBody2D
    {
        public abstract bool IsAllowed(S controlledNode, T collider);
    }
}