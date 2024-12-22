using Godot;

namespace PlatFormColor.scripts.Player
{
    public class GravityModifier : Interfaces.IPhysicsModifier
    {
        private readonly float _gravityFactor = 1.0f;

        public GravityModifier(float gravityFactor = 1.0f)
        {
            _gravityFactor = gravityFactor;
        }
        public void Apply(CharacterBody2D characterBody, double delta)
        {
            if (characterBody.IsOnFloor())
                return;

            Vector2 velocity = characterBody.Velocity;
            velocity += characterBody.GetGravity() * _gravityFactor * (float)delta;

            characterBody.Velocity = velocity;
        }
    }

}