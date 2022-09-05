using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterInvenItem : BaseUI
{

    PlayerInfo playerInfo;
    SaveCharacter saveInfo;
    private Transform slot;
    private Transform focus;
    private Transform checkBox;
    private Image icon;
    private TMP_Text levelText;
    private List<Image> star = new List<Image>();
    private System.Action<PlayerInfo> clickEvent;
    private int grade;
    private Transform shadow;
    Button button;

    private new TMP_Text name;
    private TMP_Text priceText;



    void OnClick()
    {
        if (clickEvent != null)
            clickEvent(playerInfo);
    }

    public void SetInfo(PlayerInfo info)
    {
        playerInfo = info;

        // ������ �޾����� �κ��丮 ��ư�� ��� ������� �� �� ������ �����ؾ� �մϴ�.
        if (info != null)
        {
            
            //������ ������ ����
            if (icon != null)
            {
                icon.gameObject.SetActive(true);
                
                icon.sprite= info.sprite;
            }

            // ������ ���� ����
            if (levelText != null)
            {
                levelText.text = "LV." + info.level.ToString();
                levelText.gameObject.SetActive(true);
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
                    

            }

            if (focus != null)
            {
                if (info.equip)
                    focus.gameObject.SetActive(true);
                else
                    focus.gameObject.SetActive(false);
            }

            if (checkBox != null)
            {
                if (info.checkbox == true)
                {
                    checkBox.gameObject.SetActive(true);
                }
                else
                {
                    checkBox.gameObject.SetActive(false);
                }
            }

            //if(name != null)
            //{
            //    name.text = info.name;
            //    name.gameObject.SetActive(true);

            //}

            //if (atk != null)
            //{
            //    atk.text = info.maxAttack.ToString();
            //    atk.gameObject.SetActive(true);
            //}

            //if (def != null)
            //{
            //    def.text = info.maxDefence.ToString();
            //    def.gameObject.SetActive(true);
            //}

            //if (hp != null)
            //{
            //    hp.text = info.maxHp.ToString();
            //    hp.gameObject.SetActive(true);
            //}

            //if (job != null)
            //{

            //    job.text =  "("+ info.job.ToString() + ")";
            //    job.gameObject.SetActive(true);
            //}

            if (slot != null)
            {
                slot.gameObject.SetActive(true);
            }

            if (name != null)
            {
                name.text = info.name.ToString();
                name.gameObject.SetActive(true);
            }

            if (priceText != null)
            {
                priceText.text = "���� : " + string.Format("{0:#,0}", info.price) + "��";
                priceText.gameObject.SetActive(true);
            }

        }
    }

    //public void SetInfo(PlayerInfo info)
    //{
    //    SetInfo(info);

    //    if ((info.jobType & characterJobType) != characterJobType)
    //    {
    //        shadow.gameObject.SetActive(true);
    //    }
    //    else
    //        shadow.gameObject.SetActive(false);

    //}


    public void SetButtonListener(System.Action<PlayerInfo> action)
    {
        if (button != null)
            clickEvent = action;
            
    }

    public void Clear()
    {
        SetInfo(null);

        if (icon != null) icon.gameObject.SetActive(false);
        if (levelText != null) levelText.gameObject.SetActive(false);
        if (shadow != null) shadow.gameObject.SetActive(false);
        if (focus != null) focus.gameObject.SetActive(false);
        if(slot != null) slot.gameObject.SetActive(false);
        if (name != null) name.gameObject.SetActive(false);
        if (priceText != null) priceText.gameObject.SetActive(false);
        if (checkBox != null) checkBox.gameObject.SetActive(false);
        //if (name != null) slot.gameObject.SetActive(false);
        //if (atk != null) slot.gameObject.SetActive(false);
        //if (def != null) slot.gameObject.SetActive(false);
        //if (hp != null) slot.gameObject.SetActive(false);
        //if (job != null) slot.gameObject.SetActive(false);

        foreach (var img in star)
            img.gameObject.SetActive(false);


    }

    #region //�߻� �Լ� ���Ǻ�

    public override void Init()
    {
        slot = UtilHelper.Find<Transform>(transform, "Slot", false, false);
        focus = UtilHelper.Find<Transform>(transform, "Focus", false, false);
        checkBox = UtilHelper.Find<Transform>(transform, "CheckBox", false, false);
        icon = UtilHelper.Find<Image>(transform, "Icon", false, false);
        levelText = UtilHelper.Find<TMP_Text>(transform, "LevelText", false, false);

        name = UtilHelper.Find<TMP_Text>(transform, "Name", false, false);
        priceText = UtilHelper.Find<TMP_Text>(transform, "PriceText", false, false);

        star.AddRange(UtilHelper.FindAll<Image>(transform, "Grade", false, false));
        shadow = UtilHelper.Find<Transform>(transform, "Shadow", false, false);
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClick);

     
        //name = UtilHelper.Find<TMP_Text>(transform, "NAME", false, false);
        //atk = UtilHelper.Find<TMP_Text>(transform, "ATK", false, false);
        //def = UtilHelper.Find<TMP_Text>(transform, "DEF", false, false);
        //hp = UtilHelper.Find<TMP_Text>(transform, "HP", false, false);
        //job = UtilHelper.Find<TMP_Text>(transform, "JOB", false, false);
        //print(job);

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
