using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CFrictionFactor : CBase
    {
        [Export]
        protected float _frictionFactor = 1f;

        [Export]
        protected Node _controlledNode = null;

        public override void _Ready()
        {
            base._Ready();
            if (_controlledNode is Interfaces.IEntityWithProperties _controlledEntity)
            {
                _controlledEntity.AddProperty(Globals.Property.FrictionFactor, _frictionFactor);
            }
            else
            {
                throw new System.Exception($"Cannot add Fricion component because {_controlledNode.Name} is not a IEntityWithProperties.");
            }
        }
    }
}