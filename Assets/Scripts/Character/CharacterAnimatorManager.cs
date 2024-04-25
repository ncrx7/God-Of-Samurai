using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    [SerializeField] CharacterManager _characterManager;

    float vertical;
    float horizontal;

    protected virtual void Awake()
    {

    }

    public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
    {
        //Classic assign
        _characterManager.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
        _characterManager.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);

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
}
