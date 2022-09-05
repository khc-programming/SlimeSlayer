using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
public class PlayerInfo : SaveCharacter
{
    /*
    
    - shareInfo -
    public int spawnID;
    public int hp;
    public int currHp; 
      
    - saveInfo -
    public int uniqueID;
    public int tableID;
    public int grade;
    public int level;
    public bool equip;
    public int equipCharacter;

    - characterSaveInfo -
    public int jobType;
    public int[] equipItemArray 

     */

    private bool isUpdate = false;
    public bool IsUpdate
    {
        set { isUpdate = value; }
        get { return isUpdate; }
    }

    public Job job;
    public JobBit jobBit;
    public int iconCount;
    public Sprite sprite;

    public int basicAttack;
    public int attack;
    public int basicDefence;
    public int defence;
    public int basicHp;
    public float speed;
    public float currSpeed;

    public ItemInfo weaponObj;
    public ItemInfo shieldObj;
    public ItemInfo petObj;

    public string explain;
    public string name;

    public int itemAttack;
    public int itemDefence;
    public int itemHP;

    

    public string model;
    public int weaponID = 4;

    public int price;

    

}


[RequireComponent(typeof(AIPath2D))]
public class Player : Unit
{
    //public List<Weapon> weapons;
    //public Weapon weapon;
    public AIPath2D path;

    // ��ã�� ���� ���
    private float elapseed = 0;
    private float targetTime = 1;

    private bool damageState = false;
    private float damageElap = 0;
    private float damageTime = 1;
    private bool isDead = false;
    UIHp uiHp;
    int pathFalseCount = 0;

    public List<Weapon> weapons;
    public Weapon weapon;

    //public SpriteRenderer characterSprite;
    




    [SerializeField]
    protected PlayerInfo playerInfo = new PlayerInfo();


    public override void setWeapon()
    {
        if (weapons != null)
        {
            foreach (var value in weapons)
            {
                value.Init();
                value.SetInfo(playerInfo.equipItemArray[0], playerInfo.job);
            }
        }
    }

    public override void setInfo(int spawnID, int tableID, int LV)
    {
        
        playerInfo.spawnID = spawnID;
        playerInfo.tableID = tableID;
        playerInfo.model = DataManager.ToS(TableType.PLAYERTABLE, tableID, "MODEL");


       // GameDB.SetCharInfoUpdate(playerInfo.uniqueID);
    }


    

    public override void setUpdate()
    {
       
    }

    public override void Init()
    {

        base.Init();
        unitType = UnitType.Player;
        targetType = TargetType.On;

        path = GetComponent<AIPath2D>();
        if (path != null) path.Init();

        //characterSprite = UtilHelper.Find<SpriteRenderer>(transform, "root");


        weapons.AddRange(GetComponentsInChildren<Weapon>(true));
        if (weapons.Count >= 1)
        {
            foreach (var value in weapons)
            {
                value.Init();
                if (value.spriteColor != null)
                    weapon = value;
            }
        }

        //playerInfo = GameDB.GetChar(GameDB.userInfo.charUniqueID);
        //GameDB.SetCharInfoUpdate(playerInfo.uniqueID);
        
        gameObject.layer = (int)unitType;
        gameObject.tag = unitType.ToString();

    }

    public PlayerInfo SetPlayerInfo
    {
        set {
            int tempSpanwID = playerInfo.spawnID;
            
            playerInfo = value;
            playerInfo.spawnID = tempSpanwID;

            
            setWeapon();
            }
    }

    public PlayerInfo GetPlayerInfo
    {
        get { return playerInfo; }
        
    }

//    public void SetPlayer(PlayerInfo info)
//    {
//        //playerInfo.isUpdate = true;
//        playerInfo.job = info.job;
//        playerInfo.jobBit = info.jobBit;
//        playerInfo.iconCount = info.iconCount;
//        playerInfo.sprite = info.sprite;

//        playerInfo.maxAttack = info.maxAttack;
//        playerInfo.attack = info.attack;
//        playerInfo.maxDefence = info.maxDefence;
//        playerInfo.defence = info.defence;
//        playerInfo.maxHp = info.maxHp;
//        playerInfo.speed = info.speed;
//        playerInfo.currSpeed = info.currSpeed;

//        playerInfo.weaponObj = info.weaponObj;
//        playerInfo.shieldObj = info.shieldObj;
//        playerInfo.petObj = info.petObj;

//        playerInfo.explain = info.explain;
//        playerInfo.name = info.name;

//        playerInfo.itemAttack = info.itemAttack;
//        playerInfo.itemDefence = info.itemDefence;
//        playerInfo.itemHP = info.itemHP;

//        playerInfo.lastState[0] = info.lastState[0];
//        playerInfo.lastState[1] = info.lastState[1];
//        playerInfo.lastState[2] = info.lastState[2];

//}


