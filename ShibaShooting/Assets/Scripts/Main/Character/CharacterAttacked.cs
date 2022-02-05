using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacked : MonoBehaviour
{
    CharacterBase character;
    private bool isDamaged = false;
    [SerializeField] private List<string> enemyList;
    [SerializeField] private bool isContinueDamaged;
    [SerializeField] private int twinkleCount;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isContinueDamaged && isDamaged) return;
        if (CompareTag(ConstantManager.PLAYER_TAG) && GameManager.Instance.player.playerState == PlayerState.Big) return;
        
        if (enemyList.Contains(collision.tag))
        {
            GameManager.Instance.poolManager.DespawnPoolObject(collision.gameObject);
            StartCoroutine(Damage());
            GameManager.Instance.Vibrate();
        }
    }

    public IEnumerator Damage()
    {
        isDamaged = true;
        SoundManager.Instance.DamagedAudio();
        GameManager.Instance.Dead();

        for (int i = 0; i < twinkleCount; i++)
        {
            character.spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            character.spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        isDamaged = false;
        yield break;
    }
}
