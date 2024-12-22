using Godot;

namespace PlatFormColor.scripts.Interfaces
{
    public interface IPhysicsModifier
    {
        public void Apply(CharacterBody2D characterBody, double delta);
    }
}