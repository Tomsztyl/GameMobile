using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthentionSceneController : MonoBehaviour
{
    [Header("Controlls Autention Login and Register")]
    [Tooltip("Box Object form to login")]
    [SerializeField] private GameObject _authentionLogin = null;
    [Tooltip("Box Object form to register")]
    [SerializeField] private GameObject _authentionRegister = null;

    private void Awake()
    {
        DeleteAccountFromPlayerPrefs();
    }

    private void DeleteAccountFromPlayerPrefs()
    {
        PlayerPrefs.SetString("UsernameLogin", string.Empty);
        PlayerPrefs.SetString("UsernamePassword", string.Empty);
    }

    #region Trigger Authention Box Active
    public void TriggerBoxAuthentionLogin()
    {
        if (_authentionLogin == null)
            Debug.LogError("Box Login Authention is Null!");
        else
        {
            if (_authentionRegister != null)
            {
                if (_authentionRegister.activeSelf == true)
                {
                    _authentionRegister.SetActive(!_authentionRegister.activeSelf);
                }
            }
            else
            {
                Debug.LogError("Box Register Authention is Null!");
                return;
            }
            

            if (_authentionLogin.activeSelf==true)
            {              
                _authentionLogin.SetActive(!_authentionLogin.activeSelf);
            }
            else
            {
                _authentionLogin.SetActive(!_authentionLogin.activeSelf);
            }
        }
    }
    public void TriggerBoxAuthentionRegister()
    {
        if (_authentionRegister == null)
            Debug.LogError("Box Register Authention is Null!");
        else
        {
            if (_authentionLogin!=null)
            {
                if (_authentionLogin.activeSelf == true)
                {
                    _authentionLogin.SetActive(!_authentionLogin.activeSelf);
                }
            }
            else
            {
                Debug.LogError("Box Login Authention is Null!");
                return;
            }

            if (_authentionRegister.activeSelf == true)
            {              
                _authentionRegister.SetActive(!_authentionRegister.activeSelf);
            }
            else
            {
                _authentionRegister.SetActive(!_authentionRegister.activeSelf);
            }
        }
    }
    #endregion
}
