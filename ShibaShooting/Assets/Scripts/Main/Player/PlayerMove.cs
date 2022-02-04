using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("²Ù¹Ì±â")]
    [SerializeField] private SpriteRenderer deco;

    private Vector2 targetPosition = Vector2.zero;
    public BackgroundMove backMove { get; private set; }

    private bool isDamaged = false;

    private void Awake()
    {
        GameManager.Instance.SetPlayerMove(GetComponent<CharacterBase>());
    }

    void Start()
    {
        backMove = FindObjectOfType<BackgroundMove>();
        InputEventManager.StartListening("MOVE", Move);
    }

    private void Move(EventParam eventParam)
    {
        if (GameManager.Instance.UIManager.GetIsStop()) return;
        if (!GameManager.Instance.CurrentUser.GetIsCompleteTutorial() && GameManager.Instance.tutorialManager.GetIsTutorial()) return;

        targetPosition = eventParam.vectorParam;

        targetPosition.x = Mathf.Clamp(targetPosition.x, GameManager.Instance.MinPosition.x, GameManager.Instance.MaxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, GameManager.Instance.MinPosition.y, GameManager.Instance.MaxPosition.y);

        transform.localPosition =
        Vector2.MoveTowards(transform.localPosition, targetPosition, GameManager.Instance.player.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;
        if (GameManager.Instance.player.playerState == PlayerState.Big) return;

        if (collision.CompareTag(ConstantManager.ENEMY_BULLET_TAG) || collision.CompareTag(ConstantManager.LIGHTNING_TAG))
        {
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
            Vibrate();
        }
    }

    private void Vibrate()
    {
        if (GameManager.Instance.CurrentUser.isVibrate)
            Handheld.Vibrate();
    }

    public IEnumerator Damage()
    {
        isDamaged = true;
        SoundManager.Instance.DamagedAudio();
        GameManager.Instance.Dead();
        for (int i = 0; i < 5; i++)
        {
            GameManager.Instance.player.spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            GameManager.Instance.player.spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        isDamaged = false;
        yield break;
    }

    private void OnDestroy()
    {
        InputEventManager.StopListening("MOVE", Move);
    }
}