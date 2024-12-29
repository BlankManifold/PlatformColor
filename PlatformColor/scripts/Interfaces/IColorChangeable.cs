using Godot;

namespace PlatFormColor.scripts.Interfaces
{
    interface IColorChangeable
    {
        public void ChangeColor(Color color);
        public Color GetColor();
    }
}