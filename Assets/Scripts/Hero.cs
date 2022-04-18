using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : Entity
{
    //public float offset = -90f;

    Rigidbody2D body;
    public bool debug;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed = 5.0f;

    public GameObject bullet;
    public Transform shotPoint;
    public Transform punchpoint;
    public Transform mytransform;
    public LayerMask enemy;
    public float attackrange;
    Weapon weapon;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchpoint.position, attackrange);
    }

    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        weapon = new Gun(0.4f, bullet, shotPoint, mytransform);
    }

    void Start()
    {
    }

    void Update()
    {
        Vector3 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            legsrun = true;
            if (!weapon.isattacking)
                State = States.run_ruki;
        }
        else
        {
            legsrun = false;
            if (!weapon.isattacking)
                State = States.idle;
        }

        anim.SetBool("LegsRun", (bool)legsrun);
        

        if (weapon.curreloadtime > 0)
            weapon.curreloadtime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (weapon.curreloadtime <= 0)
            {
                weapon.AttackStart();
                StartCoroutine(WaitForAtc(weapon.attackspeed - 0.005f));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = new Club(1, 0.5f, attackrange, enemy, punchpoint);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = new Gun(0.4f, bullet, shotPoint, mytransform);
        }
    }

   

    public IEnumerator WaitForAtc(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("suka");
        weapon.isattacking = false;
    }



    public void HeroAttack()
    {
        weapon.AttackEnd();
    }



    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
