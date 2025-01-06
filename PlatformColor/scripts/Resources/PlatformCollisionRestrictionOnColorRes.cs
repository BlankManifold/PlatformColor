using Godot;
using PlatFormColor.scripts.Interfaces;
using Entity = PlatFormColor.scripts.Interfaces.IPropAndResEntity;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class PlatformCollisionRestrictionOnColorRes : PlatformCollisionRestrictionRes
    {
        [Export]
        private Color _allowedColor = new(1, 0, 0, 1);
        private Platform.Platform _lastValidPlatform = null;
        public event NotifyAction RequestReset;

        public override bool IsAllowed(CharacterBody2D controlledNode, Platform.Platform platform)
        {
            if (platform == null)
                return true;

            Entity entity = (controlledNode is Entity) ? (Entity)controlledNode : null;

            if (platform == _lastValidPlatform)
            {
                entity?.UpdateRes();
                return true;
            }

            Variant? colliderColor = platform.GetProperty(Globals.Property.Color);
            if (colliderColor == null || _allowedColor == (Color)colliderColor)
            {
                entity?.UpdateRes();
                _lastValidPlatform = platform;
                return true;
            }
            //TODO Connect reset to LevelManager, put Reset and UpdateRes at Component level 
            // instead of at Resource level
            entity?.Reset();
            return false;
        }
    }
}