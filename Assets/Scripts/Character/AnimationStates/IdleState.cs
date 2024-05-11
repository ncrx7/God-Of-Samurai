using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState
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
        //Eğer zıplama inputu geldiyse change state ile jumping statesine geç
        //characterManager.ChangeState(new JumpingState());
        //Eğer horizontal vertical yürüme değerleri 0 dan büyüksek walking statesine geç
    }
}
