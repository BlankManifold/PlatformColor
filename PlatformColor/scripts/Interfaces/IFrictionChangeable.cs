using Godot;

namespace PlatFormColor.scripts.Interfaces
{
    interface IFrictionChangeable : IHasFriction
    {
        public void ChangeColor(Color color);
    }
}