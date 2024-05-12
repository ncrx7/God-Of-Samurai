using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IState
{
    public void EnterState(CharacterManager characterManager)
    {
        Debug.Log("Entering Falling State");
    }

    public void ExitState(CharacterManager characterManager)
    {
        Debug.Log("Exiting Falling State");
    }

    public void UpdateState(CharacterManager characterManager)
    {
        throw new System.NotImplementedException();
    }
}
