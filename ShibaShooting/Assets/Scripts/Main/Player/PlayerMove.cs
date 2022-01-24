using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterBase player;
    
    [Header("²Ù¹Ì±â")]
    [SerializeField] private SpriteRenderer deco;
    [SerializeField] private Sprite sleep;
    [SerializeField] private Sprite lightning;

    private Vector2 targetPosition = Vector2.zero;
    public BackgroundMove backMove { get; private set; }

    private bool isDamaged = false;

    private void Awake()
    {
        GameManager.Instance.SetPlayerMove(player);
    }

    void Start()
    {
        player = GetComponent<CharacterBase>();
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
        Vector2.MoveTowards(transform.localPosition, targetPosition, player.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;
        if (player.playerState == PlayerState.Big) return;

        if (collision.CompareTag(ConstantManager.ENEMY_BULLET_TAG) || collision.CompareTag(ConstantManager.LIGHTNING_TAG))
        {
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
        }

        Vibrate();
        StartCoroutine(Damage());
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
            player.spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            player.spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        isDamaged = false;
        yield break;
    }

    public void DecoLightning(bool isTrue)
    {
        deco.enabled = isTrue;
        deco.sprite = lightning;
    }

    private void OnDestroy()
    {
        InputEventManager.StopListening("MOVE", Move);
    }
}