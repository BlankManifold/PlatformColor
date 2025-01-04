using Godot;
using SCs = System.Collections.Generic;
using GCs = Godot.Collections;

namespace PlatFormColor.scripts.Player
{
	public delegate void NotifyPlatformCollision(Player player, Platform.Platform platform);
	public partial class Player : CharacterBody2D, Interfaces.IColorChangeable, Interfaces.IHasFriction, Interfaces.IHasWeight
	{
		public event NotifyPlatformCollision RequestPlatformHandling;
		protected Managers.StateManager _stateManager;
		protected GCs::Array<Components.CBase> _components = new();

		[Export(PropertyHint.ResourceType)]
		protected Resources.PlayerRes _res = null;

		public override void _Ready()
		{
			base._Ready();

			_stateManager = GetNode<Managers.StateManager>("%StateManager");


			foreach (Node child in GetChildren())
			{
				if (child is Components.CBase component)
					_components.Add(component);
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			GetNode<Label>("Label").Text = _stateManager.GetCurrentStateName();

			foreach (Components.CBase component in _components)
				component.Apply(delta);

			MoveAndSlide();

			// Platform.Platform platform = GetCollidedPlatform();
			// RequestPlatformHandling?.Invoke(this, platform);
		}

		public virtual void ChangeColor(Color color)
		{
			return;
		}
		public virtual Color GetColor()
		{
			return new Color();
		}
		public float GetFriction()
		{
			return _res.GetGlobalPhysicsProperty("Friction");
		}
		public float GetWeight()
		{
			return _res.GetGlobalPhysicsProperty("Weight");
		}

		private Platform.Platform GetCollidedPlatform()
		{
			GetNode<Label>("Label").Text += "\n" + GetSlideCollisionCount();

			if (GetSlideCollisionCount() == 0)
				return null;


			KinematicCollision2D collision = GetLastSlideCollision();
			if (collision.GetCollider() is Platform.Platform platform)
			{
				GetNode<Label>("Label").Text += "\n platform " + platform.Name;
				return platform;
			}
			else
			{
				GetNode<Label>("Label").Text += "\n not platform";
				return null;
			}
		}
	}
}