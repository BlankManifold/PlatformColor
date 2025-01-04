using Godot;
using GCs = Godot.Collections;

namespace PlatFormColor.scripts.Player
{
	public delegate void NotifyPlatformCollision(Player player, Platform.Platform platform);
	public partial class Player : CharacterBody2D, Interfaces.IEntityWithProperties
	{
		protected GCs::Dictionary<Globals.Property, Variant> PropertiesDict = new();
		public event NotifyPlatformCollision RequestPlatformHandling;
		protected Managers.StateManager _stateManager;
		protected GCs::Array<Components.CBase> _dynamicComponents = new();
		// protected GCs::Array<Components.CBase> _components = new();

		[Export(PropertyHint.ResourceType)]
		protected Resources.PlayerRes _res = null;

		public override void _Ready()
		{
			base._Ready();

			_stateManager = GetNode<Managers.StateManager>("%StateManager");


			foreach (Node child in GetChildren())
			{
				// _components.Add(component);
				if (child is Components.CDynamicBase dynamicComponent)
				{
					_dynamicComponents.Add(dynamicComponent);
				}
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			GetNode<Label>("Label").Text = _stateManager.GetCurrentStateName();

			foreach (Components.CDynamicBase dynamicComponent in _dynamicComponents)
				dynamicComponent.Apply(delta);

			MoveAndSlide();

			// Platform.Platform platform = GetCollidedPlatform();
			// RequestPlatformHandling?.Invoke(this, platform);
		}

		public Variant? GetProperty(Globals.Property property)
		{
			if (PropertiesDict.TryGetValue(property, out Variant value))
				return value;

			return null;
		}
		public virtual void AddProperty(Globals.Property property, Variant value)
		{
			if (PropertiesDict.ContainsKey(property))
				return;

			PropertiesDict.Add(property, value);
		}
		public virtual void SetProperty(Globals.Property property, Variant value)
		{
			if (!PropertiesDict.ContainsKey(property))
				return;

			PropertiesDict[property] = value;
		}

	}
}