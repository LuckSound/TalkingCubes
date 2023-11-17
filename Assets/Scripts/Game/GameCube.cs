using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GameCube : MonoBehaviour
{
    private MeshRenderer _cubeRenderer;
    private SphereObjectData _nowData;

    public SphereObjectData Skin => _nowData;
    
    private void Awake()
    {
        _cubeRenderer = this.GetComponent<MeshRenderer>();
    }

    public void Mimic(SphereObjectData newData)
    {
        _nowData = newData;
        _cubeRenderer.sharedMaterial = newData.Sphere.sharedMaterial;
    }
}
