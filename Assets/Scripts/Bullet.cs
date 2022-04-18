using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Bullet(float bspeed, float bdistance, float lifetime, int dmg) : base(bspeed, bdistance, lifetime, dmg) { }
    public float bspeed = 30f;
    public float bdistance = 10f;
    protected float lifetime = 2f;
    protected int dmg = 1;
    public LayerMask whatisSolid;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.forward, bdistance, whatisSolid);
        if (hitinfo.collider != null)
        {
            if(hitinfo.collider.CompareTag("Enemy"))
            {
                hitinfo.collider.GetComponent<Enemy>().GetDamage(dmg);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * bspeed * Time.deltaTime);
        StartCoroutine(LifeTimeDestruction());

    }
    private IEnumerator LifeTimeDestruction()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
