using System.Collections;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [Header("��ũ�� ���ǵ�")]
    [SerializeField]
    private float speed = 0.5f;

    [Header("���׸���")]
    [SerializeField]
    private Material dark;
    [SerializeField]
    private Material bright;
    [SerializeField]
    private Material red;

    private float tiredTime;

    private Vector2 offset = Vector2.zero;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        tiredTime = PlayerPrefs.GetFloat("tiredTime", 7);
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        offset.x += speed * Time.deltaTime;
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }

    public IEnumerator ChangeBackground()
    {
        meshRenderer.material = dark;
        yield return new WaitForSeconds(tiredTime);
        meshRenderer.material = bright;

        if(GameManager.Instance.UIManager.GetEnemyHP() == 160)
        {
            meshRenderer.material = red;
        }

        GameManager.Instance.playerMove.IsItem(false);
    }

    public void ChangeToRed()
    {
        meshRenderer.material = red;
    }
}
