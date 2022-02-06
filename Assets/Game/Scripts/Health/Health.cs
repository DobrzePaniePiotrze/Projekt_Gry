using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
   [SerializeField] private float startingHealth;

   public float currentHealth { get; private set;}
   private Animator anim;
   private bool dead;
   [Header ("iFrames")]
   [SerializeField]private float iFramesDuration;
   [SerializeField]private int numberOflashes;
   private SpriteRenderer spriteRend;
   [SerializeField] private Behaviour[] components;

private void Start()
{
    currentHealth = startingHealth;
    anim = GetComponent<Animator>();
    spriteRend = GetComponent<SpriteRenderer>();

}
public void TakeDamage(float _damage)
{
    currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

    if (currentHealth > 0)
    {
        anim.SetTrigger("hurt");
        StartCoroutine(Invunerability());
    }else
    {
        if(!dead)
        {
            anim.SetTrigger("die");
        //   if(GetComponent<Movment>() !=null)
        //     GetComponent<Movment>().enabled = false;

        //   if(GetComponentInParent<EnemyPatrol>() !=null)
        //      GetComponentInParent<EnemyPatrol>().enabled = false;
        //      if(GetComponent<MeleeEnemy>() !=null)
        //     GetComponent<MeleeEnemy>().enabled = false;

            foreach(Behaviour component in components)
            {
                component.enabled = false;
            }
        dead = true;
        }
    }
       
    }

public void AddHealth(float _value)
{
    currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);

}
private IEnumerator Invunerability()
{
    Physics2D.IgnoreLayerCollision(6,14, true);
    for(int i = 0; 1<numberOflashes;i++)
    {
        spriteRend.color = new Color(1,0,0,0.5f);
        yield return new WaitForSeconds(iFramesDuration/(numberOflashes * 2));
        spriteRend.color = Color.white;
        yield return new WaitForSeconds(iFramesDuration/(numberOflashes * 2));

    }
    Physics2D.IgnoreLayerCollision(6,14, false);
}
private void Update()
{

    Debug.Log(currentHealth);

    if(Input.GetKeyDown(KeyCode.E)){
        TakeDamage(1);
    }
}

private void Deactivate()
{
    gameObject.SetActive(false);
}
}

