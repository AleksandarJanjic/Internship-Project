using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    public bool isCollectable = true;

    public float waitSeconds = .1f;

    public Transform firePoint;
    public Transform firePointAbove;
    public float checkRadius;
    Color raycastColor = new Color(1, 1f, 1, 1f);

    public GameObject bombExplosionObj;

    [SerializeField]
    private ParticleSystem explosion;

    public void ActivateBomb()
    {
        StartCoroutine(BombDroped());
    }

    public IEnumerator BombDroped()
    {
        isCollectable = false;
        yield return new WaitForSeconds(3f);
        bombExplosionObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        bombExplosionObj.SetActive(false);
        BombExplodes();
    }

    private void BombExplosion_Lines()  //Is not used
    {

        RaycastHit2D raycastInfo = Physics2D.Raycast(firePoint.position, Vector2.left, checkRadius);
        Debug.DrawRay(firePoint.position, Vector2.left, raycastColor ,checkRadius);
        DestroyHittedObjects(raycastInfo);

        raycastInfo = Physics2D.Raycast(firePoint.position, Vector2.right, checkRadius);
        Debug.DrawRay(firePoint.position, Vector2.right, raycastColor, checkRadius);
        DestroyHittedObjects(raycastInfo);

        raycastInfo = Physics2D.Raycast(firePoint.position, Vector2.up, checkRadius);
        Debug.DrawRay(firePoint.position, Vector2.up, raycastColor, checkRadius);
        DestroyHittedObjects(raycastInfo);

        raycastInfo = Physics2D.Raycast(firePoint.position, Vector2.down, checkRadius);
        Debug.DrawRay(firePoint.position, Vector2.down, raycastColor, checkRadius);
        DestroyHittedObjects(raycastInfo);

    }

    void DestroyHittedObjects(RaycastHit2D raycastHit) //Is not used
    {
        if(raycastHit.transform == transform)
        {
            return;
        }
        if (raycastHit != false)
        {
            Debug.Log(raycastHit.collider.gameObject.tag);
            Destroy(raycastHit.collider.gameObject);
        }
    }

    void BombExplodes()
    {
        explosion.Play();
        Destroy(gameObject);
    }
}
