using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonController : MonoBehaviourPunCallbacks
{
    public event Action<bool> OnEnterRoom;
    public event Action OnOpponentConnected;
    
    private SphereObjectsContainer _sphereObjectsContainer;

    private void Awake()
    {
        PhotonPeer.RegisterType(typeof(SphereObjectData), 
            (byte) 'S', 
            SerializeSphereObjectData,
            DeserializeSphereObjectData);
    }

    private object DeserializeSphereObjectData(byte[] serializedCustomObject)
    {
        return _sphereObjectsContainer.DeserializeFromBytes(serializedCustomObject);
    }

    private byte[] SerializeSphereObjectData(object customObject)
    {
        SphereObjectData sphereObjectData = (SphereObjectData) customObject;
        return sphereObjectData.SerializeToBytes();
    }

    public void Init(SphereObjectsContainer sphereObjectsContainer)
    {
        _sphereObjectsContainer = sphereObjectsContainer;
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Connected to Master. Now {PhotonNetwork.CountOfRooms} rooms.");
        if (PhotonNetwork.CountOfRooms > 0)
            PhotonNetwork.JoinRandomRoom();
        else
            PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = 2});
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log($"Join room failed. Created new.");
        PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = 2});
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
        Debug.Log($"Joined to existing room {PhotonNetwork.CurrentRoom.Name}");
        OnEnterRoom?.Invoke(PhotonNetwork.PlayerList.Length > 1);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        Debug.Log("Joined to new room");
        OnEnterRoom?.Invoke(PhotonNetwork.PlayerList.Length > 1);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        
        Debug.Log("Player joined to our room");
        OnOpponentConnected?.Invoke();
    }
}
