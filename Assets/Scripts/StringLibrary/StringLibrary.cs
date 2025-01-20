using UnityEngine;

namespace StringLibrary
{
    public struct ActionNames
    {
        public const string Move = "MOVE";
        public const string Shoot = "SHOOT";
        public const string Spin = "SPIN";
    }
    
    public struct Alerts
    {
        public const string DamageTaken = " damage taken!";
    }
    
    public struct AnimationIDs
    {
        public static readonly int Move = Animator.StringToHash("Move");
        public static readonly int Shoot = Animator.StringToHash("Shoot");
    }

    public struct EditorLabels
    {
        // public const string Animation = "ANIMATION";
        public const string Movement = "MOVEMENT";
    }
    
    public struct Errors
    {
        public const string InstanceExists = "There is already an existing instance of ";
        public const string NoMainCamera = "No MAIN CAMERA found in scene!";
    }
    
    public struct GeneralStrings
    {
        public const string Dash = " - ";
        public const string Empty = "";
        public const string LineEnd = "\n";
        public const string Space = " ";
    }

    public struct UIStrings
    {
        public const string ActionPointsLabel = "ACTION POINTS: ";
        public const string TurnLabel = "TURN: ";
    }

    public struct Warnings
    {
        public const string DeletingExtraInstance = "Deleting extraneous instance!";
    }
}