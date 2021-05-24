using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System.Linq;
using UnityEngine.UI;

public class BasketController : CharacterMiniController
{
    #region Mechanism Collecting Eggs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LocomotionObject locomotionObject = collision.transform.GetComponent<LocomotionObject>();
        if (locomotionObject != null)
        {
            EggGameManager eggGameManager = GameObject.FindObjectOfType<EggGameManager>().GetComponent<EggGameManager>();
            if (eggGameManager!=null)
            {
                eggGameManager.SetCollectEgg(1);
                Destroy(locomotionObject.gameObject);
            }

        }
    }
    #endregion
}
