using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public event Action OnSendClick;
    public event Action OnChangeClick;
    
    [SerializeField] private Button _sendButton;
    [SerializeField] private Button _changeButton;
    [SerializeField] private TextMeshProUGUI _roomName;

    private void Awake()
    {
        _sendButton.onClick.AddListener(SendButtonClick);
        _changeButton.onClick.AddListener(ChangeButtonClick);
    }

    private void Update()
    {
        _roomName.text = PhotonNetwork.CurrentRoom?.Name;
    }

    private void OnDestroy()
    {
        _sendButton.onClick.RemoveAllListeners();
        _changeButton.onClick.RemoveAllListeners();
    }

    private void ChangeButtonClick()
    {
        OnChangeClick?.Invoke();
    }

    private void SendButtonClick()
    {
        OnSendClick?.Invoke();
    }
}
