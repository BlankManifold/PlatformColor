using Godot;

namespace PlatFormColor.scripts.Interfaces
{
    interface IColorChangeable : IHasColor
    {
        public void ChangeColor(Color color);
    }
}