using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public int health = 100;
    public int max_health = 100;
    public bool is_defeated = false;

    public void die()
    {
         is_defeated = true;
        
    }

    public void CheckHealth()
    { 
        if(health <=0)
        {
            health = 0;
            die();
        }
    }

    public void SetHP(int current_HP)
    {
       health = current_HP;
       CheckHealth();
    }

    public void Heal(int heal)
    {
        int HP_after_Heal = health + heal;
        SetHP(HP_after_Heal);
    }

}
