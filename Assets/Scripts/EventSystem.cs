using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem
{
    //Genaral Events
    public static Action<ulong, float, float, float> LocomotionAction;
    public static Action<ulong, string, float> UpdateFloatAnimatorParameterAction;
    public static Action <ulong, string, bool, bool, bool, bool> PlayTargetAnimationAction;

    //Skill Events
    public static Action<ulong> DodgeAction;
}
