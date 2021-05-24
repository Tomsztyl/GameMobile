using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxController : MonoBehaviour
{
    [SerializeField] private Text _textDisplayBoxMessage=null;
    
    #region Check Message Is Accept Client
    public void HideBoxMessage()
    {
        DisplayTextMessageBox(string.Empty);
        gameObject.SetActive(false);
    }
    #endregion
    #region Display Message Box 
    public void DisplayTextMessageBox(string textDisplay)
    {
        DateTime dt = DateTime.Now;
        _textDisplayBoxMessage.text = "@MANAGER[" + dt.ToString("HH:mm") + "]~" + textDisplay;
    }
    #endregion
}
