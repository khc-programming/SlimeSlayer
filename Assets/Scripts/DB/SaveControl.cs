using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Load(TableType.ITEMTABLE);
        
        //GameDB.Load("PlayerInfo.json");

        //GameDB.CreateRandomItem();
        //GameDB.Save("PlayerInfo.json");

        //GameDB.Load("PlayerInfo.json");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameDB.Load("PlayerInfo.json");
            UIInventory i = GameObject.FindObjectOfType<UIInventory>();
            i.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameDB.Save("PlayerInfo.json");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //GameDB.Save("PlayerInfo.json");
            GameDB.randomCreate(50);
            UIInventory i = GameObject.FindObjectOfType<UIInventory>();
            i.SetItemList(GameDB.GetItems((int)ItemBitCategory.ALL), (int)GameDB.userInfo.jobType);
            //SetItemList(GameDB.GetItems());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameDB.userInfo.listOfChar.Add(new SaveCharacter() { equip = true, grade = 3, level = 60, tableID = 1, uniqueID = 100, jobType = (int)JobBit.WARRIOR });
            GameDB.userInfo.listOfChar.Add(new SaveCharacter() { equip = false, grade = 1, level = 30, tableID = 2004, uniqueID = 101, jobType = (int)JobBit.WIZARD });
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            UIInventory i = GameObject.FindObjectOfType<UIInventory>();
            i.invenItemList[5].itemInfo.equip = true;
            i.invenItemList[5].itemInfo.equipCharacter = 101;
            i.invenItemList[5].itemInfo.wearType = (int)ItemBitCategory.ALL;
            i.invenItemList[5].SetInfo(i.invenItemList[5].itemInfo);
        }




    }
}
