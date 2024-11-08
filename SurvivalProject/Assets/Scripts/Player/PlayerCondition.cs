using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Idamagalbe
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, Idamagalbe
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if(health.curValue <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    private void Die()
    {
        Debug.Log("죽어");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke(); //등록된 기능 호출
    }

    public bool UseStamina(float amount)
    {
        if(stamina.curValue - amount<0f)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }
}
