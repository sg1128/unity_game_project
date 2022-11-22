using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Synergy_Info2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject info1;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        info1.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        info1.SetActive(false);
    }
}
