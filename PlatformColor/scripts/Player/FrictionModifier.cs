using Godot;

namespace PlatFormColor.scripts.Player
{
    public class FrictionModifier : Interfaces.IPhysicsModifier
    {
        private float _friction = 0f;
        private CharacterBody2D _controlledNode = null;

        public FrictionModifier(CharacterBody2D controlledNode)
        {
            if (controlledNode is Interfaces.IHasFriction controlledNodeWithFriction)
            {
                _controlledNode = controlledNode;
                _friction = controlledNodeWithFriction.GetFriction();
            }
            else
            {
                throw new System.Exception("CharacterBody2D is not a IHasFriction");
            }

            if (_controlledNode is Interfaces.IFrictionChangeable frictionChangeableNode)
                frictionChangeableNode.FrictionChangedEvent += _OnFrictionChanged;
        }
        public void Apply(double delta)
        {
            bool movePressed = Input.IsActionPressed("player_move_left") || Input.IsActionPressed("player_move_right");

            if (movePressed)
                return;

            Vector2 velocity = _controlledNode.Velocity;
            velocity.X = Mathf.MoveToward(_controlledNode.Velocity.X, 0, _friction);

            _controlledNode.Velocity = velocity;
        }

        private void _OnFrictionChanged(float frictionFactor)
        {
            _friction = ((Interfaces.IHasFriction)_controlledNode).GetFriction() * frictionFactor;
        }
    }
}