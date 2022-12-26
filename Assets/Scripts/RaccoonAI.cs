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

    private bool captured = false;
    private float timeInCapture = 0f;
    [SerializeField]
    private float timeBeforeEscape = 10f;

    [HideInInspector]
    public AnimalAI AnimalAI;
    void Start() {
        AnimalAI = GetComponent<AnimalAI>();
        handTransform = transform.Find("Hand").transform;
    }

    void FixedUpdate() {
        if (captured)
        {
            timeInCapture += Time.deltaTime;
            if (timeInCapture >= timeBeforeEscape)
                Escape();
            return;
        }

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
        if (targetRB != null)
        {
            targetRB.detectCollisions = false;
            targetRB.useGravity = false;
            targetRB.isKinematic = true;
        }

        //  Перемещение предмета на место рук
        target.transform.SetParent(transform);
        target.transform.position = handTransform.position;
    }

    /// <summary>
    /// Выбросить предмет из рук
    /// </summary>
    public void Drop()
    {
        if (itemInHands == null)
            return;

        Rigidbody targetRB = itemInHands.GetComponent<Rigidbody>();
        if (targetRB != null)
        {
            targetRB.detectCollisions = true;
            targetRB.useGravity = true;
            targetRB.isKinematic = false;
        }

        itemInHands.transform.SetParent(null);
        itemInHands.GetComponent<InterestingObject>().Locked = false;
        itemInHands = null;
        AnimalAI.InterestedInTarget = true;
    }


    /// <summary>
    /// Захват енота в плен
    /// </summary>
    public void Capture()
    {
        Drop();
        AnimalAI.DisableAI = true;
        timeInCapture = 0f;
        captured = true;
    }

    /// <summary>
    /// Побег из плена
    /// </summary>
    public void Escape()
    {
        captured = false;
        AnimalAI.DisableAI = false;
    }
}
