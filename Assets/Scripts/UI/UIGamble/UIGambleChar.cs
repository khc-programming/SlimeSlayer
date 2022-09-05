using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIGambleChar : BaseUI, IPointerEnterHandler, IPointerExitHandler 
{
    PlayerInfo playerInfo;
    
    
    
    
    private Image icon;
    private Button button;
    private List<Image> star = new List<Image>();
    private int grade;

    
    private System.Action onClickDelegate;

    
    private System.Action<PlayerInfo> pointerEnterDelegate;

    
    private System.Action<PlayerInfo> pointerExitDelegate;

    public void SetOnClickDelegate(System.Action function)
    {
        onClickDelegate = function;
    }

    public void SetPointerEnterDelegate(System.Action<PlayerInfo> function)
    {
        pointerEnterDelegate = function;
    }

    public void SetPointerExitDelegate(System.Action<PlayerInfo> function)
    {
        pointerExitDelegate = function;
    }

    public void OnClick()
    {
        if (pointerEnterDelegate != null)
        {
            onClickDelegate();
                
        }
    }

    public void PointerEnter()
    {
        if (onClickDelegate != null)
        {
            if (playerInfo != null)
                pointerEnterDelegate(playerInfo);
        }
    }

    public void PointerExit()
    {
        if (pointerExitDelegate != null)
        {
            if (playerInfo != null)
                pointerExitDelegate(playerInfo);
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


    public void SetInfo(PlayerInfo info)
    {
        playerInfo = info;

        // 정보를 받았을때 인벤토리 버튼이 어떻게 보여줘야 할 지 정보를 설정해야 합니다.
        if (info != null)
        {

            //아이템 아이콘 설정
            if (icon != null)
            {
                icon.gameObject.SetActive(true);

                icon.sprite = info.sprite;
            }

            // 별 등급 표시
            for (int i = star.Count - 1; i >= 0; --i)
            {
                if ((info.grade - 1) < i)
                    star[i].gameObject.SetActive(false);
                else
                {
                    star[i].gameObject.SetActive(true);
                }

                gameObject.SetActive(true);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    #region //추상 함수 정의부

    public override void Init()
    {
        
        icon = UtilHelper.Find<Image>(transform, "Icon", false, true);
        star.AddRange(UtilHelper.FindAll<Image>(transform, "Grade", false, true));
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClick);

        gameObject.SetActive(false);
    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
