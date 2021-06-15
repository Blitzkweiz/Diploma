using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolData", menuName = "Pool Data", order = 51)]
public class PoolScriptableObject : ScriptableObject
{
    public int size;
    public GameObject prefab;
    public ObjectType type;
}
