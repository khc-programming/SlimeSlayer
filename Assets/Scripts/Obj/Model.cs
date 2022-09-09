using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Model : MonoBehaviour
{
    private Animator animator;
    
    private SpriteColor spriteColor;
    private Rigidbody2D rigid2D;
    private Collider2D coll2D;
  
    private Target target;



    public Animator getAnimator
    {
        get
        {
            return animator;
        }
    }

    

    public SpriteColor getSpriteColor
    {
        get
        {
            return spriteColor;
        }
    }

    public Rigidbody2D getRigid2D
    {
        get
        {
            return rigid2D;
        }
    }

    public Target getTarget
    {
        get
        {
            return target;
        }
    }

    public Collider2D getColl2D
    {
        get
        {
            return coll2D;
        }
    }



    public void Init()
    {
        animator = GetComponentInChildren<Animator>();
       
        rigid2D = GetComponent<Rigidbody2D>();
        spriteColor = GetComponent<SpriteColor>();
        if (spriteColor != null) spriteColor.Init();
        target = GetComponent<Target>();
        if (target != null) target.Init();
        coll2D = GetComponent<Collider2D>();

        
    }

    public void SetTrigger(string trigger)
    {

        animator.SetTrigger(trigger);
    }

    public void Move(Vector2 velocity , bool colliderChek = false)
    {
        if (velocity == Vector2.zero)
            animator.SetBool("Move", false);
        // 좌, 우키를 입력하고 있지 않다면 애니메이션이
        // 출력되지 않도록 처리합니다.
        else
        {
            if(IsTag("Move") == false)
                animator.SetTrigger("AttackCancel");
            animator.SetBool("Move", true);
        }

        if(colliderChek == false)
        {
            
            transform.Translate(velocity.normalized * velocity.magnitude * Time.deltaTime);
            
            //Vector3 v = velocity.normalized;
            //v += transform.position;
            //transform.position = Vector3.MoveTowards(transform.position, v,
            //                                                 velocity.magnitude * Time.deltaTime);
        }
        else
        {
            rigid2D.velocity = Vector2.zero;
        }

        
        

    }

    public void Attack()
    {
        
            animator.SetTrigger("Attack");   
    }

    public void Attack(Vector2 velocity)
    {
       
    }

    public bool IsTag(string tag, int layer = 0)
    {
        // 지정한 레이어에서 실행되고 있는 애니메이션의
        // 정보를 얻습니다.
        AnimatorStateInfo stateInfo =
            animator.GetCurrentAnimatorStateInfo(layer);

        return stateInfo.IsTag(tag);
    }
    // SetFloat, SetInteger, SetBool 함수등을 사용해서
    // 값을 변경처리하면 현재 애니메이션에서 사용자가 
    // 요청한 애니메이션으로 변경하는 과정( 업데이터 과정)이 
    // 되게 됩니다. 변경하려고 업데이트를 하는 과정이라면
    // true값을 리턴하게 됩니다.
    public bool IsInTransition(int layer = 0)
    {
        return animator.IsInTransition(layer);
    }



}
