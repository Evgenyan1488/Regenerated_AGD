using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fight : AnimController
{
    public GameObject bullet;
    public Transform shotPoint;
    public Transform mytransform;
    public LayerMask enemy;
    public float attackrange;

}




public abstract class Weapon : AnimController
{

    protected int dmg = 1;

    public bool isrecharged;
    public bool isattacking;


    public float attackspeed = 0.4f;
    public float curreloadtime;
    public float animationTime;


    public Transform shotPoint;



    public virtual void AttackStart()
    {

    }
    public virtual void AttackEnd()
    {

    }

}


public class Gun : Weapon
{
    //protected float bspeed = 5f;
    //public float bdistance = 5f;
    //protected float lifetime = 0.4f;

    //public LayerMask whatisSolid;
    public Transform mytransform;
    public GameObject bullet;


    public Gun(float atkspeed, GameObject bullet, Transform shotpoint, Transform transform)
    {
        attackspeed = atkspeed;
        this.bullet = bullet;
        this.shotPoint = shotpoint;
        this.mytransform = transform;
    }


    public override void AttackStart()
    {
        isattacking = true;
        State = States.pistol_shot;
        curreloadtime = attackspeed;
    }
    public override void AttackEnd()
    {
        Instantiate(bullet, shotPoint.position, mytransform.rotation);
    }
}






public class Club : Weapon
{
    private float attackrange;
    private LayerMask enemy;
    private Transform mytransform;

    public Club(int dmg, float atkspeed, float attackrange, LayerMask enemy, Transform mytransform)
    {
        attackspeed = atkspeed;
        this.dmg = dmg;
        this.attackrange = attackrange;
        this.enemy = enemy;
        this.mytransform = mytransform;
    }

    void Start()
    {

    }


    void Update()
    {

    }

    public override void AttackStart()
    {
        isattacking = true;
        State = States.punch;
        curreloadtime = attackspeed;
        Debug.Log("atakuyu");
    }
    public override void AttackEnd()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mytransform.position, attackrange, enemy);
        Debug.Log("tichka");
        for (int i = 0; i < colliders.Length; i++)
            colliders[i].GetComponent<Enemy>().GetDamage(dmg);
    }
}