using Godot;
using SCs = System.Collections.Generic;

namespace PlatFormColor.scripts.Player
{
	public delegate void NotifyPlatformCollision(Player player, Platform.Platform platform);
	public partial class Player : CharacterBody2D, Interfaces.IColorChangeable, Interfaces.IHasFriction, Interfaces.IHasWeight
	{
		public event NotifyPlatformCollision RequestPlatformHandling;
		protected SCs::List<Interfaces.IPhysicsModifier> _physicsModifierList = new();
		protected Managers.StateManager _stateManager;

		[Export(PropertyHint.ResourceType)]
		protected Resources.PlayerRes _res = null;

		public override void _Ready()
		{
			base._Ready();

			_stateManager = GetNode<Managers.StateManager>("%StateManager");

			_physicsModifierList.Add(new GravityModifier(this));
			_physicsModifierList.Add(new FrictionModifier(this));
		}

		public override void _PhysicsProcess(double delta)
		{
			GetNode<Label>("Label").Text = _stateManager.GetCurrentStateName();

			foreach (var modifier in _physicsModifierList)
				modifier.Apply(delta);

			MoveAndSlide();

			Platform.Platform platform = GetCollidedPlatform();
			RequestPlatformHandling?.Invoke(this, platform);
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
			if (GetSlideCollisionCount() == 0)
				return null;

			KinematicCollision2D collision = GetLastSlideCollision();
			if (collision.GetCollider() is Platform.Platform platform)
			{
				return platform;
			}
			else
			{
				return null;
			}
		}
	}
}