using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkChanger : MonoBehaviour
{
    public static CharacterNetworkChanger Instance;

    [Header("NETWORK JOIN")]
    [SerializeField] bool _startGameAsClient;

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

    private void Update()
    {
        if(_startGameAsClient)
        {
            _startGameAsClient = false;

            NetworkManager.Singleton.Shutdown();

            NetworkManager.Singleton.StartClient();
        }
    }
}
