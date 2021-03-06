using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.SetPoolManager(this);
    }
    //원하는 타입을 풀링된 오브젝트에서 꺼내는 함수
    public T GetPoolObject<T>(string objName)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals(objName))
            {
                return transform.GetComponent<T>();
            }
        }

        return default(T);
    }

    //풀링된 게임오브젝트를 꺼내는 함수
    public GameObject GetPoolObject(string objName)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;

            if (obj.name.Contains(objName))
            {
                return obj;
            }
        }

        return null;
    }

    //원하는 오브젝트의 이름이 풀링된 오브젝트에 있을 경우 => true
    public bool IsInPoolObject(string objName)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;

            if (obj.name.Contains(objName))
            {
                return true;
            }
        }

        return false;
    }

    public void DespawnPoolObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
    }
}
