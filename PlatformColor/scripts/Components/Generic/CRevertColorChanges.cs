using Godot;
using System.Linq;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CRevertColorChanges : CBase
    {
        [Export]
        protected CColor _CColor = null;
        [Export]
        protected Node2D _controlledNode = null;
        [Export]
        protected double _revertWaitTime = 2f;
        protected Interfaces.IPropAndResEntity _controlledEntity = null;
        private Color _color;
        private Timer _revertTimer;
        private Label _label;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode is not Interfaces.IPropAndResEntity)
                _ = warnings.Append<string>(
                    "Must assign a Node2D that implements IPropAndResEntity interface to change color to it."
                    );
            if (_CColor == null)
                _ = warnings.Append<string>("Must assign a CColor component to listen color changes.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();

            _revertTimer = GetNode<Timer>("Timer");
            _revertTimer.WaitTime = _revertWaitTime;
            _revertTimer.Timeout += _OnRevertTimerTimeOut;

            _color = _CColor.GetColor();
            _CColor.ChangedColor += _OnChangedColor;

            if (_controlledNode is Interfaces.IPropAndResEntity properties)
            {
                _controlledEntity = properties;
            }
            else
            {
                throw new System.Exception("ControlledNode is not a IPropAndResEntity.");
            }

            _label = GetNode<Label>("Label");
            _label.Text = _revertTimer.WaitTime.ToString();
            _label.GlobalPosition = _controlledNode.GlobalPosition;
        }

        public override void _PhysicsProcess(double delta)
        {
            _label.Text = _revertTimer.TimeLeft.ToString("0.00");
        }
        private void _OnChangedColor(Color color)
        {
            if (color == _color)
                return;

            _revertTimer.Start();
        }
        private void _OnRevertTimerTimeOut()
        {
            _controlledEntity.UpdateRes();
            _controlledEntity.SetProperty(Globals.Property.Color, _color);
            _label.Text = _revertTimer.WaitTime.ToString();
        }

    }

}