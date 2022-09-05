using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquInputEventHandler : MonoBehaviour
{
    UIInventory inventory;
    UICharacterInventory charInventory;

    UIItemPopup itemPopup;
    UICharacterPopup charPopup;

    UIEquipPanel equipPanel;
    UICharacterState characterState;

    // 클릭한 아이템 정보
    public static ItemInfo selected;
    public static PlayerInfo charSelected;


    UISkillBox skillBox;

    public bool isSkillPopMove = false;

    Button skillOnBtn;
    Button skillOffBtn;

    UISkillPop skillPopup;

    public void Init()
    {
        inventory = GetComponentInChildren<UIInventory>(true);
        if (inventory != null)
        {
            inventory.UITabClickEvent(UITabOnClick);
            inventory.UIInvenItemClickEvent(UIInvenItemOnClick);
            inventory.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);
        }

        charInventory = GetComponentInChildren<UICharacterInventory>(true);
        if (charInventory != null)
        {
            charInventory.UITabClickEvent(UITabOnClick);
            charInventory.UIInvenItemClickEvent(UIInvenItemOnClick);
            charInventory.SetItemList(GameDB.GetCharList((int)JobBit.ALL));
        }

        itemPopup = GetComponentInChildren<UIItemPopup>(true);

        if (itemPopup != null)
        {
            itemPopup.SetOnEquipDelegate(OnClickEquipItem);
            itemPopup.SetOffEquipDelegate(OnClickOffEquipItem);
            itemPopup.SetChangeEquipDelegate(OnClickChangeEquipItem);
        }

        charPopup = GetComponentInChildren<UICharacterPopup>(true);

        if (charPopup != null)
        {
            charPopup.SetOnEquipDelegate(OnClickEquipItem);
        }

        equipPanel = GetComponentInChildren<UIEquipPanel>(true);
        characterState = GetComponentInChildren<UICharacterState>(true);


        skillBox = GetComponentInChildren<UISkillBox>(true);
        if(skillBox != null)
        {
            skillBox.SetSkillUpAllDelegate(SkillUpOnClick);
            skillBox.SetPointerEnterAllDelegate(PointerEnter);
            skillBox.SetPointerExitAllDelegate(PointerExit);
        }

        skillPopup = GetComponentInChildren<UISkillPop>(true);

        skillOnBtn = UtilHelper.FindButton(transform, "SkillOnBtn", SkillOnBtnOnClick);
        skillOffBtn = UtilHelper.FindButton(transform, "UISkillBox/SkillOffBtn", SkillOffBtnOnClick);


    }

    // 스킬 박스 용 함수

    public void SkillUpOnClick(SaveSkill info)
    {
        if (GameDB.player == null || info == null)
            return;

        if(GameDB.player.GetPlayerInfo.skillPoint > 0)
        {
            AudioMng.Instance.PlayUI("UI_Button");
            GameDB.player.GetPlayerInfo.skillPoint--;
            info.level++;
            skillBox.SetUpdate();

            
        }
        else
        {
            AudioMng.Instance.PlayUI("UI_Exit");
        }

        
    }

    public void PointerEnter(SaveSkill info)
    {
        skillPopup.transform.position = Input.mousePosition;
        float halfHeigh = Screen.height * 0.5f;
        if (charPopup.transform.position.y > halfHeigh)
        {
            RectTransform rect = skillPopup.GetComponent<RectTransform>();
            if (rect != null) rect.pivot = new Vector2(0, 1);
        }
        else
        {
            RectTransform rect = skillPopup.GetComponent<RectTransform>();
            if (rect != null) rect.pivot = new Vector2(0, 0);
        }
        isSkillPopMove = true;
        skillPopup.Open(info);
    }

    public void PointerExit(SaveSkill info)
    {
        isSkillPopMove = false;
        skillPopup.Close();
    }



    public void SkillOnBtnOnClick()
    {
        if (GameDB.player == null)
            return;

        skillBox.SetSkillList();
        skillBox.gameObject.SetActive(true);
    }

    public void SkillOffBtnOnClick()
    {
        skillBox.gameObject.SetActive(false);
    }

    //


    public void OnClickEquipItem(PlayerInfo playerInfo)
    {
        if (playerInfo == null)
            return;

        AudioMng.Instance.PlayUI("UI_Equip");
        PlayerInfo currInfo = GameDB.GetChar(GameDB.userInfo.GetCharUniqueID);


        if (currInfo != null)
        {
            currInfo.IsUpdate = false;
            charInventory.SetCombatEquipment(currInfo, false);
        }

        ////// 화면 왼쪽의 착용 ui에 보여지도록 처리합니다.
        characterState.EquipItem(playerInfo);


        ////// 화면 오른쪽 인벤토리의 캐릭터을 찾아서 착용표시가 나오도록 처리합니다.
        charInventory.SetCombatEquipment(playerInfo, true);

        equipPanel.TakeOffEquipmentAll();
        equipPanel.EquipItemAll(playerInfo.equipItemArray);




        inventory.SetItemList(GameDB.GetItems((int)inventory.currBitTab), playerInfo.jobType);
        //inventory.SetItemList((GameDB.GetItems((int)ItemBitCategory.ALL), (int)playerInfo.job));

        // 팝업창이 켜져 있다면 비활성화합니다.
        if (charPopup != null)
        {
            // 나중에는 스케일 애니메이션 추가할 것.
            charPopup.gameObject.SetActive(false);
        }

        charPopup.Close();
    }


    // 누군가가 착용하고 있지 않은 아이템이라면 바로 착용 처리합니다.
    // 아이템 팝업창의 착용하기 버튼을 클릭할 때 호출되는 함수입니다.
    public void OnClickEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        // 선택된 아이템 카테고리를 캐릭터가 입고 있는지 확인
        if (GameDB.userInfo.CurrCharacter.WearingTheEquipment((int)itemInfo.category))
        {
            // 입고 있다면 아이템 상태를 벗음 상태로 변경하고 , 인벤토리에서 착용 표시를 끈다.
            inventory.SetCombatEquipment(GameDB.GetItem(GameDB.userInfo.CurrCharacter.equipItemArray[(int)itemInfo.category]), 0, false);

        }

        // 선택된 아이템을 아이템 장착 화면에 표시
        equipPanel.EquipItem(itemInfo);


        //// 화면 오른쪽 인벤토리의 아이템을 찾아서 착용표시가 나오도록 처리합니다.
        inventory.SetCombatEquipment(itemInfo, GameDB.userInfo.CurrCharacter.uniqueID, true);

        // 현재 캐릭터가 착용하고 데이터 입력
        GameDB.userInfo.CurrCharacter.SetWearing((int)itemInfo.category, itemInfo.uniqueID);

        //// 팝업창이 켜져 있다면 비활성화합니다.
        if (itemPopup != null)
        {
            // 나중에는 스케일 애니메이션 추가할 것.
            itemPopup.gameObject.SetActive(false);
        }

        itemPopup.Close();
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);
        characterState.EquipItem(GameDB.GetChar(GameDB.userInfo.CurrCharacter.uniqueID));

        //invettory.
    }

    // 아이템 팝업 창의 해제하기 버튼을 클릭할 때 호출되는 함수입니다. 
    public void OnClickOffEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        int category = (int)itemInfo.category;

        // 아이템을 해제하는 코드를 작성
        SaveCharacter Character = GameDB.userInfo.Get(itemInfo.equipCharacter);

        if (Character != null)
        {
            // 착용을 해제합니다.
            Character.SetWearing(category, 0);
            GameDB.SetCharInfoUpdate(Character.uniqueID);
        }

        // 이전 장비를 해제합니다.
        inventory.SetCombatEquipment(itemInfo, 0, false);

        //
        equipPanel.TakeOffEquipment(itemInfo.category);

        //// 팝업창이 켜져 있다면 비활성화합니다.
        if (itemPopup != null)
        {
            // 나중에는 스케일 애니메이션 추가할 것.
            itemPopup.gameObject.SetActive(false);
        }

        itemPopup.Close();
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);
        characterState.EquipItem(GameDB.GetChar(GameDB.userInfo.CurrCharacter.uniqueID));

    }

    // ----- 아이템 팝업 창의 교체하기 버튼을 클릭할 때 호출되는 함수입니다. -----
    // 교체하기 함수는 다른 캐릭터가 착용하고 있는 아이템에서 착용을 클릭하였을때, 
    // 또는 특정 부위에 해당하는 다른 장비를 착용하고 있고, 현재 아무도 착용하고 있지 않은 아이템의 착용을 클릭했을때 호출되는 함수입니다.
    public void OnClickChangeEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        int category = (int)itemInfo.category;

        // 해당 아이템을 착용하고 있는 캐릭터를 찾습니다.
        SaveCharacter otherCharacter = GameDB.userInfo.Get(itemInfo.equipCharacter);

        // 현재 아이템을 착용하고 있는 다른 캐릭터가 있다면 처리합니다.
        if (otherCharacter != null)
        {
            // 다른 캐릭터의 착용을 해제합니다.
            otherCharacter.SetWearing(category, 0);
            GameDB.SetCharInfoUpdate(otherCharacter.uniqueID);
        }

        // 현재 캐릭터의 유니크 아이디
        int currCharUniqueID = GameDB.userInfo.GetCharUniqueID;

        // 현재 캐릭터가 착용중이었던 아이템 아이디를 찾습니다.
        int itemUniqueID = GameDB.userInfo.GetIDOfItem(currCharUniqueID, category);

        // 현재 캐릭터가 착용중인 아이템 정보를 얻습니다.
        ItemInfo prevItem = GameDB.GetItem(itemUniqueID);

        // 선택한 아이템을 착용합니다.
        GameDB.userInfo.CurrCharacter.SetWearing(category, itemInfo.uniqueID);
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);

        // 이전 장비를 해제합니다.
        inventory.SetCombatEquipment(prevItem, 0, false);

        // 선택한 장비를 착용합니다.
        inventory.SetCombatEquipment(itemInfo, currCharUniqueID, true);

        // 선택한 장비를 화면 왼쪽의 착용 ui에 보여지도록 처리합니다.
        equipPanel.EquipItem(itemInfo);

        if (itemPopup != null)
        {
            // 나중에는 스케일 애니메이션 추가할 것.
            itemPopup.gameObject.SetActive(false);
        }

        itemPopup.Close();
        
        characterState.EquipItem(GameDB.GetChar(GameDB.userInfo.CurrCharacter.uniqueID));

    }



    public void UITabOnClick(UICharacterTab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        JobBit bitCategory = JobBit.ALL;
        System.Enum.TryParse<JobBit>(uiTab.JobBitCategory.ToString(), out bitCategory);

        charInventory.SetUITab(uiTab.JobCategory);
        charInventory.SetItemList(GameDB.GetCharList((int)bitCategory));
    }

    public void UITabOnClick(UITab uiTab)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        ItemBitCategory bitCategory = ItemBitCategory.ALL;
        System.Enum.TryParse<ItemBitCategory>(uiTab.BitCategory.ToString(), out bitCategory);

        inventory.SetUITab(uiTab.Category);
        inventory.SetItemList(GameDB.GetItems((int)bitCategory), (int)GameDB.userInfo.jobType);
    }

    public void UIInvenItemOnClick(PlayerInfo info)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        if (info == null)
        {
            charSelected = info;
            return;
        }
                PopupOpen(info);
        

        charSelected = info;
    }


    public void UIInvenItemOnClick(ItemInfo info)
    {
        AudioMng.Instance.PlayUI("UI_Button");
        if (info == null)
        {
            selected = info;
            return;
        }

        if(info.category == ItemCategory.WEAPON || info.category == ItemCategory.SHIELD || info.category == ItemCategory.PET)
        {
            // && info.sprite != null 처리는 왜 하는 걸까?
            if (info != null && info.sprite != null)
            {
                PopupOpen(info);
            }
        }
        
        else if (info.category == ItemCategory.PORTION)
        {

        }
  


        selected = info;
    }


    public void PopupOpen(PlayerInfo info)
    {


        bool onEquipBtnState = false;
        //bool changeEquipBtnState = false;

        SaveCharacter character = GameDB.userInfo.CurrCharacter;
        
        if(GameDB.userInfo.GetCharUniqueID == info.uniqueID)
        {

        }
        else
        {
            onEquipBtnState = true;
        }

        if(GameDB.currSceneType == SceneType.GameScene)
        {
            charPopup.Open(info);
        }
        else
        {
            charPopup.Open(info, onEquipBtnState);
        }

        
    }

    public void PopupOpen(ItemInfo info)
    {

        
        bool onEquipBtnState = false;
        bool offEquipBtnState = false;
        bool changeEquipBtnState = false;

        
        SaveCharacter character = GameDB.userInfo.CurrCharacter;
        int catecory = (int)info.category;
       


        // 아이템 잡타입에 캐릭터가 포할 될 시
        if ((character.jobType & info.wearType) == character.jobType)
        {
            
            // 아이템을 누군가가 착용 중일 때
            if (info.equip)
            {
                
                // 캐릭터가 해당 아이템 카테고리를 착용 중일 때
                if (character.WearingTheEquipment(catecory))
                {
                    // 아이템을 착용중인게 캐릭터일 때
                    if(info.equipCharacter == character.uniqueID)
                    {
                        offEquipBtnState = true;
                    }
                    else
                    {
                        offEquipBtnState = true;
                        changeEquipBtnState = true;
                    }
                }
                else
                {
                    offEquipBtnState = true;
                    changeEquipBtnState = true;
                }
            }
            else
            {

                if (character.WearingTheEquipment(catecory))
                {
                    onEquipBtnState = true;
                }
                else
                {
                    onEquipBtnState = true;

                }
                
            }
        }
        // 캐릭터가 사용 못하는 아이템이면
        else
        { 
            if(info.equip)
            {
                offEquipBtnState = true;
            }
            else
            {

            }
        }

        if(GameDB.currSceneType == SceneType.GameScene)
        {
            itemPopup.Open(info);
        }
        else
        {
            itemPopup.Open(info, onEquipBtnState, changeEquipBtnState, offEquipBtnState, false, false);
        }
        
        
    }


    public void Run()
    {
        if (skillPopup != null && isSkillPopMove == true)
            skillPopup.transform.position = Input.mousePosition;
       

    }


}
