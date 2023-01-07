using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAI : MonoBehaviour
{
    [HideInInspector]
    public LootableAnimal LootableAnimal;
    void Start()
    {
        LootableAnimal = GetComponent<LootableAnimal>();
    }

    public void MakeEgg()
    {
        LootableAnimal.Loot();
    }
}
