using Godot;

namespace PlatFormColor.scripts.Interfaces
{
    public delegate void NotifyValue(float value);
    interface IFrictionChangeable : IHasFriction
    {
        public event NotifyValue FrictionChangedEvent;
        public void ChangeFriction(float frictionFactor);
    }
}