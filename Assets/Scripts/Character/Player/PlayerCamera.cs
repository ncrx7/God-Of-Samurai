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
    [SerializeField] private float _cameraSmoothSpeed = 100;
    [SerializeField] float _leftAndRightRotationSpeed = 220;
    [SerializeField] float _upAndDownRotationSpeed = 220;
    [SerializeField] float _minimumPivot = -30;
    [SerializeField] float _maximumPivot = 60;
    [SerializeField] float _cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask _collideWithLayers;

    [Header("Camera Values")]
    private Vector3 _cameraVelocity;
    [SerializeField] float _leftAndRightLookAngle;
    [SerializeField] float _upAndDownLookAngle;
    private Vector3 _cameraObjectPosition; // USED FOR CAMERA COLLISION
    private float _cameraZPosition;
    private float _targetCameraZPosition;

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
        _cameraZPosition = CameraObject.transform.localPosition.z;
    }

    public void HandleCameraActions()
    {
        HandleFollowTarget();
        HandleCameraRotation();
        HandleCameraCollision();
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

    private void HandleCameraCollision()
    {
        _targetCameraZPosition = _cameraZPosition;

        RaycastHit hit;
        Vector3 direction = CameraObject.transform.position - _cameraPivotTransform.transform.position;
        direction.Normalize();

        if(Physics.SphereCast(_cameraPivotTransform.position, _cameraCollisionRadius, direction, out hit, Mathf.Abs(_targetCameraZPosition), _collideWithLayers))
        {
            float distanceFromHitObject = Vector3.Distance(_cameraPivotTransform.position, hit.point);
            _targetCameraZPosition = -(distanceFromHitObject -_cameraCollisionRadius);
        }
        
        //MAKE IT SNAP BACK IF TARGET POS IS LESS THAN CAMERA COLLISION RADIUS
        if(Mathf.Abs(_targetCameraZPosition) < _cameraCollisionRadius)
        {
            _targetCameraZPosition = -_cameraCollisionRadius;
        }

        _cameraObjectPosition.z = Mathf.Lerp(CameraObject.transform.localPosition.z, _targetCameraZPosition, 0.2f);
        CameraObject.transform.localPosition = _cameraObjectPosition;
    }
}
