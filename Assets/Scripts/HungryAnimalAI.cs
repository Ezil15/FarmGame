using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimalAI))]
public class HungryAnimalAI : MonoBehaviour
{

    public int MaxSatiety = 100;
    [SerializeField]
    private int lowSatiety = 80;
    private int satiety;
    public int Satiety {
        get { return satiety; }
        private set { satiety = value; }
    }

    [SerializeField]
    private float minHungerTick, maxHungerTick;
    private float hungerTickCooldown;

    [HideInInspector]
    public AnimalAI AnimalAI;
    void Start()
    {
        AnimalAI = GetComponent<AnimalAI>();
        AnimalAI.InterestedInTarget = false;
        satiety = MaxSatiety;
        hungerTickCooldown = maxHungerTick;
    }

    void FixedUpdate()
    {
        if (AnimalAI.DisableAI)
            return;
        
        hungerTickCooldown -= Time.deltaTime;
        if (hungerTickCooldown <= 0f)
        {
            hungerTickCooldown = Random.Range(minHungerTick, maxHungerTick);
            AddHunger();
        }
    }

    /// <summary>
    /// Заставляет существо проголодаться на hunger единиц. Может привести к смерти.
    /// </summary>
    public void AddHunger(int hunger = 1)
    {
        Satiety -= hunger;

        if (Satiety <= 0)
        {
            //  Смерть
            AnimalAI.DisableAI = true;
            Destroy(gameObject);
            return;
        }

        if (!AnimalAI.InterestedInTarget && IsHungry())
            AnimalAI.InterestedInTarget = true;
    }

    /// <summary>
    /// Покормить существо на satiety единиц.
    /// </summary>
    public void AddSatiety(int satiety = 1)
    {
        Satiety += satiety;

        if (Satiety > MaxSatiety)
            Satiety = MaxSatiety;

        if (AnimalAI.InterestedInTarget && !IsHungry())
            AnimalAI.InterestedInTarget = false;
    }

    /// <summary>
    /// true если животное достаточно голодно чтобы начать искать еду.
    /// </summary>
    public bool IsHungry()
    {
        return satiety <= lowSatiety;
    }

    public void OnFindEat(GameObject eat)
    {
        int satiety = eat.GetComponent<EatableObject>().Eat();
        AddSatiety(satiety);
    }
}
