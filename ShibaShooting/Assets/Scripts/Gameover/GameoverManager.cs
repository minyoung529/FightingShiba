using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject clearCanvas;
    [SerializeField]
    private GameObject overCanvas;

    private void Start()
    {
        if(PlayerPrefs.GetString("GameOver", "false") == "true")
        {
            clearCanvas.SetActive(false);
            overCanvas.SetActive(true);
            highScoreText.color = new Color(1f, 1f, 1f, 1f);
            scoreText.color = new Color(1f, 1f, 1f, 1f);

        }
        else
        {
            clearCanvas.SetActive(true);
            overCanvas.SetActive(false);
            highScoreText.color = new Color(0f, 0f, 0f, 1f);
            scoreText.color = new Color(0f, 0f, 0f, 1f);
        }

        highScoreText.text = string.Format("HIGHSCORE\n{0}", PlayerPrefs.GetInt("HIGHSCORE"));
        scoreText.text = string.Format("SCORE\n{0}", PlayerPrefs.GetInt("Score"));
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
