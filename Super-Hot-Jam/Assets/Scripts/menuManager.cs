using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class menuManager : MonoBehaviour
{

    public GameObject title_screen;
    public GameObject menu_screen;
    public GameObject credits_screen;
    public GameObject readme_screen;

    public GameObject time_text;

    public GameObject play_button;
    public GameObject play_description;

    public GameObject readme_button;
    public GameObject readme_description;

    public GameObject credits_button;
    public GameObject credits_description;

    public GameObject quit_button;
    public GameObject quit_description;

    // Start is called before the first frame update
    void Start()
    {
        title_screen.SetActive(true);
        menu_screen.SetActive(false);
        credits_screen.SetActive(false);
        readme_screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (title_screen.activeSelf && Input.GetAxis("Submit") == 1)
        {
            SwitchTo(credits_screen);
            SwitchTo(menu_screen);
        }

        time_text.GetComponent<Text>().text = System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00");

        if (EventSystem.current.currentSelectedGameObject == credits_button)
        {
            credits_description.SetActive(true);
        }
        else
        {
            credits_description.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject == quit_button)
        {
            quit_description.SetActive(true);
        }
        else
        {
            quit_description.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject == readme_button)
        {
            readme_description.SetActive(true);
        }
        else
        {
            readme_description.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject == play_button)
        {
            if (Random.Range(0.0f, 1.0f) >= 0.02f)
            {
                play_description.SetActive(true);
            }
            else
            {
                play_description.SetActive(false);
            }
        }
        else
        {
            play_description.SetActive(false);
        }
    }

    public void SwitchTo(GameObject new_screen)
    {
        title_screen.SetActive(false);
        menu_screen.SetActive(false);
        credits_screen.SetActive(false);
        readme_screen.SetActive(false);
        new_screen.SetActive(true);
        new_screen.GetComponentInChildren<Button>().Select();
    }

    public void CloseApp()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
