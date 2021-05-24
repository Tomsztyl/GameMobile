using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum KindAnimationButton
{
    TriggerAnimation360,
}
public class SimplyAnimationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Properties Animation Button Input /Image Component")]
    [SerializeField] private float _standByValue=0f;
    [SerializeField] private float _standByValueMax=1f;
    [SerializeField] private float _accelerationValue = 2f;
    [SerializeField] private float _calculateValue;
    [SerializeField] public KindAnimationButton kindAnimationButton = KindAnimationButton.TriggerAnimation360;
    private bool isPonterOnSlot = false;

    private Image imageComponent = null;



    private void Start()
    {
        if (imageComponent==null)
        {
            imageComponent=GetComponent<Image>();
            if (kindAnimationButton == KindAnimationButton.TriggerAnimation360)
            {
                imageComponent.fillAmount = 0;
                _standByValue = imageComponent.fillAmount;
            }
       
        }       
    }
    private void Update()
    {
        if (kindAnimationButton == KindAnimationButton.TriggerAnimation360)
            TriggerAnimation360();
    }
    private void TriggerAnimation360()
    {
        if (isPonterOnSlot)
        {
            if (_calculateValue < _standByValueMax)
            {
                _calculateValue += _accelerationValue * Time.deltaTime;
                imageComponent.fillAmount = _calculateValue;
            }
            else
            {
                _calculateValue = _standByValueMax;
                imageComponent.fillAmount = _calculateValue;
            }
        }
        else
        {
            if (_calculateValue > _standByValue)
            {
                _calculateValue -= _accelerationValue * Time.deltaTime;
                imageComponent.fillAmount = _calculateValue;
            }
            else
            {
                _calculateValue = _standByValue;
                imageComponent.fillAmount = _calculateValue;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPonterOnSlot = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isPonterOnSlot = false;
    }

}
