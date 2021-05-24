using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMiniController : MonoBehaviour
{
    public enum InputButton
    {
        LeftUp,
        LeftDown,
        RightUp,
        RightDown,
    }

    enum KindGame
    {
        CatchingEggs,
    }
    [SerializeField] private KindGame kindGame;

    #region Dictionary Point Character
    [SerializeField]
    public CharacterPointsBase _characterPointsBase;

    [System.Serializable]
    public class CharacterPointsBase : SerializableDictionaryBase<InputButton, CharacterPointsProperties> { }

    [System.Serializable]
    public class CharacterPointsProperties
    {
        public Transform _pointsCharacter;
    }
    private CharacterPointsProperties GetValuePropertiesCharacter(InputButton characterPointPlace)
    {
        return _characterPointsBase.FirstOrDefault(x => x.Key == characterPointPlace).Value;
    }
    #endregion

    #region Move Basket
    #region Left Site
    public void MoveLeftUpCharacter()
    {
        InputButton kindMove = InputButton.LeftUp;
        MoveCharacter(kindMove);
    }
    public void MoveLeftDownCharacter()
    {
        InputButton kindMove = InputButton.LeftDown;
        MoveCharacter(kindMove);
    }
    #endregion
    #region Right Site
    public void MoveRightUpCharacter()
    {
        InputButton kindMove = InputButton.RightUp;
        MoveCharacter(kindMove);
    }
    public void MoveRightDownCharacter()
    {
        InputButton kindMove = InputButton.RightDown;
        MoveCharacter(kindMove);
    }
    public void MoveCharacter(InputButton kindMove)
    {
        if (GetValuePropertiesCharacter(kindMove) != null && GetValuePropertiesCharacter(kindMove)._pointsCharacter != null)
        {
            if (kindGame == KindGame.CatchingEggs)
            {
                gameObject.transform.localPosition = GetValuePropertiesCharacter(kindMove)._pointsCharacter.localPosition;
            }
        }
       
    }
    #endregion
    #endregion
}
