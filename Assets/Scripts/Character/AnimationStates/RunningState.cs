using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState
{
    public void EnterState(CharacterManager characterManager)
    {
        Debug.Log("Entering Running State");
    }

    public void ExitState(CharacterManager characterManager)
    {
        Debug.Log("Exiting Running State");
    }

    public void UpdateState(CharacterManager characterManager)
    {
        EventSystem.MovementLocomotionActionOnGround(characterManager.networkID);

        if (PlayerInputManager.Instance.sprintInput)
        {
            EventSystem.UpdateAnimatorParameterAction?.Invoke(characterManager.networkID, AnimatorValueType.FLOAT, "Vertical", 2, false);
        }
        else
        {
            EventSystem.UpdateAnimatorParameterAction?.Invoke(characterManager.networkID, AnimatorValueType.FLOAT, "Horizontal", 0, false);
            EventSystem.UpdateAnimatorParameterAction?.Invoke(characterManager.networkID, AnimatorValueType.FLOAT, "Vertical", PlayerInputManager.Instance.MoveAmount, false);
        }

        if (!characterManager.isRunning)
        {
            characterManager.ChangeState(new IdleState());
        }
        else if (characterManager.isJumping)
        {
            characterManager.ChangeState(new JumpingState());
        }
        else if(!characterManager.isGrounded)
        {
            characterManager.ChangeState(new FallingState());
        }

    }
}
