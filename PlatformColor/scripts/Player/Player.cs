using Godot;
using SCs = System.Collections.Generic;

namespace PlatFormColor.scripts.Player
{
	public partial class Player : CharacterBody2D, Interfaces.IColorChangeable, Interfaces.IFrictionChangeable
	{
		protected SCs::List<Interfaces.IPhysicsModifier> _physicsModifierList = new();
		protected SCs::List<Interfaces.IPlatformModifier> _platformModifierList = new();
		protected Managers.StateManager _stateManager;

		[Export(PropertyHint.ResourceType)]
		protected Resources.PlayerRes _res = null;

		public override void _Ready()
		{
			base._Ready();

			_stateManager = GetNode<Managers.StateManager>("%StateManager");
			_physicsModifierList.Add(new GravityModifier(_res.GetGlobalPhysicsProperty("Gravity")));
			_physicsModifierList.Add(new FrictionModifier(_res.GetGlobalPhysicsProperty("Friction")));
			_platformModifierList.Add(new PlatformColorModifier());
			_platformModifierList.Add(new PlatformFrictionModifier());
		}

		public override void _PhysicsProcess(double delta)
		{
			GetNode<Label>("Label").Text = _stateManager.GetCurrentStateName();

			foreach (var modifier in _physicsModifierList)
				modifier.Apply(this, delta);

			Platform.Platform platform = GetCollidedPlatform();
			if (platform != null)
			{
				foreach (var modifier in _platformModifierList)
					modifier.Apply(this, platform, delta);
			}

			MoveAndSlide();
		}

		public virtual void ChangeColor(Color color)
		{
			return;
		}
		public virtual Color GetColor()
		{
			return new Color();
		}
		public virtual void ChangeFriction(float friction)
		{

			return;
		}
		public virtual float GetFriction()
		{
			return 0f;
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