using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    [SerializeField] private Text _textReneveStatsPlayersTop = null;
    [SerializeField] private Text _textRenevePlayer = null;
    [SerializeField] private float _timeToReneveStats = 5f;

    private void Start()
    {
        StartCoroutine(UpdateStats());
    }

    #region Execiute Stats With DataBase
    IEnumerator UpdateStats()
    {
        StartCoroutine(Stats());
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("UsernameLogin")) && !string.IsNullOrEmpty(PlayerPrefs.GetString("UsernamePassword")))
        {
            StartCoroutine(StatsPlayer(PlayerPrefs.GetString("UsernameLogin"), PlayerPrefs.GetString("UsernamePassword")));
        }
        yield return new WaitForSeconds(_timeToReneveStats);
        StartCoroutine(UpdateStats());
    }

    #region Stats Players Top
    IEnumerator Stats()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnityMobilePHP/GetScoreTop.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //error stats www.error
                DisplayStatsPlayers(www.error);
            }
            else
            {
                //get stats www.downloadHandler.text
                DisplayStatsPlayers(www.downloadHandler.text);
            }
        }
        yield return new WaitForSeconds(_timeToReneveStats);
    }
    private void DisplayStatsPlayers(string textDisplay)
    {
        if (_textReneveStatsPlayersTop != null)
            _textReneveStatsPlayersTop.text = textDisplay;
    }
    #endregion

    #region Stats Player
    IEnumerator StatsPlayer(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnityMobilePHP/GetScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //error login something went wrong with connect DB
                _textRenevePlayer.text = www.error;
            }
            else
            {
                //Login Succes Request From DB
                _textRenevePlayer.text =username+":["+  www.downloadHandler.text+ "] score";
            }
        }
    }
    #endregion
    #endregion
}
