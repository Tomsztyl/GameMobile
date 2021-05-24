using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthentionLoginController : MonoBehaviour
{
    [Header("Text for Authention")]
    [SerializeField] private InputField _textLogin = null;
    [SerializeField] private int _lenghtLoginMin = 3;
    [SerializeField] private InputField _textPass = null;
    [SerializeField] private int _lenghtPassMin = 8;

    [Header("Variable Message Box")]
    [SerializeField] private GameObject _messageBoxControllerObj = null;
    [SerializeField] private MessageBoxController _messageBoxController = null;

    [Header("Variable to check Logged")]
    [SerializeField] private GameObject _authentionManager = null;


    #region Validation Filed
    private void ValidationMessageBoxController()
    {
        if (_messageBoxController != null)
        {
            return;
        }
        else
        {
            if (_messageBoxControllerObj != null)
            {
                _messageBoxController = _messageBoxControllerObj.GetComponent<MessageBoxController>();
            }
        }

    }
    #region Validation Login
    private bool IsLoginCorrect()
    {
        if (_textLogin.text.Length >= _lenghtLoginMin)
            return true;
        else
            return false;
    }
    #endregion
    #region Valiadtion Password
    private bool IsPasswordCorrect()
    {
        if (_textPass.text.Length >= _lenghtPassMin)
            return true;
        else
            return false;
    }
    #endregion
    #endregion

    #region Execiute Login With DataBase 
    public void ExeciuteLoginPressButtonLogin()
    {
        if (IsLoginCorrect() && IsPasswordCorrect())
            StartCoroutine(Login(_textLogin.text, _textPass.text));
        else if (!IsLoginCorrect() && !IsPasswordCorrect())
        {
            DisplayMessageBox("Complete all fields!");
        }
        else if (!IsLoginCorrect())
        {
            DisplayMessageBox("Your Login is to short minimum sign:[" + _lenghtLoginMin + "]!");       
        }
        else if (!IsPasswordCorrect())
        {
            DisplayMessageBox("Your password does not meet the requirements, minimum sign:[" + _lenghtPassMin + "]!");          
        }
    }
    IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnityMobilePHP/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //error login something went wrong with connect DB
                DisplayMessageBox(www.error);
            }
            else
            {
                if (www.downloadHandler.text == "Login Success")
                {
                    //Login Succes Request From DB
                    DisplayMessageBox(www.downloadHandler.text);
                    PlayerPrefs.SetString("UsernameLogin", username);
                    PlayerPrefs.SetString("UsernamePassword", password);

                    //instantiate logged checker
                    if (_authentionManager != null)
                        Instantiate(_authentionManager);
                    else
                        Debug.LogError("Authention Manager didn't isntantiate");

                    //load scene Game
                    SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                }
                DisplayMessageBox(www.downloadHandler.text);
            }
        }
    }
    private void DisplayMessageBox(string textDisplay)
    {
        //display Message Box Loggin Action 
        _messageBoxControllerObj.SetActive(true);
        ValidationMessageBoxController();
        _messageBoxController.DisplayTextMessageBox(textDisplay);
    }
    #endregion
}
