using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDark03 : Skill
{




    public void OnColl2D()
    {
        getModel.getColl2D.enabled = true;
    }

    public void OffColl2D()
    {
        getModel.getColl2D.enabled = false;
    }

    public override void Shoot()
    {
        //getModel.getSpriteColor.SetColor(getSkillInfo.color);



        //getModel.getSpriteColor.Execute(getSkillInfo.color,
        //                            new Color(getSkillInfo.color.r * 0.8f, getSkillInfo.color.g * 0.8f, getSkillInfo.color.b * 0.8f, 0.8f),
        //                            ColorMode.Pingpong,
        //                            0.1f);

        getModel.getRigid2D.velocity = Vector2.zero;
        getSkillInfo.saveVelocity = Vector2.zero;

        //Destroy(gameObject, 5f);
    }

    private void SkillAttack()
    {
        
        Collider2D[] enemys = Physics2D.OverlapBoxAll(attackPivo.transform.position,
                            attackPivo.size,
                            0,
                            1 << LayerMask.NameToLayer("Monster"));

        foreach(var value in enemys)
        {
            value.SendMessage("SetDamage", skillInfo.attack, SendMessageOptions.DontRequireReceiver);
        }

        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.tag == "Monster" || collision.tag == "Player" || collision.tag == "OutWall")
    //    {
    //        collision.SendMessage("SetDamage", skillInfo.attack, SendMessageOptions.DontRequireReceiver);
    //        //print(collision.name);
    //        //getModel.getColl2D.enabled = false;
    //        //getModel.getRigid2D.velocity = Vector2.zero;
    //        //getModel.getAnimator.SetTrigger("Finish");

    //    }


    //}




}

