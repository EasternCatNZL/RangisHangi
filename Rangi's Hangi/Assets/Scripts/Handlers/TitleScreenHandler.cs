using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenHandler : MonoBehaviour {

    public enum currentState
    {
        NOMENU,
        HOMEMENU,
        OPTIONSMENU,
        QUITMENU,
        SOCIALMENU,
        STAGEMENU
    }

    [Header("Scene stuff")]
    public int gameSceneNum = 1;

    [Header("Menus")]
    public GameObject homeMenuStuff;
    public GameObject optionMenuStuff;
    public GameObject quitMenuStuff;
    public GameObject socialMenuStuff;
    public GameObject stageMenuStuff;

    [Header("Start screen text thing")]
    public int fadeDirection = 1;
    public float currentAlpha = 0;
    public float fadeSpeed = 10.0f;

    public Text startText;

    //bool homeMenuOpen = false;
    //bool optionsMenuOpen = false;
    //bool quitMenuOpen = false;
    //bool socialMenuOpen = false;

    currentState state = currentState.NOMENU;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TextFade();
        InputCheck();
	}

    private void InputCheck()
    {
        if (state == currentState.NOMENU)
        {
            if (Input.touchCount > 0)
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        OpenHomeMenu();
                        break;
                    default:
                        break;
                }
            }
        }
        
        //android back button <- dont know about ios
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackKeyLogic();
        }
    }

    void TextFade()
    {
        if (state == currentState.NOMENU)
        {
            currentAlpha += (fadeSpeed * fadeDirection) * Time.deltaTime;
            if (currentAlpha >= 1.0f)
            {
                fadeDirection = -1;
                currentAlpha = 1.0f;
            }
            else if(currentAlpha <= 0.0f)
            {
                fadeDirection = 1;
                currentAlpha = 0.0f;
            }
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, currentAlpha);
        }
    }

    //back key logic
    private void BackKeyLogic()
    {
        switch (state)
        {
            //case currentState.HOMEMENU:
            //    OpenQuitMenu();
            //    break;
            case currentState.OPTIONSMENU:
                CloseOptionMenu();
                break;
            case currentState.SOCIALMENU:
                CloseSocialMenu();
                break;
            case currentState.QUITMENU:
                CloseQuitMenu();
                break;
            case currentState.STAGEMENU:
                CloseStageMenu();
                break;
            default:
                OpenQuitMenu();
                break;
        }
    }

    public void OpenHomeMenu()
    {
        startText.gameObject.SetActive(false);
        homeMenuStuff.SetActive(true);
        state = currentState.HOMEMENU;
    }

    //open quit menu on top of home
    public void OpenQuitMenu()
    {
        quitMenuStuff.SetActive(true);
        state = currentState.QUITMENU;
    }

    public void CloseOptionMenu()
    {
        optionMenuStuff.SetActive(false);
        homeMenuStuff.SetActive(true);
        state = currentState.HOMEMENU;
    }

    public void CloseSocialMenu()
    {
        socialMenuStuff.SetActive(false);
        homeMenuStuff.SetActive(true);
        state = currentState.HOMEMENU;
    }

    public void CloseQuitMenu()
    {
        quitMenuStuff.SetActive(false);
        if (homeMenuStuff.activeInHierarchy)
        {
            state = currentState.HOMEMENU;
        }
        else
        {
            state = currentState.NOMENU;
        }
    }

    public void CloseStageMenu()
    {
        stageMenuStuff.SetActive(false);
        homeMenuStuff.SetActive(true);
        state = currentState.HOMEMENU;
    }

    //button logic
    public void GoToGame()
    {
        SceneManager.LoadScene(gameSceneNum);
    }

    public void OpenOptionMenu()
    {
        optionMenuStuff.SetActive(true);
        homeMenuStuff.SetActive(false);
        state = currentState.OPTIONSMENU;
    }

    public void OpenSocialMenu()
    {
        socialMenuStuff.SetActive(true);
        homeMenuStuff.SetActive(false);
        state = currentState.SOCIALMENU;
    }

    public void OpenStageMenu()
    {
        homeMenuStuff.SetActive(false);
        stageMenuStuff.SetActive(true);
        state = currentState.STAGEMENU;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
