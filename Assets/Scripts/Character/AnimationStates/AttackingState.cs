using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IState
{
    public void EnterState(CharacterManager characterManager)
    {
        Debug.Log("Entering Attacking State");
    }

    public void ExitState(CharacterManager characterManager)
    {
        Debug.Log("Exiting Attacking State");
    }

    public void UpdateState(CharacterManager characterManager)
    {
        
    }
/*     IEnumerator AttackStateCounter(CharacterManager characterManager)
    {
        yield return new WaitForSeconds(5f);
        characterManager.isAttacking = false;
    } */

}
