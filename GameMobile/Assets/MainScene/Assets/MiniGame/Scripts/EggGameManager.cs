using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EggGameManager : MonoBehaviour
{
    [Header("Components needed for instantiate")]
    [Tooltip("Menu Switching Game object in hierarchy")]
    [SerializeField] private GameObject menu=null;
    [Tooltip("Place where was spawning egg")]
    [SerializeField]private PlankController[] plankController;
    [Tooltip("Prefab object spawn")]
    [SerializeField]private GameObject egg;
    [Tooltip("Text display score egg")]
    [SerializeField] private Text _textCountEgg;

    [Header("Properties for instantiate")]
    [Tooltip("Default speed transform egg to point")]
    [SerializeField] private float _movePointEggTimeDef = 1f;
    [Tooltip("Increasing Speed transform egg to point")]
    [SerializeField] private float _increasingspeedInstaiatePointEgg = 0.01f;
    [Tooltip("Default speed instantiate egg in plank")]
    [SerializeField] private float _speedInstantiateEgg = 2f;
    [Tooltip("Increasing speed instantiate egg in plank")]
    [SerializeField] private float _increasingspeedInstaiateEgg = 0.02f;
    [Tooltip("Score collect egg")]
    [SerializeField]private int _collectEgg = 0;
    [Tooltip("Live Default")]
    [SerializeField]private int _liveDef = 3;


    private int _liveCalculate;                                     //variable needed to subtraction live default
    private float _movePointEggTimeCalculate;                       //variable needed to Increasing speed transform
    private float _speedInstantiateEggCalculate;                    //variable needed to Increasing speed instantiate

    private bool parseScoreFromDB=false;
    private int scoreEggFromDB;



    private void Start()
    {
        GetScoreFromDataBase();
        SetDefaultProperties();
        StartCoroutine(EggInstantiateCorutine());
    }

    #region Switch Game State
    private void StartEggGame()
    {
        plankController[Random.Range(0, plankController.Length)].InstantiateObject(egg, _movePointEggTimeCalculate);
    }
    public void RestartGame()
    {
        SetDefaultProperties();
        DestroyEgg();

    }
    #endregion

    #region Restart Game Properties
    public void SetDefaultProperties()
    {
        _movePointEggTimeCalculate = _movePointEggTimeDef;
        _speedInstantiateEggCalculate = _speedInstantiateEgg;
        _liveCalculate = _liveDef;
        _collectEgg = 0;
        DisplayCollectEgg();
    }
    private void DestroyEgg()
    {
        LocomotionObject[] eggController = GameObject.FindObjectsOfType<LocomotionObject>();
        foreach (LocomotionObject egg in eggController)
        {
            _collectEgg = 0;
            Destroy(egg.gameObject);
        }
    }
    #endregion

    #region Collect Egg And Display Score
    public void SetCollectEgg(int collect)
    {
        _collectEgg += collect;
        CheckBreakRecord();
        DisplayCollectEgg();
        SpeedTimeInstantiateObject();
    }
    private void DisplayCollectEgg()
    {
        if (_textCountEgg != null)
        {
            _textCountEgg.text = string.Empty + _collectEgg;
        }
    }
    #endregion

    #region Mechanism Spawn Egg
    IEnumerator EggInstantiateCorutine()
    {
        if (parseScoreFromDB)
        {
            StartEggGame();
        }
        yield return new WaitForSeconds(_speedInstantiateEggCalculate);
        StartCoroutine(EggInstantiateCorutine());
    }

    private void SpeedTimeInstantiateObject()
    {
        _movePointEggTimeCalculate -= _increasingspeedInstaiatePointEgg;
        _speedInstantiateEggCalculate -= _increasingspeedInstaiateEgg;
    }
    #endregion

    #region Live Controller
    public void LiveMechanism()
    {
        _liveCalculate--;
        if (_liveCalculate <= 0)
        {
            RestartGame();
        }
    }
    #endregion

    #region Menu Mechanism
    public void MenuSwitch()
    {
        if (menu != null)
        {
            if (menu.activeSelf==false)
            {
                menu.SetActive(!menu.activeSelf);
            }
            else
            {
                menu.SetActive(!menu.activeSelf);
            }
        }
        else
            Debug.LogError("Menu is null");
    }
    #endregion

    #region Get Score DataBase
    private void GetScoreFromDataBase()
    {
       StartCoroutine(GetScore(PlayerPrefs.GetString("UsernameLogin"),PlayerPrefs.GetString("UsernamePassword")));
    }
    IEnumerator GetScore(string username, string password)
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
                Debug.LogError("Incorrect data from data base: "+  www.error);
            }
            else
            {
                int score;
                parseScoreFromDB = int.TryParse(www.downloadHandler.text, out score);

                if (parseScoreFromDB)
                {
                    scoreEggFromDB = score;
                }
                else
                {
                    Debug.LogError("Incorrect data from data base: " + www.downloadHandler.text);
                }
            }
        }
    }
    #endregion

    #region Set Score DataBase
    private void CheckBreakRecord()
    {
        //method check if break the record
        if (_collectEgg> scoreEggFromDB)
        {
            SetScoreFromDataBase(_collectEgg.ToString());
        }
    }
    private void SetScoreFromDataBase(string score)
    {
        StartCoroutine(SetScore(PlayerPrefs.GetString("UsernameLogin"), PlayerPrefs.GetString("UsernamePassword"), score));
    }
    IEnumerator SetScore(string username, string password,string score)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("bestScore", score);

        using (UnityWebRequest www = UnityWebRequest.Post("https://unityfsadsa.000webhostapp.com/gameUnityMobilePHP/SetScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //error login something went wrong with connect DB
                Debug.LogError("Updating points to database failed!: " + www.error);
            }
            else
            {
                if (www.downloadHandler.text== "Record updated successfully")
                {
                    Debug.Log(www.downloadHandler.text);
                    GetScoreFromDataBase();
                }
                else
                {
                    Debug.LogError("Updating points to database failed!: " + www.downloadHandler.text);
                }
            }
        }
    }


    #endregion
}
