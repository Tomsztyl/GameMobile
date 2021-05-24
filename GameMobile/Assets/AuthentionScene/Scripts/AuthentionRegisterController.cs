using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AuthentionRegisterController : MonoBehaviour
{
    [Header("Input from Box Register")]
    [SerializeField] private InputField _textLogin = null;
    [SerializeField] private int _lenghtLoginMin = 3;
    [SerializeField] private InputField _textEmail = null;
    [SerializeField] private int _lenghtEmailMin = 6;
    [SerializeField] private InputField _textPass = null;
    [SerializeField] private int _lenghtPassMin = 8;
    [SerializeField] private InputField _textRepPass = null;

    [Header("Variable Message Box")]
    [SerializeField] private GameObject _messageBoxControllerObj = null;
    [SerializeField] private MessageBoxController _messageBoxController = null;


    #region Validation Input Filed
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
    #region Validation Password
    private bool IsPassRepIsCorrcet()
    {
        #region Is Componnet Input Field is null
        if (_textPass == null)
        {
            Debug.LogError("Input Text Password is NULL");
            return false;
        }
        if (_textRepPass == null)
        {
            Debug.LogError("Input Text RepPassword is NULL");
            return false;
        }
        #endregion

        if (_textPass.text == _textRepPass.text && _textPass.text.Length >= _lenghtPassMin)
            return true;
        else
        {
            return false;
        }
    }
    #endregion
    #region Validation Login
    private bool IsLoginCorrect()
    {
        if (_textLogin.text.Length >= _lenghtLoginMin)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    #endregion
    #region Validation Email
    private bool IsEmailCorrect()
    {
        if (_textEmail.text.Length >= _lenghtEmailMin)
            return true;
        else
        {
            return false;
        }
    }
    #endregion
    #endregion
    #region Execiute Form Data Base
    public void ExeciuteRequestToDataBase()
    {
        if (IsPassRepIsCorrcet() && IsLoginCorrect() && IsEmailCorrect())
            StartCoroutine(Register(_textLogin.text, _textPass.text, _textEmail.text));
        else if (!IsPassRepIsCorrcet() && !IsLoginCorrect() && !IsEmailCorrect())
        {
            DisplayMessageBox("Complete all fields!");
        }
        else if (!IsEmailCorrect())
        {
            DisplayMessageBox("Your Email is to short minimum sign:[" + _lenghtEmailMin + "]!");
        }
        else if (!IsLoginCorrect())
        {
            DisplayMessageBox("Your Login is to short minimum sign:[" + _lenghtLoginMin + "]!");
        }
        else if (!IsPassRepIsCorrcet())
        {
            DisplayMessageBox("Your password does not meet the requirements, minimum sign:[" + _lenghtPassMin + "]!");
        }

    }
    IEnumerator Register(string usernameReg, string passwordReg, string mailReg)
    {
        WWWForm form = new WWWForm();
        form.AddField("registerUser", usernameReg);
        form.AddField("registerPass", passwordReg);
        form.AddField("registerMail", mailReg);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnityMobilePHP/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Display Error
                DisplayMessageBox(www.error);
            }
            else
            {
                //Display Request Correct From Data Base
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
