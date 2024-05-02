using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TitleScreenManager : MonoBehaviour
{
   [Header("Menus")]
   [SerializeField] GameObject _titleScreenMainMenu;
   [SerializeField] GameObject _titleScreenLoadMenu;

   [Header("Buttons")]
   [SerializeField] Button _loadMenuReturnButton;
   [SerializeField] Button _mainMenuLoadGameButton;

   public void StartNetworkAsHost()
   {
      NetworkManager.Singleton.StartHost();
   }

   public void StartNewGame()
   {
      WorldSaveGameManager.Instance.AttemptToCreateNewGame();
   }

   public void OpenLoadMenu()
   {
      _titleScreenMainMenu.SetActive(false);
      _titleScreenLoadMenu.SetActive(true);

      _loadMenuReturnButton.Select();
   }

   public void CloseLoadMenu()
   {
      _titleScreenLoadMenu.SetActive(false);
      _titleScreenMainMenu.SetActive(true);

      _mainMenuLoadGameButton.Select();
   }
}
