using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUICollapsed : MonoBehaviour, IPointerEnterHandler
{
    public GameObject extendedUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        extendedUI.SetActive(true);
    }
}
