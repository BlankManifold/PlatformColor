using Godot;
using Entity = PlatFormColor.scripts.Interfaces.IPropAndResEntity;

namespace PlatFormColor.scripts.CollisionRestrictions
{
    [GlobalClass]
    public partial class PlatformCollisionRestrictionOnColor : PlatformCollisionRestriction
    {
        [Export]
        private bool _onlyOnFloor = true;
        [Export]
        private Components.Generic.CColor _CColor = null;
        [Export]
        private int _colorIndex = 0;
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
            if (colliderColor == null || _CColor.GetColor(_colorIndex) == (Color)colliderColor)
            {
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