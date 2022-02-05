using System.Collections;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private Vector2 targetPosition = Vector2.zero;

    [SerializeField] private bool isInControl;

    private void Awake()
    {
        if (isInControl)
        {
            GameManager.Instance.SetPlayer(GetComponent<CharacterBase>());
        }
    }

    void Start()
    {
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

    

    private void OnDestroy()
    {
        InputEventManager.StopListening("MOVE", Move);
    }
}