using Godot;
using SCs = System.Collections.Generic;

namespace PlatFormColor.scripts.Player
{
	public partial class Player : CharacterBody2D
	{
		protected SCs::List<Interfaces.IPhysicsModifier> _physicsModifierList = new();
		protected Managers.StateManager _stateManager;

		[Export(PropertyHint.ResourceType)]
		protected Resources.PlayerRes _res = null;

		public override void _Ready()
		{
			base._Ready();

			_stateManager = GetNode<Managers.StateManager>("%StateManager");
			_physicsModifierList.Add(new GravityModifier(_res.GetGlobalPhysicsProperty("Gravity")));
			_physicsModifierList.Add(new FrictionModifier(_res.GetGlobalPhysicsProperty("Friction")));
		}
		public override void _PhysicsProcess(double delta)
		{
			GetNode<Label>("Label").Text = _stateManager.GetCurrentStateName();
			GetNode<Label>("Label").Text += "\n" + Input.GetActionStrength("player_move_left");
			GetNode<Label>("Label").Text += "\n" + Input.GetActionStrength("player_move_right");
			GetNode<Label>("Label").Text += "\n" + Mathf.Sign(Input.GetAxis("player_move_left", "player_move_right"));

			foreach (var modifier in _physicsModifierList)
				modifier.Apply(this, delta);

			MoveAndSlide();
		}
	}
}