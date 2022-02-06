using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{

    [SerializeField] private float  attackCooldown;
    [SerializeField] private Transform  firePoint;
    [SerializeField] private GameObject[]  slimeballs;
    private Animator anim;
    private  Movment PMovment;
    private  float cooldownTimer = Mathf.Infinity;
    
    private void Awake(){
        anim = GetComponent<Animator>();
        PMovment = GetComponent<Movment>();
    }

    private void Update(){
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && PMovment.canAttack())
        Attack();
        cooldownTimer += Time.deltaTime;

    }

    private void Attack(){
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        slimeballs[FindSlimeball()].transform.position = firePoint.position;
        slimeballs[FindSlimeball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindSlimeball()
    {
        for(int i = 0; i < slimeballs.Length; i++)
        {
            if(!slimeballs[i].activeInHierarchy)
            return i;
        }
        return 0;
    }
}

    


