using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(menuName = "Sphere Objects Container", fileName = "SO/SphereContainer")]
public class SphereObjectsContainer : ScriptableObject
{
    [SerializeField] private List<SphereObjectData> _sphereObjects;

    public SphereObjectData GetRandomObject()
    {
        int index = Random.Range(0, _sphereObjects.Count);
        return _sphereObjects[index];
    }

    public SphereObjectData GetObjectWithKey(int key)
    {
        return _sphereObjects.First(x => x.Key == key);
    }
    
    public SphereObjectData DeserializeFromBytes(byte[] data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using MemoryStream memoryStream = new MemoryStream(data);
        SphereObjectData.SerializableData sphereData = formatter.Deserialize(memoryStream) as SphereObjectData.SerializableData;
        return GetObjectWithKey(sphereData.Key);
    }
}
