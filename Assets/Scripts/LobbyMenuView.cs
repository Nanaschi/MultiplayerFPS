using System;
using UnityEngine;
using Zenject;

public class LobbyMenuView : MonoBehaviour
{
    [SerializeField] private RectTransform _loadingMenu;
    [SerializeField] private RectTransform _lobbyButtons;
    private UIController _uiController;


    [Inject]

    void InitInject(UIController uiController)
    {
        _uiController = uiController;
    }
    
    private void OnEnable()
    {
        Launcher.OnConnectedToMasterAction +=SwitchUIElements;
    }

    private void SwitchUIElements()
    {
        _uiController.SelectActiveUI(_lobbyButtons, _loadingMenu);
    }

    private void SwitchMenu()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        
    }
}
