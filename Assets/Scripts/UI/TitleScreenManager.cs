using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TitleScreenManager : MonoBehaviour
{
   public static TitleScreenManager Instance { get; private set; }

   [Header("Menus")]
   [SerializeField] GameObject _titleScreenMainMenu;
   [SerializeField] GameObject _titleScreenLoadMenu;

   [Header("Buttons")]
   [SerializeField] Button _mainMenuNewGameButton;
   [SerializeField] Button _mainMenuLoadGameButton;
   [SerializeField] Button _loadMenuReturnButton;
   [SerializeField] Button _deleteCharacterSlotsConfirmButton;

   [Header("Pop Ups")]
   [SerializeField] GameObject _noCharacterSlotsPopUp;
   [SerializeField] Button _noCharacterSlotsOkayButton;
   [SerializeField] GameObject _deleteCharacterSlotPopUp;

   [Header("Character Save Slot")]
   public CharacterSaveSlot currentSelectedSlot = CharacterSaveSlot.NO_SLOT;

   [Header("Title Screen Inputs")]
   [SerializeField] bool _deletrCharacterSlotPopUp = false;

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

   //CHARACTER SLOTS
   public void DisplayNoFreeCharacterSlotsPopUp()
   {
      _noCharacterSlotsPopUp.SetActive(true);
      _noCharacterSlotsOkayButton.Select();
   }

   public void CloseNoCharacterSlotsButton()
   {
      _noCharacterSlotsPopUp.SetActive(false);
      _mainMenuNewGameButton.Select();
   }

   public void SelectCharacterSlot(CharacterSaveSlot characterSlot)
   {
      currentSelectedSlot = characterSlot;
   }

   public void SelectNoSlot()
   {
      currentSelectedSlot = CharacterSaveSlot.NO_SLOT;
   }

   public void AttemptToDeleteCharacterSlot(CharacterSaveSlot characterSaveSlot)
   {
      currentSelectedSlot = characterSaveSlot;
   
      if (currentSelectedSlot != CharacterSaveSlot.NO_SLOT)
      {
         _deleteCharacterSlotPopUp.SetActive(true);
         _deleteCharacterSlotsConfirmButton.Select();
      }
   }

   public void ConfirmDeleteCharacterSlotButton()
   {
      _deleteCharacterSlotPopUp.SetActive(false);
      WorldSaveGameManager.Instance.DeleteGame(currentSelectedSlot);

      _titleScreenLoadMenu.SetActive(false);
      _titleScreenLoadMenu.SetActive(true);

      _loadMenuReturnButton.Select();
   }

   public void CloseDeleteCharacterPopUp()
   {
      _deleteCharacterSlotPopUp.SetActive(false);
      _loadMenuReturnButton.Select();
   }
}
