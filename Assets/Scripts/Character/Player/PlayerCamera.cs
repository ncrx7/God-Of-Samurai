using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance { get; private set; }
    public PlayerManager playerManager { get; set; }
    public Camera CameraObject;
    [SerializeField] Transform _cameraPivotTransform;

    [Header("Camera Settings")]
    private float _cameraSmoothSpeed = 100;
    [SerializeField] float _leftAndRightRotationSpeed = 220;
    [SerializeField] float _upAndDownRotationSpeed = 220;
    [SerializeField] float _minimumPivot = -30;
    [SerializeField] float _maximumPivot = 60;

    [Header("Camera Values")]
    private Vector3 _cameraVelocity;
    [SerializeField] float _leftAndRightLookAngle;
    [SerializeField] float _upAndDownLookAngle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void HandleCameraActions()
    {
        HandleFollowTarget();
        HandleCameraRotation();
    }

    private void HandleFollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, playerManager.transform.position, ref _cameraVelocity, _cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void HandleCameraRotation()
    {
        _leftAndRightLookAngle += (PlayerInputManager.Instance.CameraHorizontalInput * _leftAndRightRotationSpeed) * Time.deltaTime;
        _upAndDownLookAngle -= (PlayerInputManager.Instance.CameraVerticalInput * _upAndDownRotationSpeed) * Time.deltaTime;
        _upAndDownLookAngle = Mathf.Clamp(_upAndDownLookAngle, _minimumPivot, _maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;
        //Rotate this game object left and right
        cameraRotation.y = _leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        //Rotate Camera pivot up and down
        cameraRotation = Vector3.zero;
        cameraRotation.x = _upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        _cameraPivotTransform.localRotation = targetRotation;
    }
}
