using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    List<Idamagalbe> things = new List<Idamagalbe>();
    void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    private void DealDamage()
    {
        for(int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Idamagalbe damagalbe))
        {
            things.Add(damagalbe);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Idamagalbe damagalbe))
        {
            things.Remove(damagalbe);
        }
    }
}
