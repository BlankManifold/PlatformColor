using System.Linq;
using Godot;
using SCs = System.Collections.Generic;

namespace PlatFormColor.scripts.Components.Generic
{

    public delegate void Collided<T>(T collider);
    public partial class CReportCollision<T> : CDynamicBase where T : PhysicsBody2D
    {
        [Export]
        protected int _collisionLayer = 0;
        [Export]
        protected bool _reportNoCollision = false;
        [Export]
        protected CharacterBody2D _controlledNode = null;
        private Label _label;

        protected SCs::List<CollisionRestrictions.CollisionRestriction<CharacterBody2D, T>> _genericRestrictions = new();

        public Collided<T> Collided;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode == null)
                _ = warnings.Append<string>("Must assign a CharacterBody2D. It will detect its collision.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();
            _label = GetNode<Label>("Label");
        }
        public CharacterBody2D GetControlledNode()
        {
            return _controlledNode;
        }
        public override void Apply(double delta)
        {
            if (!_active)
                return;

            _label.Text = "";
            if (_controlledNode.GetSlideCollisionCount() == 0)
            {
                _label.Text += "Collider: ";
                if (_reportNoCollision)
                    Collided?.Invoke(null);
                return;
            }

            KinematicCollision2D collision = _controlledNode.GetLastSlideCollision();

            if (collision.GetCollider() is T TCollider)
            {
                if (!TCollider.GetCollisionLayerValue(_collisionLayer))
                    return;

                foreach (CollisionRestrictions.CollisionRestriction<CharacterBody2D, T> restriction in _genericRestrictions)
                {
                    if (!restriction.IsAllowed(_controlledNode, TCollider))
                    {
                        _label.Text += "Collider: NOT ALLOWED";
                        return;
                    }
                }

                Collided?.Invoke(TCollider);
                _label.Text += "Collider: " + TCollider.Name;
            }
        }
    }
}