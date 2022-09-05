using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveInfo : ShareInfo
{
    // 같은 아이템이 있을 수 있기 때문에 각 아이템을 구별하기 위한 유니크 아이디
    public int uniqueID;

    // 아이템의 테이블에서의 아이디
    public int tableID;

    // 현재의 성급 ( 테이블 값은 태생 성급, 이 데이터는 현재의 성급을 가리킵니다. )
    public int grade;

    // 현재의 레벨 ( 생성된 이후의 레벨 수치를 가리킵니다. )
    public int level;

    // 아이템 착용 여부( 착용중인 상태를 가리키는 변수 )
    public bool equip;

    // 아이템을 착용하고 있는 캐릭터 아이디
    public int equipCharacter;

    public bool checkbox = false;

    // 아이템의 랭크 ( 간혹 강화에 따라 아이템 또는 캐릭터의 랭크가 변경되는 경우도 있기 때문에 추가했습니다. )
    // normal, rare, special rare, super special rare
    //public int rank;

    //// 테스트하기 위해 추가한 데이터 ( 등급에 따른 코스트 비용 )
    //public int cost;
}

[System.Serializable]
public class SaveSkill
{
    public int level = 0;
    //public int numver = 0;
    public int tableID = 0;

   
    
}

[System.Serializable]
public class SaveCharacter : SaveInfo
{
    public int jobType;
    public int[] equipItemArray = new int[3] {0, 0, 0};
    public List<SaveSkill> skillList = new List<SaveSkill>();
    public int skillPoint = 0;
    public bool isSkill1 = true;
    public bool isSkill2 = true;
    public bool isSkill3 = true;

    // 해당 카테고리에 아이템을 착용시키는 함수입니다.
    public void SetWearing(int category, int itemUniqueID)
    {
        equipItemArray[category] = itemUniqueID;
    }

    // 카테고리에 맞는 아이템을 착용중인지 체크하는 함수입니다.
    public bool WearingTheEquipment(int category)
    {
        return equipItemArray[category] != 0;
    }
}