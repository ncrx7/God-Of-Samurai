using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void EnterState(CharacterManager characterManager)
    {
        Debug.Log("Entering Idle State");
    }

    public void ExitState(CharacterManager characterManager)
    {
        Debug.Log("Exiting Idle State");
    }

    public void UpdateState(CharacterManager characterManager)
    {
        EventSystem.UpdateAnimatorParameterAction?.Invoke(characterManager.networkID, AnimatorValueType.FLOAT, "Vertical", 0, false);

        if(characterManager.isJumping) //!characterManager.isGrounded
        {
            //JUMP
            characterManager.ChangeState(new JumpingState());
        }
        else if(characterManager.isRunning && characterManager.isGrounded)
        {
            characterManager.ChangeState(new RunningState());
        }
        else if(characterManager.isBasicAttacking)
        {
            characterManager.ChangeState(new BasicAttackingState());
        }
        //Eğer zıplama inputu geldiyse change state ile jumping statesine geç
        //characterManager.ChangeState(new JumpingState());
        //Eğer horizontal vertical yürüme değerleri 0 dan büyüksek walking statesine geç
    }
}