    public void changeHpBar()
    {

        if (uiHp == null)
            return;
     
        Destroy(uiHp.gameObject);

        uiHp = UIMng.Instance.UIHpCreate(HpPivot, playerInfo);
       
        uiHp.SetColor(Color.green);
    }


public override void SetDamage(int attack)
    {
        damageState = true;
        damageElap = 0;
        int damage = playerInfo.lastState[1] - attack;


        if (uiHp == null)
        {
            uiHp = UIMng.Instance.UIHpCreate(HpPivot, playerInfo);
            uiHp.SetColor(Color.green);
        }

        if (damage < 0)
        {
            playerInfo.currHp += damage;

            

        }


        if (playerInfo.currHp <= 0)
        {
            AudioMng.Instance.Play2DEffect("PlayerDie");
            isDead = true;
            getModel.getSpriteColor.Execute(Color.black,
                                new Color(0, 0, 0, 0),
                                ColorMode.One,
                                1.0f,
                                true);
            Destroy(gameObject, 1.0f);
            isDie = true;


            if (getModel.getRigid2D != null)
                getModel.getRigid2D.isKinematic = true;

            if (getModel.getColl2D != null)
                getModel.getColl2D.enabled = false;
            // NewMonster������Ʈ�� ������� �ʵ��� ó���մϴ�.
            enabled = false;
        }
        else
        {
            AudioMng.Instance.Play2DEffect("PlayerHit");
            getModel.getSpriteColor.Execute(Color.black,
                    Color.white,
                    ColorMode.One,
                    0.2f);

        }
    }


    



    public override float GetHP()
    {
        float hp = (float)playerInfo.currHp / playerInfo.hp;

        hp = Mathf.Floor(hp * 100) / 100f;

        return hp;
    }





    #region _�߻��Լ����_
    public override void Idle()
    {

    }
    public override void Move()
    {

    }

    public override void Move(Vector2 dir)
    {
        // if (getModel.IsTag("Attack"))
        //  return;

        Vector3 temp = dir.normalized * 0.5f;
        temp += getModel.getTarget.getCenter.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        

        if (Physics2D.OverlapBox(temp, new Vector2(0.1f, 0.6f), angle, 1 << LayerMask.NameToLayer("Monster")))
        //if (Physics2D.CircleCast(temp, 0.2f,
        //           dir, 0.001f, 1 << LayerMask.NameToLayer("Monster")))
        {
            
            Vector2 v = dir * playerInfo.currSpeed;
            getModel.Move(v, true);
            charDir(dir);
        }
        else
        {
            Vector2 v = dir * playerInfo.currSpeed;
            getModel.Move(v);
            charDir(dir);
        }



    }

    public override void Attack()
    {

    }

    public override void Attack(Vector2 dir)
    {

    }

    public override void Patrol()
    {

    }

    public override void Chase()
    {

        if (GameDB.targetUnit == null)
            return;

        AIPath2D targetAI = GameDB.targetUnit.GetComponent<AIPath2D>();
        if (targetAI == null)
            return;


        if (path.path.Count == 0)
        {
            getModel.getAnimator.SetBool("Move", false);

            //print(gameObject.name);

            AIPathMng.Instance.Find(path, targetAI);

        }
        else
        {

            List<AINode2D> tempList = new List<AINode2D>();
            tempList.AddRange(path.path);
            tempList.Reverse();


            if (path.path[0].pathCheck(this, GameDB.targetUnit) || tempList[0].OnCheckUnit(GameDB.targetUnit) == false)
            {



                if (getModel.getTarget.target != null)
                {
                    charDir(getModel.getTarget.getTargetPosDir);
                }

                pathFalseCount++;
                if (pathFalseCount == 10)
                {
                    List<AINode2D> tempPath = new List<AINode2D>();
                    //tempPath.Add(path.path[0]);
                    tempPath = AIPathMng.Instance.tempWallCreate(this, GameDB.targetUnit);
                    path.path.Clear();
                    AIPathMng.Instance.Find(path, targetAI, tempPath);
                    pathFalseCount = 0;

                }
                else
                {
                    List<AINode2D> tempPath = new List<AINode2D>();
                    tempPath.Add(path.path[0]);
                    path.path.Clear();
                    AIPathMng.Instance.Find(path, targetAI, tempPath);

                }

            }
            else
            {


                Vector3 dir = path.path[0].transform.position - transform.position;
                dir.Normalize();
                charDir(dir);

                getModel.getAnimator.SetBool("Move", true);


                Vector3 v = Vector3.zero;
                v.y = -0.45f;
                v += path.path[0].transform.position;

                
                
                transform.position = Vector3.MoveTowards(transform.position,
                                                                                       v,
                                                                                       playerInfo.currSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, v) < 0.01f)
                    path.path.RemoveAt(0);



            }
        }
        //else
        //{

        //    Vector3 dir = path.path[0].transform.position - transform.position;
        //    dir.Normalize();
        //    charDir(dir);

        //    getModel.getAnimator.SetBool("Move", true);
        //    transform.position = Vector3.MoveTowards(transform.position,
        //                                                                            path.path[0].transform.position,
        //                                                                            playerInfo.currSpeed * Time.deltaTime);

        //    if (Vector3.Distance(transform.position, path.path[0].transform.position) < 0.01f)
        //        path.path.RemoveAt(0);
        //}
    }

    public override void Skill1()
    {

    }

    public override void Skill2()
    {

    }

    #endregion ��� �Լ� ���




}