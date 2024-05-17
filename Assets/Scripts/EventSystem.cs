using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem
{
    //Genaral Events
    public static Action<ulong> MovementLocomotionActionOnGround;
    public static Action <ulong> MovementLocomotionActionOnAir;
    public static Action<ulong, AnimatorValueType, string, float, bool> UpdateAnimatorParameterAction;
    public static Action <ulong, string, bool, bool, bool, bool> PlayTargetAnimationAction;
    public static Action <ulong, string[], float> SetAnimationSpeedAction;

    //Skill Events
    public static Action<ulong> DodgeAction;
    public static Action <ulong> SprintAction;
    public static Action <ulong> JumpAction;
    public static Action <ulong, int> OnSkillButtonPressed;
}
