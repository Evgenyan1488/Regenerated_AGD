using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimController : MonoBehaviour
{
    protected static Animator anim;
    public enum States
    {
        idle,
        run_ruki,
        punch,
        pistol_shot,
        pee,
        smoke
    }
    public bool legsrun;
    private void Awake()
    {
    }
    public States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
}



public class Entity : AnimController
{
    protected int lives;

    public void GetDamage(int damage)
    {
        lives-=damage;
        if (lives < 1)
            Die();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
