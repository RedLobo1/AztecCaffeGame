using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnLocatTransform;
    public void Spawn()
    {
        Instantiate(_prefab, _spawnLocatTransform.position, Quaternion.identity);
    }
    //use prefab and spawn ten copies of it tih different names

}
