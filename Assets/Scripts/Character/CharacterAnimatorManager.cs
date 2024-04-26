using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
//using Unity.Netcode;

public class CharacterAnimatorManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;
    public Animator animator;

    float vertical;
    float horizontal;

    protected virtual void Awake()
    {

    }

    protected virtual void OnEnable()
    {
        EventSystem.UpdateFloatAnimatorParameterAction += UpdateFloatAnimatorParameter;
        EventSystem.PlayTargetAnimationAction += PlayTargetAnimation;
    }

    protected virtual void OnDisable()
    {
        EventSystem.UpdateFloatAnimatorParameterAction -= UpdateFloatAnimatorParameter;
        EventSystem.PlayTargetAnimationAction -= PlayTargetAnimation;
    }

    public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
    {
        //Classic assign
        animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);

        /*         //Blended assign
                float snappedHorizontal = 0;
                float snappedVertical = 0;

                #region Horizontal
                if (horizontalValue > 0 && horizontalValue <= 0.5f)
                {
                    snappedHorizontal = 0.5f;
                }
                else if (horizontalValue > 0.5f && horizontalValue <= 1)
                {
                    snappedHorizontal = 1;
                }
                else if (horizontalValue < 0 && horizontalValue >= -0.5f)
                {
                    snappedHorizontal = -0.5f;
                }
                else if (horizontalValue < -0.5f && horizontalValue >= -1)
                {
                    snappedHorizontal = -1;
                }
                else
                {
                    snappedHorizontal = 0;
                }
                #endregion

                #region Vertical
                if (verticalValue > 0 && verticalValue <= 0.5f)
                {
                    snappedVertical = 0.5f;
                }
                else if (verticalValue > 0.5f && verticalValue <= 1)
                {
                    snappedVertical = 1;
                }
                else if (verticalValue < 0 && verticalValue >= -0.5f)
                {
                    snappedVertical = -0.5f;
                }
                else if (verticalValue < -0.5f && verticalValue >= -1)
                {
                    snappedVertical = -1;
                }
                else
                {
                    snappedVertical = 0;
                }
                #endregion

                _characterManager.animator.SetFloat("Horizontal", snappedHorizontal);
                _characterManager.animator.SetFloat("Vertical", snappedVertical); */
    }

    private void UpdateFloatAnimatorParameter(ulong id, string parameterName, float value)
    {
        Debug.Log("id :" + id);
        if (id == GetComponent<PlayerManager>().networkID)
        {
            animator.SetFloat(parameterName, value, 0.5f, Time.deltaTime);

        }
        else
        {
            Debug.Log("ID NOT MATHCED");
        }
    }

    public virtual void PlayTargetAnimation(ulong id, string targetAnimation, bool isPerformingAction, bool canRotate = false, bool canMove = false, bool applyRootMotion = true)
    {
        if (id == GetComponent<PlayerManager>().networkID)
        {

            _characterManager.applyRootMotion = applyRootMotion;
            animator.CrossFade(targetAnimation, 0.2f);
            _characterManager.isPerformingAction = isPerformingAction;
            _characterManager.canMove = canMove;
            _characterManager.canRotate = canRotate;

            _characterManager.characterNetworkManager.NotiftTheServerOfActionAnimationServerRPC(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }
    }
}
