using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.SetPoolManager(this);
    }
    //���ϴ� Ÿ���� Ǯ���� ������Ʈ���� ������ �Լ�
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

    //Ǯ���� ���ӿ�����Ʈ�� ������ �Լ�
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

    //���ϴ� ������Ʈ�� �̸��� Ǯ���� ������Ʈ�� ���� ��� => true
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

    public void SetPoolObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
    }
}
