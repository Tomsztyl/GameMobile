using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthentionManager : MonoBehaviour
{
    [Tooltip("Second to check account")]
    [SerializeField] private float timeToCheckLogin = 5f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //start checking is logged player
        StartCoroutine(CheckLogin(PlayerPrefs.GetString("UsernameLogin"), PlayerPrefs.GetString("UsernamePassword")));
    }

    #region Check Login
    /// <summary>
    /// Check if have connect with data base
    /// </summary>
    /// <param name="username"></param>
    /// username to connect with data base [PlayerPrefs.GetString("UsernamePassword")] variable
    /// <param name="password"></param>
    /// password to connect with data base [PlayerPrefs.GetString("UsernameLogin")] vairable()
    /// <returns></returns>
    IEnumerator CheckLogin(string username, string password)
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
                Debug.Log(PlayerPrefs.GetString("UsernameLogin")+ " " + www.error);
                LogOut();
            }
            else
            {
                if (www.downloadHandler.text == "Login Success")
                {
                    //Login Succes Request From DB
                    Debug.Log(PlayerPrefs.GetString("UsernameLogin") + " " + www.downloadHandler.text);
                }
                else
                {
                    //something is worng logout
                    Debug.Log(PlayerPrefs.GetString("UsernameLogin") + " " + www.downloadHandler.text);
                    LogOut();
                }

            }
            yield return new WaitForSeconds(timeToCheckLogin);
            StartCoroutine(CheckLogin(PlayerPrefs.GetString("UsernameLogin"), PlayerPrefs.GetString("UsernamePassword")));
        }
    }
    private void LogOut()
    {
        //delete data login and password from player prefs
        PlayerPrefs.SetString("UsernameLogin", string.Empty);
        PlayerPrefs.SetString("UsernamePassword", string.Empty);

        //change scene on AuthenticScene
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "AuthenticScene")
        {
            SceneManager.LoadScene("AuthenticScene", LoadSceneMode.Single);
        }
        Destroy(this.gameObject);
    }
    #endregion
}
