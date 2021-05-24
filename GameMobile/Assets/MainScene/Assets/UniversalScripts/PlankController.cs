using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankController : MonoBehaviour
{
    [Header("Array Tranform for move object")]
    [Tooltip("Select point to move Locomotion Object")]
    [SerializeField] private Transform[] _pointInstantiate;

    public void InstantiateObject(GameObject objectSpawning,float _movePointObjectTime)
    {
        int indexMove = 0;
        var instantiateObject = Instantiate(objectSpawning, _pointInstantiate[indexMove]);
        instantiateObject.SetActive(true);
        instantiateObject.transform.localPosition = Vector3.zero;
        LocomotionObject locomotionObject = instantiateObject.GetComponent<LocomotionObject>();
        if(locomotionObject != null)
        {
            locomotionObject.ManagerPoint(_pointInstantiate, _movePointObjectTime, indexMove);
        }
    }
}
