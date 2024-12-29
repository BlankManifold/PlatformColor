using Godot;

namespace PlatFormColor.scripts.Platform
{
	public partial class Platform : StaticBody2D, Interfaces.IColorChangeable
	{
		public override void _Ready()
		{
			ColorRect rect = GetNode<ColorRect>("ColorRect");
			RectangleShape2D shape = (RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape;

			rect.Size = shape.Size;
			rect.Position -= rect.Size / 2.0f;
		}

		public void ChangeColor(Color color)
		{
			GetNode<ColorRect>("ColorRect").Color = color;
		}
		public Color GetColor()
		{
			return GetNode<ColorRect>("ColorRect").Color;
		}
	}
}