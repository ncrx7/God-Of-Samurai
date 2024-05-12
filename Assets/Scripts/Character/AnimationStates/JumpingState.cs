using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IState
{
    public void EnterState(CharacterManager characterManager)
    {
        Debug.Log("Entering Jumping State");
        //EventSystem.JumpAction?.Invoke(characterManager.networkID);
    }

    public void ExitState(CharacterManager characterManager)
    {
        Debug.Log("Exiting Jumping State");
    }

    public void UpdateState(CharacterManager characterManager)
    {
        //EventSystem.MovementLocomotionActionOnAir?.Invoke(characterManager.networkID);

        if(characterManager.isGrounded && !characterManager.isRunning)
        {
            characterManager.ChangeState(new IdleState());
        }
        else if(characterManager.isGrounded && characterManager.isRunning)
        {
            characterManager.ChangeState(new RunningState());
        }
        
        //if character is grounded ise
        //characterManager.ChangeState(new IdleState());
    }
}
