using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWind01 : Skill
{

    public override void Shoot()
    {
        //getModel.getSpriteColor.SetColor(getSkillInfo.color);



        //getModel.getSpriteColor.Execute(getSkillInfo.color,
        //                            new Color(getSkillInfo.color.r * 0.8f, getSkillInfo.color.g * 0.8f, getSkillInfo.color.b * 0.8f, 0.8f),
        //                            ColorMode.Pingpong,
        //                            0.1f);

        getModel.getRigid2D.velocity = transform.right * getSkillInfo.speed;
        getSkillInfo.saveVelocity = getModel.getRigid2D.velocity;

        //Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Monster" || collision.tag == "OutWall")
        {
            collision.SendMessage("SetDamage", skillInfo.attack, SendMessageOptions.DontRequireReceiver);

            getModel.getColl2D.enabled = false;
            getModel.getRigid2D.velocity = Vector2.zero;
            getModel.getAnimator.SetTrigger("Finish");

        }


    }


}

