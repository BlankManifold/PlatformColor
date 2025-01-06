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
        protected CharacterBody2D _controlledNode = null;

        protected SCs::List<Resources.CollisionRestrictionRes<CharacterBody2D, T>> _genericRestrictions = new();

        public Collided<T> Collided;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode == null)
                _ = warnings.Append<string>("Must assign a CharacterBody2D. It will detect its collision.");

            return warnings;
        }

        public override void Apply(double delta)
        {
            if (!_active)
                return;

            GetNode<Label>("Label").Text = "";
            if (_controlledNode.GetSlideCollisionCount() == 0)
            {
                GetNode<Label>("Label").Text += "Collider: ";
                Collided?.Invoke(null);
                return;
            }

            KinematicCollision2D collision = _controlledNode.GetLastSlideCollision();

            if (collision.GetCollider() is T TCollider)
            {
                if (!TCollider.GetCollisionLayerValue(_collisionLayer))
                    return;

                foreach (Resources.CollisionRestrictionRes<CharacterBody2D, T> restriction in _genericRestrictions)
                {
                    if (!restriction.IsAllowed(_controlledNode, TCollider))
                    {
                        GetNode<Label>("Label").Text += "Collider: NOT ALLOWED";
                        return;
                    }
                }

                Collided?.Invoke(TCollider);
                GetNode<Label>("Label").Text += "Collider: " + TCollider.Name;
            }
        }
    }
}