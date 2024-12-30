using Godot;

namespace PlatFormColor.scripts.Player
{
    public class GravityModifier : Interfaces.IPhysicsModifier
    {
        private CharacterBody2D _controlledNode = null;
        private readonly float _gravityFactor = 1.0f;

        public GravityModifier(CharacterBody2D controlledNode, float gravityFactor = 1.0f)
        {
            _controlledNode = controlledNode;
            _gravityFactor = gravityFactor;
        }
        public void Apply(double delta)
        {
            if (_controlledNode.IsOnFloor())
                return;

            Vector2 velocity = _controlledNode.Velocity;
            velocity += _controlledNode.GetGravity() * _gravityFactor * (float)delta;

            _controlledNode.Velocity = velocity;
        }
    }

}