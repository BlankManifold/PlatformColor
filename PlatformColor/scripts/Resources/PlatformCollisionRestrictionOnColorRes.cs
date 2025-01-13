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
        [Export]
        private bool _onlyOnFloor = true;
        private Platform.Platform _lastValidPlatform = null;
        //public event NotifyAction RequestReset;

        //TODO problema: a volte mi fa  reset in posizione che non è floor, loop di reset
        public override bool IsAllowed(CharacterBody2D controlledNode, Platform.Platform platform)
        {
            if (platform == null)
                return true;
            if (_onlyOnFloor && !controlledNode.IsOnFloor())
                return true;

            Entity entity = (controlledNode is Entity) ? (Entity)controlledNode : null;

            if (platform == _lastValidPlatform)
            {
                //TODO mettere dei checkpoint per ogni platform (un array di checkpoint)
                // ritorna all'ultimo passsato, piu controllo sul respawn
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