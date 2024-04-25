using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem
{
    public static Action<ulong, float, float, float> LocomotionAction;
    public static Action<ulong, string, float> UpdateFloatAnimatorParameterAction;
}
