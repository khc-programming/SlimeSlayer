using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 8f;

    public Transform setTarget
    {
        set { target = value; }
    }

    public float setSpeed
    {
        set { speed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        int monster = LayerMask.NameToLayer("Monster");
        int player = LayerMask.NameToLayer("Player");
        int playerSkill = LayerMask.NameToLayer("PlayerSkill");
        int monsterSkill = LayerMask.NameToLayer("MonsterSkill");
        int outWall = LayerMask.NameToLayer("OutWall");
        int inWall = LayerMask.NameToLayer("InWall");


        Physics2D.IgnoreLayerCollision(player, player);
        Physics2D.IgnoreLayerCollision(player, monster);
        Physics2D.IgnoreLayerCollision(monster, monster);

        Physics2D.IgnoreLayerCollision(playerSkill, player);
        Physics2D.IgnoreLayerCollision(playerSkill, playerSkill);
        Physics2D.IgnoreLayerCollision(playerSkill, monsterSkill);


        Physics2D.IgnoreLayerCollision(monsterSkill, monster);
        Physics2D.IgnoreLayerCollision(monsterSkill, monsterSkill);

        Physics2D.IgnoreLayerCollision(outWall, inWall);




        //Physics2D.IgnoreLayerCollision(monsterLayer, monsterLayer);
        //Physics2D.IgnoreLayerCollision(player, monsterLayer);
        //Physics2D.IgnoreLayerCollision(monsterLayer, monsterLayer);
        // 횡스크롤 방식이기 때문에 
        // 몬스터와 몬스터는 충돌되지 않도록 처리합니다.
        // Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LateRun()
    {
        if (target == null)
            return;

        Vector3 tempPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime * speed);
        

        
        
    }

   

}
