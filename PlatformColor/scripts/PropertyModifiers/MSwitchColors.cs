using Godot;

namespace PlatFormColor.scripts.PropertyModifiers
{
    public partial class MSwitchColors : MBase
    {
        public override void _Ready()
        {
            base._Ready();
        }

        protected override void _OnBodyEntered(Node2D body)
        {
            if (body is Interfaces.IEntityWithProperties entity)
            {
                if (!entity.IsPropertyChangeable(Globals.Property.Color))
                    return;

                Godot.Collections.Array<Color> colors = (Godot.Collections.Array<Color>)entity.GetProperty(Globals.Property.Colors);
                Godot.Collections.Array<Color> tempColors = colors.Duplicate();
                for (int i = 0; i < colors.Count; i++)
                {
                    colors[(i + 1) % colors.Count] = tempColors[i];
                }
                entity.SetProperty(Globals.Property.Colors, colors);
                // entity.SetProperty(Globals.Property.Color, colors[0]);
            }
        }
        protected override void _OnBodyExited(Node2D body)
        {
        }
    }

}