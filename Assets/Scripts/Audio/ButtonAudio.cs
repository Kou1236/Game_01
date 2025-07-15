using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour
{
    public int audioIndex;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        AudioManager.Instance.PlayNext(audioIndex);
    }

}
