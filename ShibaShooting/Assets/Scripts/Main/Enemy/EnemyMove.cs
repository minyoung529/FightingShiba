using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    #region 변수목록
    private float hp = 160;

    [Header("Enemy 빠르기")]
    [SerializeField] protected float speed = 5f;

    private bool isDamaged = false;
    protected bool isDead = false;

    private SpriteRenderer spriteRenderer = null;
    private SpeechBubble speechBubble = null;

    [Header("총알 딜레이")]
    [SerializeField] private float fireRate = 1f;

    private float circleTimer = 0f;
    private float randomTimer = 0f;
    private float circleMaxTime = 3f;
    private Vector3 diff = Vector3.zero;
    private CharacterMove player = null;
    private float rotationZ = 0f;

    [Header("적 생성시 필요한 변수")]
    [SerializeField] private Transform enemyBulletPosition = null;
    [SerializeField] private GameObject enemyBulletPrefab = null;

    [Header("스프라이트")]
    [SerializeField] Sprite[] minyoungSprites;
    [SerializeField] private Sprite evolutionMinyoung;

    BackgroundMove back;
    #endregion

    protected virtual void Start()
    {
        back = FindObjectOfType<BackgroundMove>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        speechBubble = FindObjectOfType<SpeechBubble>();

        StartCoroutine(EnemyFire());
        StartCoroutine(MoveUpDown());
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if (transform.position.x >= 6.5f)
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (!GameManager.Instance.tutorialManager.GetIsTutorial())
            EnemyAttack();

        ChangeSprite();
    }

    private IEnumerator MoveUpDown()
    {
        float second = 0.7f;
        float distance = 0.7f;
        while(true)
        {
            transform.DOMoveY(distance, second);
            yield return new WaitForSeconds(second);
            transform.DOMoveY(-distance, second);
            yield return new WaitForSeconds(second);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 6.5f) return;
        if (isDead) return;

        if (collision.CompareTag(ConstantManager.BULLET_TAG))
        {
            if (isDamaged) return;

            OnDamaged();
            collision.gameObject.SetActive(false);
            collision.transform.SetParent(GameManager.Instance.poolManager.transform, false);
            StartCoroutine(Damaged());

            if (hp == 0)
            {
                back.ChangeToRed();
                GameManager.Instance.UIManager.StartWarning();
                GameManager.Instance.SetLifeCount(5);
                GameManager.Instance.StartCoroutine("RealBossTime");
                GameManager.Instance.StartCoroutine("DarkActive");
                SoundManager.Instance.BossBGM();
            }

            if (hp % 30 == 0 && hp > 29)
            {
                GameManager.Instance.StartCoroutine("SpawnSmallEnemy");
            }
        }
    }
    private IEnumerator Damaged()
    {
        hp--;
        spriteRenderer.material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f, 0f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", Color.clear);
        isDamaged = false;
    }

    private void OnDamaged()
    {
        isDamaged = true;
        SoundManager.Instance.Bullet();
        GameManager.Instance.UIManager.EnemyHPBar(1);
    }

    private IEnumerator EnemyFire()
    {
        while (true)
        {
            InstantiateOrPool();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private GameObject SetBulletPosition(GameObject bullet)
    {
        if (GameManager.Instance.player == null) return null;
        diff = GameManager.Instance.player.transform.position - transform.position;
        diff.Normalize();

        rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 180f);
        bullet.transform.SetParent(null);

        return bullet;
    }

    protected virtual GameObject InstantiateOrPool()
    {
        GameObject result = null;
        if (GameManager.Instance.poolManager.IsInPoolObject(enemyBulletPrefab.name))
        {
            result = GameManager.Instance.poolManager.GetPoolObject(enemyBulletPrefab.name);
            result.transform.position = enemyBulletPosition.position;
            result.transform.SetParent(null);
            result.SetActive(true);
        }

        else
        {
            result = Instantiate(enemyBulletPrefab, enemyBulletPosition.transform);
        }

        result = SetBulletPosition(result);
        return result;
    }

    private void EnemyAttack()
    {
        if (hp < 190 && hp > 150)
        {
            fireRate = 0.8f;
            speechBubble.ChangeSprites(1);
            return;
        }

        if (hp < 150 && hp > 120)
        {
            circleMaxTime = 10f;
            Circle();
            fireRate = 0.6f;
            speechBubble.ChangeSprites(2);
            return;
        }

        if (hp < 120 && hp > 100)
        {
            Random();
            speechBubble.ChangeSprites(3);
            fireRate = 0.5f;
            return;
        }

        if (hp < 100 && hp > 80)
        {
            speechBubble.ChangeSprites(4);
            return;
        }

        if (hp < 80 && hp > 50)
        {
            fireRate = 1.4f;
            Random();
            speechBubble.ChangeSprites(5);
            return;
        }

        if (hp < 50 && hp > 20)
        {
            Circle();
            circleMaxTime = 6;
            speechBubble.ChangeSprites(6);
            return;
        }

        if (hp < 20 && hp > 10)
        {
            Random();

            circleMaxTime = 3f;
            speechBubble.ChangeSprites(7);
            return;
        }

        if (hp < 10 && hp > 0)
        {
            Circle();
            circleMaxTime = 2f;
            speechBubble.ChangeSprites(8);
            return;
        }

        if (hp < 0)
        {
            Circle();
            Random();
            fireRate = 1.2f;
            circleMaxTime = 3.2f;
        }
    }

    private void ChangeSprite()
    {
        if (hp < 170)
            spriteRenderer.sprite = minyoungSprites[0];

        if (hp < 120)
            spriteRenderer.sprite = minyoungSprites[1];

        if (hp < 70)
            spriteRenderer.sprite = minyoungSprites[2];

        if (hp < 40)
            spriteRenderer.sprite = minyoungSprites[3];

        if (hp <= 0)
            spriteRenderer.sprite = evolutionMinyoung;
    }

    private void Circle()
    {
        circleTimer += Time.deltaTime;

        GameObject circleBullet = null;

        if (circleTimer >= circleMaxTime)
        {
            for (int i = -90; i < 90; i += 15)
            {
                if (GameManager.Instance.poolManager.IsInPoolObject(enemyBulletPrefab.name))
                {
                    circleBullet = GameManager.Instance.poolManager.GetPoolObject(enemyBulletPrefab.name);
                    circleBullet.transform.position = enemyBulletPosition.position;
                    circleBullet.transform.SetParent(null);
                    circleBullet.SetActive(true);
                }

                else
                    circleBullet = Instantiate(enemyBulletPrefab, enemyBulletPosition.transform);

                circleBullet.transform.position = enemyBulletPosition.transform.position;
                circleBullet.transform.rotation = Quaternion.Euler(0, 0, i);
                circleTimer = 0f;
            }
        }
    }

    private void Random()
    {
        randomTimer += Time.deltaTime;

        GameObject randomBullet = null;

        if (randomTimer >= 0.3)
        {
            int randomRot = UnityEngine.Random.Range(-90, 90);

            if (GameManager.Instance.poolManager.IsInPoolObject(enemyBulletPrefab.name))
            {
                randomBullet = GameManager.Instance.poolManager.GetPoolObject(enemyBulletPrefab.name);
                randomBullet.transform.position = enemyBulletPosition.position;
                randomBullet.transform.SetParent(null);
                randomBullet.SetActive(true);
            }

            else
                randomBullet = Instantiate(enemyBulletPrefab, enemyBulletPosition.transform);

            randomBullet.transform.position = enemyBulletPosition.transform.position;
            randomBullet.transform.rotation = Quaternion.Euler(0, 0, randomRot);
            randomTimer = 0f;
        }
    }

    public bool HpZero()
    {
        if (hp <= 0 && hp >= -1) return true;
        else if (hp <= -30 && hp > -31) return true;
        else if (hp <= -90 && hp > -91) return true;
        else if (hp <= -120 && hp > -121) return true;
        else return false;
    }
}