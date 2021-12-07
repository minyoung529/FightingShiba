using System.Collections;
using UnityEngine;

public class EnemyBullet : BulletMove
{
    [SerializeField]
    private PhysicsMaterial2D bounce;
    private Collider2D col;
    private Rigidbody2D rigid;
    private bool isBounce = false;

    private EnemyMove enemyMove;

    private void Start()
    {
        enemyMove = FindObjectOfType<EnemyMove>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    protected override void Update()
    {
        if (GameManager.Instance.tutorialManager.GetIsTutorial())
        {
            Despawn();
        }

        if (!isBounce)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.Self);
        }

        if (enemyMove.HpZero())
        {
            StartCoroutine(Bounce());
        }

        if (!isBounce)
        {
            CheckLimit();
        }
    }

    protected override void CheckLimit()
    {
        base.CheckLimit();
    }

    protected override void Despawn()
    {
        transform.SetParent(GameManager.Instance.poolManager.transform, false);
        gameObject.SetActive(false);
    }


    IEnumerator Bounce()
    {
        for (int i = 0; i < 200; i++)
        {
            rigid.AddForce(Vector2.left * 4.3f);
            isBounce = true;
            col.isTrigger = false;
            col.sharedMaterial = bounce;

            yield return new WaitForSeconds(1.5f);

            col.sharedMaterial = null;
            col.isTrigger = true;
            isBounce = false;

            yield return new WaitForSeconds(14f);
        }
    }
}