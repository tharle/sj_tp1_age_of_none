using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameParameters
{
    public class TypeWriteConfiguration
    {
        public const char BREAK_LINE = '|';
    }

    public class AnimationPlayer
    {
        public const string FLOAT_VELOCITY = "velocity";
    }

    public class AnimationMenu
    {
        public const string TRIGGER_OPEN = "Open";
        public const string TRIGGER_CLOSE = "Close";
    }

    public class Directory
    {
        public const string RESOURCES_RANK = "Sprites/stamp_mark/";
    }

    public class InputName
    {
        public const string AXIS_HORIZONTAL = "Horizontal";
        public const KeyCode NEXT_TEXT = KeyCode.Space;
    }

    public class PlatformName
    {
        public const string START = "Start";
        public const string END = "End";
        public const string SPOT = "Spot";
    }

    public class SceneName
    {
        public const string MAIN_MENU = "MainMenu";
        public const string GAME = "Game";
    }

    public class TagName {
        public const string PLAYER = "Player";
        public const string GROUND = "Ground";
        public const string SPOT = "Spot";
    }
}
