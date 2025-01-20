using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CMultipleColors : CColor
    {
        [Export]
        private Godot.Collections.Array<Color> Colors = new();

        public override void _Ready()
        {
            if (_color == new Color(0, 0, 0, 0))
                _color = Colors[0];

            if (_controlledNode is Interfaces.IEntityWithProperties _controlledEntity)
            {
                _controlledEntity.AddProperty(Globals.Property.Color, _color, _isChangeable);
                _controlledEntity.AddProperty(Globals.Property.Colors, Colors, _isChangeable);
                _controlledEntity.SettingProperty += _OnSettingsProperty;
            }
            else
            {
                throw new System.Exception($"Cannot add Color component because {_controlledNode.Name} is not a IEntityWithProperties.");
            }
        }
        public override Color GetColor(int index = 0) => Colors[index];
        protected override void _OnSettingsProperty(Globals.Property property, Variant value)
        {
            if (property is Globals.Property.Color)
            {
                Color color = (Color)value;
                _color = color;
                Colors[0] = color;
                EmitSignal(SignalName.ChangedColor, _color);
            }
            else if (property is Globals.Property.Colors)
            {
                Colors = (Godot.Collections.Array<Color>)value;
                _color = Colors[0];
                EmitSignal(SignalName.ChangedColor, _color);
            }
        }

    }
}