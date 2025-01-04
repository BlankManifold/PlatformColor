using Godot;
using GCs = Godot.Collections;
using CTNI = PlatFormColor.scripts.Components.CTwoNodeInteraction;
using CTNIRes = PlatFormColor.scripts.Resources.CTwoNodeInteractionRes;
using System;

namespace PlatFormColor.scripts.Platform
{
	public delegate void NotifyPlayerInteraction(Platform platform, Player.Player player);
	public partial class Platform : StaticBody2D, Interfaces.IColorChangeable
	{
		[Export]
		private Shape2D _shape = null;
		[Export]
		private GCs::Dictionary<Globals.InteractiveProperty, Variant> _interactivePropertiesDict = new();
		[Export]
		private Vector2 _size = new(50, 50);
		public Vector2 Size { get { return _size; } }
		[Export]
		private Color _color = new(1, 1, 1, 1);

		[Export(PropertyHint.ResourceType)]
		private GCs::Array<CTNIRes> _interactionComponentsRes = new();
		private GCs::Array<CTNI> _interactionComponents = new();
		public GCs::Array<CTNI> InteractionComponents
		{
			get { return _interactionComponents; }
		}

		public override void _Ready()
		{
			AddToGroup("platform");
			_AddComponents();

			RectangleShape2D shape = (RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape.Duplicate();
			GetNode<CollisionShape2D>("CollisionShape2D").Shape = shape;

			shape.Size = _size;

			ColorRect colorRect = GetNode<ColorRect>("ColorRect");
			colorRect.Size = _size;
			colorRect.Position -= _size / 2.0f;

			ChangeColor(_color);
		}
		private void _AddComponents()
		{
			foreach (CTNIRes componentRes in _interactionComponentsRes)
			{
				CTNI componentNode = componentRes.CreateComponent();
				componentNode.AssignParent(this);
				_interactionComponents.Add(componentNode);
				GetNode<Node>("%PICs").AddChild(componentNode);
			}
		}

		public Variant? GetInteractiveProperties(Globals.InteractiveProperty property)
		{
			if (_interactivePropertiesDict.TryGetValue(property, out Variant value))
				return value;

			return null;
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