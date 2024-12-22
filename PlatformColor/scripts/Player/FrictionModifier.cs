using Godot;

namespace PlatFormColor.scripts.Player
{
    public class FrictionModifier : Interfaces.IPhysicsModifier
    {
        private readonly float _frictionFactor = 0f;

        public FrictionModifier(float frictionFactor = 0f)
        {
            _frictionFactor = frictionFactor;
        }
        public void Apply(CharacterBody2D characterBody, double delta)
        {
            bool movePressed = Input.IsActionPressed("player_move_left") || Input.IsActionPressed("player_move_right");

            if (movePressed)
                return;

            Vector2 velocity = characterBody.Velocity;
            velocity.X = Mathf.MoveToward(characterBody.Velocity.X, 0, _frictionFactor);

            characterBody.Velocity = velocity;
        }
    }
}