using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{

    public delegate void Collided<T>(T collider);
    public partial class CReportCollision<T> : CBase where T : PhysicsBody2D
    {
        [Export]
        protected int _collisionLayer = 0;

        [Export]
        protected CharacterBody2D _controlledNode = null;

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
                if (TCollider.GetCollisionLayerValue(_collisionLayer))
                    Collided?.Invoke(TCollider);
                GetNode<Label>("Label").Text += "Collider: " + TCollider.Name;
            }
        }
    }
}