using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CWeight : CBase
    {
        [Export]
        protected float _weight = 0f;

        [Export]
        protected Node _controlledNode = null;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode == null)
                _ = warnings.Append<string>("Must assign a CharacterBody2D to apply friction on it.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();
            if (_controlledNode is Interfaces.IEntityWithProperties _controlledEntity)
            {
                _controlledEntity.AddProperty(Globals.Property.Weight, _weight);
            }
            else
            {
                throw new System.Exception($"Cannot add Gravity component because {_controlledNode.Name} is not a IEntityWithProperties.");
            }
        }
        public float GetWeight()
        {
            return _weight;
        }

    }
}