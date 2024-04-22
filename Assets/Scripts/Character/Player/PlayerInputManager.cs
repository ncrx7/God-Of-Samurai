using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }
    private NewControls _playerControls;

    [SerializeField] private Vector2 movementInput;

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new NewControls();

            _playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        _playerControls.Enable();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

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
        SceneManager.activeSceneChanged += OnSceneChanged;
        Instance.enabled = false;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // Player controls doesn't work on main menu thanks to here
        if (newScene.buildIndex == WorldSaveGameManager.Instance.GetWorldSceneIndex())
        {
            Instance.enabled = true;
        }
        else
        {
            Instance.enabled = false;
        }
    }

}
