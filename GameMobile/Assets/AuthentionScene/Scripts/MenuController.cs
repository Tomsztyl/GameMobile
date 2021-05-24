using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Tooltip("Object to instantiate stats players")]
    [SerializeField] private GameObject stats = null;

    [Tooltip("Button in menu")]
    [SerializeField] private Button newGame;
    [Tooltip("Button in menu")]
    [SerializeField] private Button logout;

    private GameObject _statsInstantiate = null;

    private void Update()
    {
        //walidation if player could press button
        CheckActiveButton(newGame);
        CheckActiveButton(logout);
    }

    #region Walidation Button Logout and New Game
    private void CheckActiveButton(Button buttonActive)
    {
        //check if player is logged 
        if (buttonActive != null)
        {
            if (string.IsNullOrEmpty(PlayerPrefs.GetString("UsernameLogin")) || string.IsNullOrEmpty(PlayerPrefs.GetString("UsernamePassword")))
                buttonActive.enabled = false;
            else
                buttonActive.enabled = true;
        }
    }
    #endregion

    #region Button Press On Click Menu
    public void TopPlayerButton()
    {
        //instantiate players top and stats player
        if (_statsInstantiate == null)
        {
            _statsInstantiate = Instantiate(stats, GameObject.FindObjectOfType<Canvas>().transform);
        }
        else
            Destroy(_statsInstantiate.gameObject);
    }
    public void LogoutButton()
    {
        //delete data login and password from player prefs
        LogOut();
        //change scene on AuthenticScene
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name!= "AuthenticScene")
        {
            SceneManager.LoadScene("AuthenticScene", LoadSceneMode.Single);
        }        
    }
    private void LogOut()
    {
        //delete data login and password from player prefs
        PlayerPrefs.SetString("UsernameLogin", string.Empty);
        PlayerPrefs.SetString("UsernamePassword", string.Empty);
    }
    public void ExitButton()
    {
        //delete data login and password from player prefs
        LogOut();

        //exit application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
    #endregion
}
