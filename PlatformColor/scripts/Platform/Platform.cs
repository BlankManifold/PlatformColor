using Godot;
using GCs = Godot.Collections;
using CTNI = PlatFormColor.scripts.Components.CTwoNodeInteraction;
using CTNIRes = PlatFormColor.scripts.Resources.CTwoNodeInteractionRes;
using System;

namespace PlatFormColor.scripts.Platform
{
	public partial class Platform : StaticBody2D, Interfaces.IEntityWithProperties, Interfaces.IColorChangeable
	{
		[Export]
		private Shape2D _shape = null;
		protected GCs::Dictionary<Globals.Property, Variant> PropertiesDict = new();
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

			GetNode<CollisionShape2D>("CollisionShape2D").Shape = _shape;


			ColorRect colorRect = GetNode<ColorRect>("ColorRect");
			colorRect.Size = _size;
			colorRect.Position -= _size / 2.0f;

			ChangeColor(_color);
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
		}
		public void SetProperty(Globals.Property property, Variant value)
		{
			if (!PropertiesDict.ContainsKey(property))
				return;

			PropertiesDict[property] = value;
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