using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIGambleItem : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    ItemInfo itemInfo;




    private Image icon;
    private Button button;
    private List<Image> star = new List<Image>();
    private int grade;


    private System.Action onClickDelegate;


    private System.Action<ItemInfo> pointerEnterDelegate;


    private System.Action<ItemInfo> pointerExitDelegate;

    public void SetOnClickDelegate(System.Action function)
    {
        onClickDelegate = function;
    }

    public void SetPointerEnterDelegate(System.Action<ItemInfo> function)
    {
        pointerEnterDelegate = function;
    }

    public void SetPointerExitDelegate(System.Action<ItemInfo> function)
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
            if (itemInfo != null)
                pointerEnterDelegate(itemInfo);
        }
    }

    public void PointerExit()
    {
        if (pointerExitDelegate != null)
        {
            if (itemInfo != null)
                pointerExitDelegate(itemInfo);
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


    public void SetInfo(ItemInfo info)
    {
        itemInfo = info;

        // ������ �޾����� �κ��丮 ��ư�� ��� ������� �� �� ������ �����ؾ� �մϴ�.
        if (info != null)
        {

            //������ ������ ����
            if (icon != null)
            {
                icon.gameObject.SetActive(true);

                icon.sprite = info.sprite;
            }

            // �� ��� ǥ��
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

    #region //�߻� �Լ� ���Ǻ�

    public override void Init()
    {

        icon = UtilHelper.Find<Image>(transform, "Icon", false, true);
        star.AddRange(UtilHelper.FindAll<Image>(transform, "Grade", false, true));
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
            button.interactable = false;
        }
            

        Close();
        
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
