using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackingState : IState
{
    //private float _combatStateTime = 5f;

    public void EnterState(CharacterManager characterManager)
    {
        Debug.Log("Entering Basic Attacking State");
        //EventSystem.HandleBasicAttackAction += ResetCombatStateTime;
    }

    public void ExitState(CharacterManager characterManager)
    {
        Debug.Log("Exiting Basic Attacking State");
        //EventSystem.HandleBasicAttackAction -= ResetCombatStateTime;
    }

    public void UpdateState(CharacterManager characterManager)
    {
        //_combatStateTime -= Time.deltaTime;

        // STATE CHANGES
        if (characterManager.isGrounded && !characterManager.isRunning && !characterManager.isBasicAttacking)
        {
            characterManager.ChangeState(new IdleState());
        }
        else if (characterManager.isGrounded && characterManager.isRunning && !characterManager.isBasicAttacking)
        {
            characterManager.ChangeState(new RunningState());
        }
        else if(characterManager.isJumping && !characterManager.isBasicAttacking) 
        {
            characterManager.ChangeState(new JumpingState());
        }
/*         else if(_combatStateTime <= 0)
        {
            characterManager.ChangeState(new IdleState());
        } */
    }

/*     private void ResetCombatStateTime(ulong id)
    {
        
        _combatStateTime = 0;
    } */
/*     IEnumerator AttackStateCounter(CharacterManager characterManager)
    {
        yield return new WaitForSeconds(5f);
        characterManager.isAttacking = false;
    } */

}
