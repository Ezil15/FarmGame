using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimalAI))]
public class LootableAnimal : MonoBehaviour
{
    private bool canLoot = false;
    /// <summary>
    /// Готовность животного к отдаванию лута
    /// </summary>
    public bool CanLoot {
        get { return canLoot; }
        private set {
            canLoot = value;
            if (canLoot)
                OnCanLoot?.Invoke();
        }
    }

    public UnityEvent OnCanLoot;

    [SerializeField]
    private float lootingDelay = 10f;
    private float currentLootingDelay = 0f;

    [SerializeField]
    private GameObject lootToDrop;

    [SerializeField]
    private Transform lootSpawnPoint;
    [SerializeField]
    private float lootSpawnForce = 1f;

    [HideInInspector]
    public AnimalAI AnimalAI;
    void Start()
    {
        AnimalAI = GetComponent<AnimalAI>();
    }

    void FixedUpdate()
    {
        if (AnimalAI.DisableAI || CanLoot)
            return;

        currentLootingDelay += Time.deltaTime;
        if (currentLootingDelay >= lootingDelay)
        {
            CanLoot = true;
            currentLootingDelay = 0f;
        }
    }

    /// <summary>
    /// Выкидывание лута
    /// </summary>
    /// <returns>Созданный лут или null</returns>
    public GameObject Loot()
    {
        if (!CanLoot)
            return null;
        CanLoot = false;

        GameObject spawnedLoot = Instantiate(lootToDrop, lootSpawnPoint.position, lootSpawnPoint.rotation, null);
        Rigidbody lootRBody = spawnedLoot.GetComponent<Rigidbody>();
        lootRBody.AddForce(lootSpawnPoint.forward * lootSpawnForce, ForceMode.Impulse);

        return spawnedLoot;
    }
}