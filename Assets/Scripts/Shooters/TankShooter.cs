using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public GameObject firepointTransform;

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.transform.position, firepointTransform.transform.rotation);

        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();

        if (doh != null)
        {
            doh.damageDone = damageDone;

            doh.owner = GetComponent<Pawn>();
        }

        Rigidbody rb = newShell.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(firepointTransform.transform.forward * fireForce);
        }

        Destroy(newShell, lifespan);
    }
}
