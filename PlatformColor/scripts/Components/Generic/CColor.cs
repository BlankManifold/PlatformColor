using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CColor : CBase
    {
        [Export]
        protected Color _color;

        [Export]
        protected Node _controlledNode = null;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode == null)
                _ = warnings.Append<string>("Must assign a Node that implements IEntityWithProperties interface to add color to it.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();
            if (_controlledNode is Interfaces.IEntityWithProperties _controlledEntity)
            {
                _controlledEntity.AddProperty(Globals.Property.Color, _color);
            }
            else
            {
                throw new System.Exception($"Cannot add Color component because {_controlledNode.Name} is not a IEntityWithProperties.");
            }
        }
        public Color GetColor()
        {
            return _color;
        }

    }
}