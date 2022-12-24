using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimalAI))]
public class RaccoonAI : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeItemDestroy = 3f;
    private float itemInHandsTime = 0f;

    private Transform handTransform;
    private GameObject itemInHands;

    [HideInInspector]
    public AnimalAI AnimalAI;
    void Start() {
        AnimalAI = GetComponent<AnimalAI>();
        handTransform = transform.Find("Hand").transform;
    }

    void FixedUpdate() {
        if (AnimalAI.DisableAI || itemInHands == null)
            return;
        
        //  Удаление предмета в руках
        itemInHandsTime += Time.deltaTime;
        if (itemInHandsTime >= delayBeforeItemDestroy)
        {
            Destroy(itemInHands);
            AnimalAI.InterestedInTarget = true;
        }
    }

    /// <summary>
    /// Взять предмет в руки и уничтожить с задержкой
    /// </summary>
    public void Steal(GameObject target)
    {
        AnimalAI.InterestedInTarget = false;
        itemInHands = target;
        itemInHandsTime = 0f;

        //  Украденный предмет больше не взаимодействует с миром
        target.GetComponent<InterestingObject>().Locked = true;
        Rigidbody targetRB = target.GetComponent<Rigidbody>();
        targetRB.detectCollisions = false;
        targetRB.useGravity = false;
        targetRB.isKinematic = true;

        //  Перемещение предмета на место рук
        target.transform.SetParent(transform);
        target.transform.position = handTransform.position;
    }
}
