using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerManager : MonoBehaviour
{
    public static ObjectPoolerManager instance;

    private Dictionary<string, PoolObject> _poolObjects;

    public List<PoolObject> objectsToPool;

    private void Awake()
    {
        instance = this;

        _poolObjects = new Dictionary<string, PoolObject>();

        for (int i = 0; i < objectsToPool.Count; i++)
        {
            PoolObject poolObject = objectsToPool[i];
            // TODO::Checking for same ID            
            _poolObjects.Add(poolObject.id, poolObject);
        }
    }

    public GameObject GetPoolableObjectById(string id)
    {
        GameObject obj = null;
        if(_poolObjects[id].objectsCreated.Count > 0)
        {
            obj = _poolObjects[id].objectsCreated[0];
            _poolObjects[id].objectsCreated.Remove(obj);
            obj.transform.parent = null;
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(_poolObjects[id].prefab);
            //_poolObjects[id].objectsCreated.Add(obj);
            //_poolObjects[id].objectsCreated.Remove(obj);
            obj.transform.parent = null;
            obj.SetActive(true);
        }        
        return obj;
    }

    public GameObject GetPoolableObjectById(string id, Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetPoolableObjectById(id);
        obj.transform.SetPositionAndRotation(position, rotation);
        IPoolComponent poolComponent = obj.GetComponent<IPoolComponent>();
        if (poolComponent != null)
        {
            poolComponent.SetPositionRotation(position, rotation);            
        }
        return obj;
    }

    public void HideObject(GameObject obj)
    {
        obj.transform.parent = this.transform;
        obj.SetActive(false);
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        PoolableObject poolableObject = obj.GetComponent<PoolableObject>();
        if (poolableObject != null)
        {
            _poolObjects[poolableObject.id].objectsCreated.Add(obj);
        }
    }
}

[System.Serializable]
public class PoolObject
{
    public string id;
    public GameObject prefab;
    public List<GameObject> objectsCreated = new List<GameObject>();
}
