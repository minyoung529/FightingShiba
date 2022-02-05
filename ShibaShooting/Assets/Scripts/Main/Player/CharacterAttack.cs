using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletPosition = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float fireRate = 0f;
    
    private void Start()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            InstantiateOrPool();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private GameObject InstantiateOrPool()
    {
        GameObject result = null;

        if (GameManager.Instance.poolManager.IsInPoolObject(bulletPrefab.name))
        {
            result = GameManager.Instance.poolManager.GetPoolObject(bulletPrefab.name);
        }

        else
        {
            result = Instantiate(bulletPrefab, bulletPosition);
        }

        result.transform.position = bulletPosition.position;
        result.transform.SetParent(null);
        GameManager.Instance.UIManager.AddScore(4);
        result.SetActive(true);

        return result;
    }
}
