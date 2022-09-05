using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMng : Mng<UIMng>
{
    public EventSystem eventMng;
    private Dictionary<UIType, BaseUI> uiDic =
        new Dictionary<UIType, BaseUI>();

    // ui프리팹을 읽어들일 경로
    private readonly string path = "Prefab/UI/";

    public void SetMoney(int money)
    {
        UIEquipmentBox.moneyText.text = string.Format("{0:#,0}", money);
        UIShop.moneyText.text = string.Format("{0:#,0}", money);
        UIUpgradeShop.moneyText.text = string.Format("{0:#,0}", money);
        UIGamble.moneyText.text = string.Format("{0:#,0}", money);
        UIQuest.moneyText.text = string.Format("{0:#,0}", money);
    }

    #region // Mng 추상 메소드 정의부


    public override void Run()
    {
        
        if ((GameDB.MngEnabled & (int)MngType.UIMng) != (int)MngType.UIMng)
            return;

        if(Input.GetKeyDown(KeyCode.B))
        {
            eventMng.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            eventMng.enabled = true;
        }

        uiDic[UIType.UIIngame].Run();
        uiDic[UIType.UIMenu].Run();
        uiDic[UIType.UIGamble].Run();
        uiDic[UIType.UIDialogMng].Run();
        uiDic[UIType.UIQuest].Run();
        uiDic[UIType.UIInfoBox].Run();
        uiDic[UIType.UIUpgradeShop].Run();
        uiDic[UIType.UIShop].Run();

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameDB.Load("PlayerInfo.json");
            UIInventory i = GameObject.FindObjectOfType<UIInventory>();
            i.SetUITab(ItemCategory.ALL);
            i.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);
            //i.SetUITab(ItemCategory.WEAPON);
            //i.SetItemList(GameDB.GetItems((int)ItemBitCategory.WEAPON), (int)GameDB.userInfo.jobType);
            //i.SetUITab(ItemCategory.ALL);
            //i.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);

            UICharacterInventory cinven = GameObject.FindObjectOfType<UICharacterInventory>();
            cinven.SetUITab(Job.ALL);
            cinven.SetItemList(GameDB.GetCharList((int)JobBit.ALL));
            //cinven.SetUITab(Job.WARRIOR);
            //cinven.SetItemList(GameDB.GetCharList((int)JobBit.WARRIOR));
            //cinven.SetUITab(Job.ALL);
            //cinven.SetItemList(GameDB.GetCharList((int)JobBit.ALL));
        }
    }

    public override void FixRun()
    {

    }

    public override void LateRun()
    {

    }
    public override void Init()
    {
        //GameDB.Load("PlayerInfo.json");
        mngType = MngType.UIMng;
        eventMng = gameObject.AddComponent<EventSystem>();
        gameObject.AddComponent<StandaloneInputModule>();

        Add(UIType.FadeUI, false);
        Add(UIType.LoadingUI, false);
        Add(UIType.UIIngame, false);
        Add(UIType.UIInfoBox, false);
        Add(UIType.UIMenu, false);
        Add(UIType.UIShop, false);
        Add(UIType.UIUpgradeShop, false);
        Add(UIType.UITitle, false);
        Add(UIType.UIGamble, false);
        Add(UIType.UIDialogMng, true);
        Add(UIType.UIQuest, false);
        Add(UIType.UIDungeon, false);


    }


    public void SetQuestList()
    {
        UIQuest quest = Get<UIQuest>(UIType.UIQuest);
        if (quest != null) quest.SetQuestList();
    }

    // UIDialogMng start

    public void OnTouchMode(string name, bool idDeal)// , QuestMode mode)
    {
        UIDialogMng dialogMng = Get<UIDialogMng>(UIType.UIDialogMng);
        if (dialogMng != null) dialogMng.OnTouchMode(name , idDeal);// , mode);
    }

    public void OffTouchMode()
    {
        UIDialogMng dialogMng = Get<UIDialogMng>(UIType.UIDialogMng);
        if (dialogMng != null) dialogMng.OffTouchMode();
    }

    public UIDialog UIDialogCreate(Transform pivot, string name)//, QuestMode mode = QuestMode.TITLE)
    {
        UIDialogMng dialogMng = Get<UIDialogMng>(UIType.UIDialogMng);
        if (dialogMng != null) return dialogMng.UIDialogCreate(pivot, name);//, mode);

        return null;
    }


    public void DelaySet()
    {
        //uiDic[UIType.UIInfoBox].Init();
    }

    public override void OnActive()
    {
      //  GameDB.MngEnabled += (int)MngType.UIMng;
    }
    public override void OnDeactive()
    {
      //  GameDB.MngEnabled -= (int)MngType.UIMng;
    }
    public override void OnGameEnable()
    {
      //  GameDB.MngEnabled += (int)MngType.UIMng;
    }
    public override void OnGameDisable()
    {
      //  GameDB.MngEnabled -= (int)MngType.UIMng;
    }

    public override void SetActive(bool state)
    {
        if (state)
        {
            OnActive();
        }
        else
        {
            OnDeactive();
        }
        gameObject.SetActive(state);
    }
    public override void SetEnable(bool state)
    {
        if (state)
        {
            OnGameEnable();
        }
        else
        {
            OnGameDisable();
        }
        enabled = state;
    }

    #endregion

    public void SetEventSystme(bool path)
    {
        eventMng.enabled = path;
    }

    public void touchCansle()
    {
        UIIngame ingame = Get<UIIngame>(UIType.UIIngame);
        if (ingame != null)
            ingame.touchCansle();
    }
    public void FadeIn(float targetTime)
    {
        FadeUI fade = Get<FadeUI>(UIType.FadeUI);
        if (fade != null)
            fade.FadeIn(false, targetTime);
    }

    public void FadeOut(float targetTime)
    {
        FadeUI fade = Get<FadeUI>(UIType.FadeUI);
        if (fade != null)
            fade.FadeOut(true, targetTime);
    }

    public void FadeInDelay(float delayTime, float targetTime = 1)
    {
        StartCoroutine(IEFadeInDelay(delayTime, targetTime));
    }

    public void FadeOutDelay(float delayTime, float targetTime = 1)
    {
        StartCoroutine(IEFadeOutDelay(delayTime, targetTime));
    }

    private IEnumerator IEFadeInDelay(float delayTime, float targetTime = 1)
    {
        yield return new WaitForSeconds(delayTime);
        FadeIn(targetTime);
    }
    private IEnumerator IEFadeOutDelay(float delayTime, float targetTime = 1)
    {
        yield return new WaitForSeconds(delayTime);
        FadeOut(targetTime);
    }

    // UIIngame 모드를 변경하는 함수 게임/보스/로비 모드
    public void modeChange()
    {
        UIIngame ingame = Get<UIIngame>(UIType.UIIngame);
        if (ingame != null) ingame.modeChange();
    }

    public void SetGetGold(int gold)
    {
        UIIngame ingame = Get<UIIngame>(UIType.UIIngame);
        if (ingame != null) ingame.SetGetGold(gold);

    }

    public void MinimapSetUpdate()
    {
        UIIngame ingame = Get<UIIngame>(UIType.UIIngame);
        if (ingame != null) ingame.MinimapSetUpdate();      
    }

    public void MinimapSet(bool state = false)
    {
        UIIngame ingame = Get<UIIngame>(UIType.UIIngame);
        if (ingame != null) ingame.MinimapSet(state);
    }

    public UIHp UIHpCreate(Transform pivot, ShareInfo info)
    {
        UIIngame ingame = Get<UIIngame>(UIType.UIIngame);
        if (ingame != null) return ingame.UIHpCreate(pivot, info);

        return null;
    }



    // 비동기로 로드하는 코드는 차후 정리해서 수업을 진행합니다.
    public BaseUI Add(UIType ui, bool activeState = true , bool init = true)
    {
        // ui가 등록되어 있다면 등록되어 있는 ui를 넘겨주도록 합니다.
        if (uiDic.ContainsKey(ui))
        {
            uiDic[ui].SetActive(activeState);
            return uiDic[ui];
        }

        
        BaseUI baseUI = this.Instantiate<BaseUI>(path + ui,
                                                  Vector3.zero,
                                                  Quaternion.identity,
                                                  init,
                                                  transform);

    

        
        if (baseUI != null)
        {
            baseUI.SetActive(activeState);
            uiDic.Add(ui, baseUI);
        }
        return baseUI;
    }
    public void Del(UIType ui)
    {
        if (uiDic.ContainsKey(ui))
        {
            uiDic[ui].Destroy();
            uiDic.Remove(ui);
        }
    }
    // 특정 ui에게 특정 함수를 호출하도록 강제하기 위한 함수입니다.
    public void CallEvent(UIType ui,
                           string function,
                           System.Object obj = null)
    {
        
        if (uiDic.ContainsKey(ui))
            uiDic[ui].SendMessage(function,
                obj,
                SendMessageOptions.DontRequireReceiver);

    }

    // uiDic에 원하는 ui가 있다면 받아온다
    public T Get<T>(UIType ui) where T : BaseUI
    {
        if (uiDic.ContainsKey(ui))
            return uiDic[ui].GetComponent<T>();

        return null;
    }

    // 특정 ui를 찾아서 상태를 변경합니다.
    public void SetActive(UIType ui, bool state)
    {
        if (uiDic.ContainsKey(ui))
        {
            uiDic[ui].SetActive(state);
        }
            
    }

    public IEnumerator IEShowDelay(float targetTime, UIType ui)
    {
        yield return new WaitForSeconds(targetTime);
        SetActive(ui, true);
    }

    public void ShowDelay(float targetTime, UIType ui)
    {
        StartCoroutine(IEShowDelay(targetTime, ui));
    }
}
