using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterSlot : BaseUI
{

    PlayerInfo playerInfo;
    private Transform slot;
    private Transform focus;
    private Image icon;
    private TMP_Text levelText;
    private List<Image> star = new List<Image>();
    private System.Action<PlayerInfo> clickEvent;
    private int grade;
    private Transform shadow;
    Button button;

    private TMP_Text name;
    private TMP_Text atk;
    private TMP_Text def;
    private TMP_Text hp;
    private TMP_Text job;

    public Color maxColor = new Color(0.2f, 0.8f, 0.4f);

    void OnClick()
    {
        if (clickEvent != null)
            clickEvent(playerInfo);
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

            // 아이템 레벨 설정
            if (levelText != null)
            {
                if (info.level >= 100) levelText.color = maxColor;
                else levelText.color = Color.black;
                levelText.text = "LV." + info.level.ToString();
                levelText.gameObject.SetActive(true);
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


            }

            if (focus != null)
            {
                if (info.equip)
                    focus.gameObject.SetActive(true);
                else
                    focus.gameObject.SetActive(false);
            }

            if (name != null)
            {
                name.text = info.name;
                name.gameObject.SetActive(true);

            }

            if (atk != null)
            {
                atk.text = $"{info.lastState[0].ToString()}({info.itemAttack})";
                atk.gameObject.SetActive(true);
            }

            if (def != null)
            {
                def.text = $"{info.lastState[1].ToString()}({info.itemDefence})";
                def.gameObject.SetActive(true);
            }

            if (hp != null)
            {
                hp.text = $"{info.lastState[2].ToString()}({info.itemHP})";
                hp.gameObject.SetActive(true);
            }

            if (job != null)
            {
                string s = "";
                if(info.job == Job.WARRIOR)
                {
                    s = "전사";
                }
                else if(info.job == Job.WIZARD)
                {
                    s = "마법사";
                }


                job.text = "(" + s + ")";
                job.gameObject.SetActive(true);
            }

            if (slot != null)
            {
                slot.gameObject.SetActive(true);
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
        if (slot != null) slot.gameObject.SetActive(false);

        if (name != null) slot.gameObject.SetActive(false);
        if (atk != null) slot.gameObject.SetActive(false);
        if (def != null) slot.gameObject.SetActive(false);
        if (hp != null) slot.gameObject.SetActive(false);
        if (job != null) slot.gameObject.SetActive(false);

        foreach (var img in star)
            img.gameObject.SetActive(false);


    }

    #region //추상 함수 정의부

    public override void Init()
    {
        slot = UtilHelper.Find<Transform>(transform, "Slot", false, false);
        focus = UtilHelper.Find<Transform>(transform, "Focus", false, false);
        icon = UtilHelper.Find<Image>(transform, "Icon", false, false);
        levelText = UtilHelper.Find<TMP_Text>(transform, "LevelText", false, false);
        star.AddRange(UtilHelper.FindAll<Image>(transform, "Grade", false, false));
        shadow = UtilHelper.Find<Transform>(transform, "Shadow", false, false);
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClick);


        name = UtilHelper.Find<TMP_Text>(transform, "NAME", false, false);
        atk = UtilHelper.Find<TMP_Text>(transform, "ATK", false, false);
        def = UtilHelper.Find<TMP_Text>(transform, "DEF", false, false);
        hp = UtilHelper.Find<TMP_Text>(transform, "HP", false, false);
        job = UtilHelper.Find<TMP_Text>(transform, "JOB", false, false);
        

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
