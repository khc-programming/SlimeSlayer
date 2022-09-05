using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class UISkillSlot : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    SaveSkill skillInfo;

    

    private System.Action<SaveSkill> pointerEnterDelegate;

    private System.Action<SaveSkill> pointerExitDelegate;


   

    public void SetPointerEnterDelegate(System.Action<SaveSkill> function)
    {
        pointerEnterDelegate = function;
    }

    public void SetPointerExitDelegate(System.Action<SaveSkill> function)
    {
        pointerExitDelegate = function;
    }


    

    public void PointerEnter()
    {
        if (pointerEnterDelegate != null)
        {
            if (skillInfo != null)
                pointerEnterDelegate(skillInfo);
        }
    }

    public void PointerExit()
    {
        if (pointerExitDelegate != null)
        {
            if (skillInfo != null)
                pointerExitDelegate(skillInfo);
        }
    }


    
    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        PointerExit();
    }


    public void SetInfo(SaveSkill info)
    {
        skillInfo = info;

    }



    #region //추상 함수 정의부

    public override void Init()
    {


    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }

    #endregion
}
