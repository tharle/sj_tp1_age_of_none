using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameParameters
{
    public class AnimationPlayer
    {
        public const string FLOAT_VELOCITY_Y = "velocity_y";
        public const string BOOL_DOUBLE_JUMP = "double_jump";
        public const string TRIGGER_DIE = "die";
    }

    public class InputName
    {
        public const string AXIS_HORIZONTAL = "Horizontal";
        public const string KEY_JUMP = "space";
    }

    public class LayerName
    {
        public const string PLATAFORM = "Plataform";
        public const string DESTROYER = "Destroyer";       

    }
    public class PlataformName
    {
        public const string START = "Start";
        public const string END = "End";
    }

    public class SceneName
    {
        public const string MAIN_MENU = "MainMenu";
        public const string GAME = "Game";
    }

    public class TagName {
        public const string PLAYER = "Player";
        public const string GROUND = "Ground";
    }
}
