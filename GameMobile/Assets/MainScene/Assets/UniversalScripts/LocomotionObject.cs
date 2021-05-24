using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LocomotionObject : MonoBehaviour
{
    [SerializeField] private UnityEvent _endLocomotionObject;                    //event when the object has reached its destination
    private float _movePointObjectime;                                           //speed object 
    private Transform[] _pointsMove;                                             //object transfer points
    private int indexMove;                                                       //actual transform point

    #region Move To Next Object
    public void ManagerPoint(Transform[] _pointsMove, float _movePointObjectime, int indexMove)
    {
        this._pointsMove = _pointsMove;
        this.indexMove = indexMove;
        this._movePointObjectime = _movePointObjectime;
        StartCoroutine(NextStep());
    }
    IEnumerator NextStep()
    {
        yield return new WaitForSeconds(_movePointObjectime);
        MoveToNextPoint();
        StartCoroutine(NextStep());
    }
    private void MoveToNextPoint()
    {
        if (_pointsMove.Length > indexMove)
        {
            this.gameObject.transform.SetParent(_pointsMove[indexMove].transform);
            this.gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            _endLocomotionObject.Invoke();
            Destroy(this.gameObject);
        }
        indexMove++;
    }
    #endregion
}
