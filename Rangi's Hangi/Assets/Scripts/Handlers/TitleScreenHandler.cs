using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenHandler : MonoBehaviour {

    public enum currentState
    {
        NOMENU,
        HOMEMENU,
        OPTIONSMENU,
        QUITMENU,
        SOCIALMENU
    }

    public int gameSceneNum = 1;

    public GameObject homeMenuStuff;
    public GameObject optionMenuStuff;
    public GameObject quitMenuStuff;
    public GameObject socialMenuStuff;

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
                        break;
                    default:
                        break;
                }
            }
        }
        

        //android back button <- dont know about ios
        if (Input.GetKeyDown(KeyCode.Escape))
        {

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
                CloseOptionMenu();
                break;
            case currentState.QUITMENU:
                CloseQuitMenu();
                break;
            default:
                OpenQuitMenu();
                break;
        }
    }

    //open quit menu on top of home
    void OpenQuitMenu()
    {
        quitMenuStuff.SetActive(true);
        state = currentState.QUITMENU;
    }

    void CloseOptionMenu()
    {
        optionMenuStuff.SetActive(false);
        homeMenuStuff.SetActive(true);
        state = currentState.HOMEMENU;
    }

    void CloseQuitMenu()
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

    //button logic
    void GoToGame()
    {
        SceneManager.LoadScene(gameSceneNum);
    }

    void OpenOptionMenu()
    {
        optionMenuStuff.SetActive(true);
        homeMenuStuff.SetActive(false);
        state = currentState.OPTIONSMENU;
    }

    void OpenSocialMenu()
    {
        socialMenuStuff.SetActive(true);
        homeMenuStuff.SetActive(false);
        state = currentState.SOCIALMENU;
    }
}
