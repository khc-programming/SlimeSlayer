using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterShop : BaseUI
{
    // 인벤토리의 슬롯의 카운트
    private int slotCount = 100;

    private UICharacterInvenItem invenCharacterItemPrefab;

    public List<UICharacterInvenItem> invenCharacterItemList = new List<UICharacterInvenItem>();
    private Dictionary<int, UICharacterInvenItem> invenCharacterItemDic = new Dictionary<int, UICharacterInvenItem>();


    // 인벤 아이템 목록을 저장함 부모 위치
    private GridLayoutGroup gridLayout;

    private Job currTab = Job.WARRIOR;

    private UICharacterTabBtns tabBtns;
    private Transform tabFocus;

    private ScrollRect scrollRect;




    public void UITabClickEvent(System.Action<UICharacterTab> onClick)
    {
        tabBtns.SetButtonListener(onClick);
    }

    public void UIInvenItemClickEvent(System.Action<PlayerInfo> onClick)
    {
        foreach (var value in invenCharacterItemList)
        {
            value.SetButtonListener(onClick);
        }
    }


    public void SetUITab(Job category)
    {
        if (currTab == category)
            return;
        currTab = category;
        tabBtns.SetHighlightActive(category);
        tabFocus.position = tabBtns.GetFocusPos(category);
        tabFocus.gameObject.SetActive(true);


        Clear();

        scrollRect.normalizedPosition = new Vector2(0, 1);

    }

    public void CreateEmptySlot(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            UICharacterInvenItem item = Instantiate(invenCharacterItemPrefab, gridLayout.transform);
            if (item != null)
            {
                item.Init();
                invenCharacterItemList.Add(item);
            }
        }
    }

    public void SetCombatEquipment(PlayerInfo character, bool equip)
    {

        if (character == null)
            return;

        character.equip = equip;

        if (invenCharacterItemDic.ContainsKey(character.uniqueID))
            invenCharacterItemDic[character.uniqueID].SetInfo(character);



        if (character.equip == true)
        {
            GameDB.userInfo.SetCharUniqueID = character.uniqueID;
            GameDB.userInfo.jobType = character.jobType;
        }

        //GameDB.userInfo.listOfChar.Clear();
        //GameDB.userInfo.listOfChar.AddRange(GameDB.charDic.Values);


    }
    public void SetItemList(List<PlayerInfo> itemlist)
    {
        invenCharacterItemDic.Clear();


        for (int i = 0; i < itemlist.Count; ++i)
        {
            invenCharacterItemList[i].SetInfo(itemlist[i]);

            // 아이템 갱신등이 발생될때 데이터를 갱신하기 위한 딕셔너리 입니다.
            invenCharacterItemDic.Add(itemlist[i].uniqueID, invenCharacterItemList[i]);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < invenCharacterItemList.Count; ++i)
            invenCharacterItemList[i].Clear();
    }


    #region //추상 함수 정의부

    public override void Init()
    {
        invenCharacterItemPrefab = Resources.Load<UICharacterInvenItem>("Prefab/UI/UICharacterInvenItem");
        gridLayout = GetComponentInChildren<GridLayoutGroup>(true);
        CreateEmptySlot(slotCount);


        tabBtns = GetComponentInChildren<UICharacterTabBtns>(true);
        if (tabBtns != null)
            tabBtns.Init();

        tabFocus = transform.Find("TabFocus");
        scrollRect = GetComponentInChildren<ScrollRect>(true);

        SetUITab(Job.WIZARD);
        //GameDB.randomCreate(20);
        //SetItemList(GameDB.GetItems());
        isInit = true;

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

    void Start()
    {

    }


}
