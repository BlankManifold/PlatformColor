using Godot;

namespace PlatFormColor.scripts.CollisionRestrictions
{
    public abstract partial class CollisionRestriction<S, T> : Node where T : PhysicsBody2D where S : PhysicsBody2D
    {
        public abstract bool IsAllowed(S controlledNode, T collider);
    }
}