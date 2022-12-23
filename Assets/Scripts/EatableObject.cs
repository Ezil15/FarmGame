using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableObject : MonoBehaviour
{
    [SerializeField]
    private int charges = 1;
    [SerializeField]
    private int satietyPerCharge = 1;

    /// <summary>
    /// Съесть объект, возвращает кол-во сытости которое он восстанавливает
    /// </summary>
    public int Eat()
    {
        charges -= 1;
        if (charges <= 0)
        {
            Destroy(gameObject);
        }
        return satietyPerCharge;
    }
}
