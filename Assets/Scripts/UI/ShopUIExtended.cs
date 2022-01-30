using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUIExtended : MonoBehaviour, IPointerExitHandler
{
    public GameObject collapsedUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        collapsedUI.SetActive(true);
    }
}
