using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PhotonController _photonController;
    [SerializeField] private UIController _uiController;
    [SerializeField] private PlayerCube _playerCube;
    [SerializeField] private SphereObjectsContainer _sphereObjectsContainer;
    
    private void Start()
    {
        _photonController.Init(_sphereObjectsContainer);
        
        _uiController.OnSendClick += UiControllerOnSendClick;
        _uiController.OnChangeClick += UiControllerOnChangeClick;
    }

    private void OnDestroy()
    {
        _uiController.OnSendClick -= UiControllerOnSendClick;
        _uiController.OnChangeClick -= UiControllerOnChangeClick;
    }
    
    private void UiControllerOnSendClick()
    {
        _playerCube.CommandChangeToEnemy();
    }

    private void UiControllerOnChangeClick()
    {
        _playerCube.Mimic(_sphereObjectsContainer.GetRandomObject());
    }
}
