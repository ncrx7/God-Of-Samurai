using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : MonoBehaviour, IState
{
    public void EnterState(CharacterManager characterManager)
    {
        Debug.Log("Entering Jumping State");
    }

    public void ExitState(CharacterManager characterManager)
    {
        Debug.Log("Exiting Idle State");
    }

    public void UpdateState(CharacterManager characterManager)
    {
        //if character is grounded ise
        //characterManager.ChangeState(new IdleState());
    }
}
