using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIExit : MonoBehaviour,
                                            IPointerDownHandler,
                                            IPointerUpHandler
{
   

    public void OnPointerDown(PointerEventData eventData)
    {
        //UIMng.Instance.Get<UIInfo>(UIType.UIInfo).SetActive(false);
        //UIMng.Instance.Get<UIIngame>(UIType.UIIngame).SetActive(true);
       
                
        UnitMng.Instance.Resume();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        

    }


}
