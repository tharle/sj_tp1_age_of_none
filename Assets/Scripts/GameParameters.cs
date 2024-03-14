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

    public class BundleExtension
    {
        public static readonly string[] SFX = {"mp3", "wav"};
    }

    public class BundleNames
    {
        public const string SFX = "sfx";
        public const string SPRITE_STAMP = "sprite_stamp";
        public const string PREFAB_LEVEL = "prefab_level";
    }

    public class BundlePath
    {
        public const string RESOURCES_RANK = "Sprites/stamp_mark/";
        public const string BUNDLE_ASSETS_INPUT = "Assets/BundleAssets";
        public const string BUNDLE_ASSETS_OUTPUT = "Assets/StreamingAssets";
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
