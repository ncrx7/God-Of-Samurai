using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
   void EnterState(CharacterManager characterManager);
   void UpdateState(CharacterManager characterManager);
   void ExitState(CharacterManager characterManager);
}
