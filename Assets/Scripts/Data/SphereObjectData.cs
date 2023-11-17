using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(menuName = "Sphere Object Data", fileName = "SO/Sphere")]
public class SphereObjectData : ScriptableObject
{
    [SerializeField] private int _key;
    [SerializeField] private MeshRenderer _sphere;

    public int Key => _key;
    public MeshRenderer Sphere => _sphere;
    
    public byte[] SerializeToBytes()
    {
        SerializableData serializableData = new SerializableData
        {
            Key = _key,
        };
        
        BinaryFormatter formatter = new BinaryFormatter();
        using MemoryStream memoryStream = new MemoryStream();
        formatter.Serialize(memoryStream, serializableData);
        return memoryStream.ToArray();
    }
    
    [Serializable]
    public class SerializableData
    {
        public int Key;
    }
}
