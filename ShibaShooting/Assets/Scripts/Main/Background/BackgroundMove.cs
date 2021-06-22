using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [Header("스크롤 스피드")]
    [SerializeField]
    private float speed = 0.5f;

    [Header("매테리얼")]
    [SerializeField]
    private Material dark;
    [SerializeField]
    private Material bright;

    private Vector2 offset = Vector2.zero;
    private MeshRenderer meshRenderer;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = bright;
    }

    private void Update()
    {
        offset.x += speed * Time.deltaTime;
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }

    public IEnumerator ChangeBackground()
    {
        if (gameManager.playerMove.GetIsItem()) yield break;

        meshRenderer.material = dark;
        yield return new WaitForSeconds(7f);
        meshRenderer.material = bright;
    }
}
