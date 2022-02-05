using UnityEngine;
using MonsterLove.StateMachine;

public enum PlayerState
{
    Idle,
    Big,
    Slow,
    Heart,
    Money,
    Small,
    Tired
}

public class PlayerSkill : MonoBehaviour
{
    private StateMachine<PlayerState, StateDriverUnity> fsm;
    private CharacterBase player;

    private void Awake()
    {
        fsm = new StateMachine<PlayerState, StateDriverUnity>(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(ConstantManager.ITEM_TAG))
        {
            PlayerState state = collision.gameObject.GetComponent<Item>().GetItem();
            player.playerState = state;
            fsm.ChangeState(player.playerState);
        }
    }

    private void Idle_Enter()
    {
        Time.timeScale = 1f;
        gameObject.transform.localScale = Vector2.one;
        player.speed = ConstantManager.PLAYER_SPEED;
    }

    private void Big_Enter()
    {
        gameObject.transform.localScale = new Vector2(1.5f, 1.5f);
    }

    private void Small_Enter()
    {
        gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
    }

    private void Slow_Enter()
    {
        Time.timeScale = 0.5f;
    }

    private void Tired_Enter()
    {
        player.speed = 7f;
    }

    private void Heart_Enter()
    {
        GameManager.Instance.PlusHeart(1);
        ChangeToIdle();
    }

    private void Money_Enter()
    {
        GameManager.Instance.UIManager.AddCoin(5);
        ChangeToIdle();
    }

    private void ChangeToIdle()
    {
        player.playerState = PlayerState.Idle;
        fsm.ChangeState(player.playerState);
    }
}
