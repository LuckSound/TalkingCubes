using Photon.Pun;
using UnityEngine;

public class EnemyCube : PhotonView
{
    [SerializeField] private GameCube _enemyGameCube;
    
    [PunRPC]
    public void ChangedHimself(SphereObjectData newData)
    {
        Debug.Log("Changed himself");
        _enemyGameCube.Mimic(newData);
    }

    public void StartToChangeHimselfOnAnotherClient(SphereObjectData sphereObject)
    {
        this.RPC(nameof(ChangedHimself), RpcTarget.Others, sphereObject);
    }
}
