using Godot;

namespace PlatFormColor.scripts.Globals
{
    public static class InputChecker
    {
        public static bool JumpPressed(CharacterBody2D characterBody)
        {
            return Input.IsActionJustPressed("player_jump") && characterBody.IsOnFloor();
        }
        public static bool WallJumpPressed(CharacterBody2D characterBody)
        {
            return Input.IsActionJustPressed("player_jump") && (characterBody.IsOnWall() || characterBody.IsOnCeiling());
        }
        public static bool MovePressed()
        {
            return Input.IsActionPressed("player_move_right") || Input.IsActionPressed("player_move_left");
        }
        public static bool MoveNotPressed()
        {
            return !(Input.IsActionPressed("player_move_right") || Input.IsActionPressed("player_move_left"));
        }

    }

}