using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinLibrary;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TextAsset tutorialScriptTxt;

    private bool isTutorial;
    List<string> tutorialScriptList = new List<string>();
    private int tutorialCount;
    private int moveIndex = 10;

    CharacterType characterType;

    private void Start()
    {
        if (GameManager.Instance.CurrentUser.GetIsCompleteTutorial())
        {
            enabled = false;
        }

        else
        {
            StartCoroutine(Tutorial());
        }

        tutorialScriptList = MinConvert.TextAssetToStringList(tutorialScriptTxt, '\n');
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isTutorial)
        {
            tutorialCount++;
            TutorialText();
        }
    }
    public bool GetIsTutorial()
    {
        return isTutorial;
    }

    public void SetIsTutorial(bool isTutorial)
    {
        this.isTutorial = isTutorial;
    }

    private IEnumerator Tutorial()
    {
        isTutorial = true;
        yield return new WaitForSeconds(2.0f);
        Time.timeScale = 0;
        isTutorial = false;
        CharacterText();
        GameManager.Instance.UIManager.ActiveDialogueBox();

    }

    private IEnumerator Tutorial_Move()
    {
        isTutorial = true;
        Time.timeScale = 1;

        yield return new WaitForSeconds(4f);
        isTutorial = false;

        Time.timeScale = 0;
        GameManager.Instance.UIManager.ActiveDialogueBox();
        CharacterText();
    }

    private void TutorialText()
    {
        if (tutorialCount == tutorialScriptList.Count)
        {
            EndTutorial();
            return;
        }

        if (tutorialCount == moveIndex)
        {
            StartCoroutine(Tutorial_Move());
            return;
        }

        CharacterText();
    }
    private void EndTutorial()
    {
        GameManager.Instance.UIManager.ActiveDialogueBox();

        GameManager.Instance.CurrentUser.SetIsCompleteTutorial(true);
        SetIsTutorial(false);

        Time.timeScale = 1f;
        GameManager.Instance.SetLife(ConstantManager.PLAYER_FIRST_LIFE);

        SoundManager.Instance.EndTutorial();

        enabled = false;
    }

    public void CharacterText()
    {
        string[] characterScripts = tutorialScriptList[tutorialCount].Split('&');
        GameManager.Instance.UIManager.CharacterText(characterScripts[0], characterScripts[1]);

        GameManager.Instance.SetCharacterType(ref characterType);
        GameManager.Instance.UIManager.ActiveCharacterInDialogue((int)characterType);
    }
}
