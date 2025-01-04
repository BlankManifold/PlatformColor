using Godot;
using GCs = Godot.Collections;


namespace PlatFormColor.scripts.Platform
{
	public partial class Platform : StaticBody2D, Interfaces.IEntityWithProperties
	{
		[Export]
		private Shape2D _shape = null;
		protected GCs::Dictionary<Globals.Property, Variant> PropertiesDict = new();
		[Export]
		private Vector2 _size = new(50, 50);
		public Vector2 Size { get { return _size; } }

		public override void _Ready()
		{
			AddToGroup("platform");

			GetNode<CollisionShape2D>("CollisionShape2D").Shape = _shape;

			ColorRect colorRect = GetNode<ColorRect>("ColorRect");
			colorRect.Size = _size;
			colorRect.Position -= _size / 2.0f;
		}
		public Variant? GetProperty(Globals.Property property)
		{
			if (PropertiesDict.TryGetValue(property, out Variant value))
				return value;

			return null;
		}
		public void AddProperty(Globals.Property property, Variant value)
		{
			if (PropertiesDict.ContainsKey(property))
				return;

			PropertiesDict.Add(property, value);

			if (property is Globals.Property.Color)
				GetNode<ColorRect>("ColorRect").Color = (Color)value;
		}
		public void SetProperty(Globals.Property property, Variant value)
		{
			if (!PropertiesDict.ContainsKey(property))
				return;

			PropertiesDict[property] = value;

			if (property is Globals.Property.Color)
				GetNode<ColorRect>("ColorRect").Color = (Color)value;
		}

	}
}