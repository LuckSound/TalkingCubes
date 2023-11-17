using Photon.Pun;
using UnityEngine;

public class PlayerCube : PhotonView
{
    [SerializeField] private GameCube _playerGameCube;
    [SerializeField] private EnemyCube _enemyCube;

    public GameCube PlayerGameCube => _playerGameCube;

    [PunRPC]
    public void RaisedToChange(SphereObjectData newData)
    {
        Debug.Log("Raised To Change");
        Mimic(newData);
    }
    
    public void Mimic(SphereObjectData sphereObject)
    {
        _playerGameCube.Mimic(sphereObject);
        _enemyCube.StartToChangeHimselfOnAnotherClient(_playerGameCube.Skin);
    }

    public void CommandChangeToEnemy()
    {
        this.RPC(nameof(RaisedToChange), RpcTarget.Others, _playerGameCube.Skin);
    }
}
