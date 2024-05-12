using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager _characterManager;

    [Header("Ground Check & Jumping")]
    [SerializeField] protected float gravityForce = -5.5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckSphereRadius = 1;
    [SerializeField] protected Vector3 yVelocity;
    [SerializeField] protected float groundedYVelocity = -20;
    [SerializeField] protected float fallStartVelocity = -5;
    protected bool fallingVelocityHasBeenSet = false;
    protected float inAirTime = 0;

    protected virtual void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
    }

    protected virtual void Update()
    {
        HandleGroundCheck();
        HandleGravity();
    }

    protected void HandleGroundCheck()
    {
        _characterManager.isGrounded = Physics.CheckSphere(_characterManager.transform.position, groundCheckSphereRadius, groundLayer);
    }

    protected void HandleGravity()
    {
        if (_characterManager.isGrounded)
        {
            if (yVelocity.y < 0)
            {
                inAirTime = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            if (!_characterManager.isJumping && !fallingVelocityHasBeenSet)
            {
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartVelocity;
            }

            inAirTime += Time.deltaTime;
            EventSystem.UpdateAnimatorParameterAction?.Invoke(_characterManager.networkID, AnimatorValueType.FLOAT, "inAirTimer", inAirTime, false);

            yVelocity.y += gravityForce * Time.deltaTime;
        }

        //Debug.Log("yvelocity: " + yVelocity);
        _characterManager.characterController.Move(yVelocity * Time.deltaTime);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_characterManager.transform.position, groundCheckSphereRadius);
    }
}
