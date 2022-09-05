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

    // Ŭ���� ������ ����
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

    // ��ų �ڽ� �� �Լ�

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

        ////// ȭ�� ������ ���� ui�� ���������� ó���մϴ�.
        characterState.EquipItem(playerInfo);


        ////// ȭ�� ������ �κ��丮�� ĳ������ ã�Ƽ� ����ǥ�ð� �������� ó���մϴ�.
        charInventory.SetCombatEquipment(playerInfo, true);

        equipPanel.TakeOffEquipmentAll();
        equipPanel.EquipItemAll(playerInfo.equipItemArray);




        inventory.SetItemList(GameDB.GetItems((int)inventory.currBitTab), playerInfo.jobType);
        //inventory.SetItemList((GameDB.GetItems((int)ItemBitCategory.ALL), (int)playerInfo.job));

        // �˾�â�� ���� �ִٸ� ��Ȱ��ȭ�մϴ�.
        if (charPopup != null)
        {
            // ���߿��� ������ �ִϸ��̼� �߰��� ��.
            charPopup.gameObject.SetActive(false);
        }

        charPopup.Close();
    }


    // �������� �����ϰ� ���� ���� �������̶�� �ٷ� ���� ó���մϴ�.
    // ������ �˾�â�� �����ϱ� ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ��Դϴ�.
    public void OnClickEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        // ���õ� ������ ī�װ��� ĳ���Ͱ� �԰� �ִ��� Ȯ��
        if (GameDB.userInfo.CurrCharacter.WearingTheEquipment((int)itemInfo.category))
        {
            // �԰� �ִٸ� ������ ���¸� ���� ���·� �����ϰ� , �κ��丮���� ���� ǥ�ø� ����.
            inventory.SetCombatEquipment(GameDB.GetItem(GameDB.userInfo.CurrCharacter.equipItemArray[(int)itemInfo.category]), 0, false);

        }

        // ���õ� �������� ������ ���� ȭ�鿡 ǥ��
        equipPanel.EquipItem(itemInfo);


        //// ȭ�� ������ �κ��丮�� �������� ã�Ƽ� ����ǥ�ð� �������� ó���մϴ�.
        inventory.SetCombatEquipment(itemInfo, GameDB.userInfo.CurrCharacter.uniqueID, true);

        // ���� ĳ���Ͱ� �����ϰ� ������ �Է�
        GameDB.userInfo.CurrCharacter.SetWearing((int)itemInfo.category, itemInfo.uniqueID);

        //// �˾�â�� ���� �ִٸ� ��Ȱ��ȭ�մϴ�.
        if (itemPopup != null)
        {
            // ���߿��� ������ �ִϸ��̼� �߰��� ��.
            itemPopup.gameObject.SetActive(false);
        }

        itemPopup.Close();
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);
        characterState.EquipItem(GameDB.GetChar(GameDB.userInfo.CurrCharacter.uniqueID));

        //invettory.
    }

    // ������ �˾� â�� �����ϱ� ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ��Դϴ�. 
    public void OnClickOffEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        int category = (int)itemInfo.category;

        // �������� �����ϴ� �ڵ带 �ۼ�
        SaveCharacter Character = GameDB.userInfo.Get(itemInfo.equipCharacter);

        if (Character != null)
        {
            // ������ �����մϴ�.
            Character.SetWearing(category, 0);
            GameDB.SetCharInfoUpdate(Character.uniqueID);
        }

        // ���� ��� �����մϴ�.
        inventory.SetCombatEquipment(itemInfo, 0, false);

        //
        equipPanel.TakeOffEquipment(itemInfo.category);

        //// �˾�â�� ���� �ִٸ� ��Ȱ��ȭ�մϴ�.
        if (itemPopup != null)
        {
            // ���߿��� ������ �ִϸ��̼� �߰��� ��.
            itemPopup.gameObject.SetActive(false);
        }

        itemPopup.Close();
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);
        characterState.EquipItem(GameDB.GetChar(GameDB.userInfo.CurrCharacter.uniqueID));

    }

    // ----- ������ �˾� â�� ��ü�ϱ� ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ��Դϴ�. -----
    // ��ü�ϱ� �Լ��� �ٸ� ĳ���Ͱ� �����ϰ� �ִ� �����ۿ��� ������ Ŭ���Ͽ�����, 
    // �Ǵ� Ư�� ������ �ش��ϴ� �ٸ� ��� �����ϰ� �ְ�, ���� �ƹ��� �����ϰ� ���� ���� �������� ������ Ŭ�������� ȣ��Ǵ� �Լ��Դϴ�.
    public void OnClickChangeEquipItem(ItemInfo itemInfo)
    {
        AudioMng.Instance.PlayUI("UI_Equip");
        int category = (int)itemInfo.category;

        // �ش� �������� �����ϰ� �ִ� ĳ���͸� ã���ϴ�.
        SaveCharacter otherCharacter = GameDB.userInfo.Get(itemInfo.equipCharacter);

        // ���� �������� �����ϰ� �ִ� �ٸ� ĳ���Ͱ� �ִٸ� ó���մϴ�.
        if (otherCharacter != null)
        {
            // �ٸ� ĳ������ ������ �����մϴ�.
            otherCharacter.SetWearing(category, 0);
            GameDB.SetCharInfoUpdate(otherCharacter.uniqueID);
        }

        // ���� ĳ������ ����ũ ���̵�
        int currCharUniqueID = GameDB.userInfo.GetCharUniqueID;

        // ���� ĳ���Ͱ� �������̾��� ������ ���̵� ã���ϴ�.
        int itemUniqueID = GameDB.userInfo.GetIDOfItem(currCharUniqueID, category);

        // ���� ĳ���Ͱ� �������� ������ ������ ����ϴ�.
        ItemInfo prevItem = GameDB.GetItem(itemUniqueID);

        // ������ �������� �����մϴ�.
        GameDB.userInfo.CurrCharacter.SetWearing(category, itemInfo.uniqueID);
        GameDB.SetCharInfoUpdate(GameDB.userInfo.CurrCharacter.uniqueID);

        // ���� ��� �����մϴ�.
        inventory.SetCombatEquipment(prevItem, 0, false);

        // ������ ��� �����մϴ�.
        inventory.SetCombatEquipment(itemInfo, currCharUniqueID, true);

        // ������ ��� ȭ�� ������ ���� ui�� ���������� ó���մϴ�.
        equipPanel.EquipItem(itemInfo);

        if (itemPopup != null)
        {
            // ���߿��� ������ �ִϸ��̼� �߰��� ��.
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
            // && info.sprite != null ó���� �� �ϴ� �ɱ�?
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
       


        // ������ ��Ÿ�Կ� ĳ���Ͱ� ���� �� ��
        if ((character.jobType & info.wearType) == character.jobType)
        {
            
            // �������� �������� ���� ���� ��
            if (info.equip)
            {
                
                // ĳ���Ͱ� �ش� ������ ī�װ��� ���� ���� ��
                if (character.WearingTheEquipment(catecory))
                {
                    // �������� �������ΰ� ĳ������ ��
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
        // ĳ���Ͱ� ��� ���ϴ� �������̸�
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
