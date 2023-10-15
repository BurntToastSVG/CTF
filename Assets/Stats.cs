using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public int Health;
    public int MaxHealth;
    public bool isDead;
    public int AttackSpeed;
    public int Damage;
    public string SpecialAttack;
    public int SpecialAttackDamage;
    public int Cooldown; 
    public bool canAttack;





    void Start()
    {
        Health = MaxHealth; 
    }


    void Update()
    {
      



    }

       

    void DealDamage()
    {

    }



    void TakeDamage(int amount)
    {
        Health -= amount;
    }

    void CheckHealth()
    {
        print(Health);
    }

    void Heal(int amount)
    {
        Health = Health + amount;
    }


    void die()
    {
        isDead = true;
    }

    public virtual void Die()
    {
        isDead = true;
    //        Destroy(gameObject);
        
    }

    //private void OnCollisionEnter(Collision c )
    //{
    //    print(c);
    //}







}



