using Godot;
using GCs = Godot.Collections;


namespace PlatFormColor.scripts.Platform
{
	public partial class Platform : StaticBody2D, Interfaces.IPropAndResEntity
	{
		[Export]
		private Shape2D _shape = null;
		[Export]
		private Vector2 _size = new(50, 50);
		public Vector2 Size { get { return _size; } }

		[Export(PropertyHint.ResourceType)]
		protected Resources.PlatformRes _res;

		protected GCs::Array<Components.CDynamicBase> _dynamicComponents = new();
		protected GCs::Dictionary<Globals.Property, Variant> PropertiesDict = new();

		public override void _Ready()
		{
			AddToGroup("platform");

			GetNode<CollisionShape2D>("CollisionShape2D").Shape = _shape;

			ColorRect colorRect = GetNode<ColorRect>("ColorRect");
			colorRect.Size = _size;
			colorRect.Position -= _size / 2.0f;

			foreach (Node child in GetChildren())
			{
				// _components.Add(component);
				if (child is Components.CDynamicBase dynamicComponent)
					_dynamicComponents.Add(dynamicComponent);
			}

			_res ??= new Resources.PlatformRes();
			CallDeferred(MethodName.UpdateRes);
		}
		public override void _PhysicsProcess(double delta)
		{
			foreach (Components.CDynamicBase dynamicComponent in _dynamicComponents)
				dynamicComponent.Apply(delta);

			// Platform.Platform platform = GetCollidedPlatform();
			// RequestPlatformHandling?.Invoke(this, platform);
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
		public void LoadRes(Resources.PlatformRes res)
		{
			PropertiesDict = res.PropertiesDict;
			GlobalPosition = res.GlobalPosition;
		}
		public void UpdateRes()
		{
			_res.PropertiesDict = PropertiesDict;
			_res.GlobalPosition = GlobalPosition;
		}
		public void Reset()
		{
			PropertiesDict = new();
			foreach (var item in _res.PropertiesDict)
				AddProperty(item.Key, item.Value);

			GlobalPosition = _res.GlobalPosition;

		}
	}
}