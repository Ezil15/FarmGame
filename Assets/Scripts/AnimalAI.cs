using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    /// <summary>
    /// Список объектов которые интересуют животное
    /// </summary>
    public List<InterestingObjectType> InterestingObjectTypes = new();

    /// <summary>
    /// Если true - отключает предоставляемый AnimalAI компонентом интеллект
    /// </summary>
    public bool DisableAI = false;

    /// <summary>
    /// Если false - бродит без цели и не взаимодействует с интересующими объектами (Напр. не голодное)
    /// </summary>
    public bool InterestedInTarget = true;

    [SerializeField]
    private float searchObjectCooldown = 0.5f;
    private float lastObjectSearch = 0f;

    [HideInInspector]
    public GameObject Target;

    [SerializeField]
    private float jumpForceModifier = 1f;
    [SerializeField]
    private float jumpCooldown = 1f;
    [SerializeField]
    private float jumpHeight = 1f;
    private float lastJump = 0f;

    [SerializeField]
    private float rotationModifier = 1f;

    [HideInInspector]
    public Rigidbody Rigidbody;
    private SensorToolkit.RangeSensor interestingObjectSensor;
    public void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        interestingObjectSensor = GetComponentInChildren<SensorToolkit.RangeSensor>();

        lastJump = Random.Range(-1f, 0f);
    }

    private void FixedUpdate()
    {
        if (DisableAI)
            return;
        
        //  Периодическое обновление ближайшего таргета
        if (InterestedInTarget)
        {
            lastObjectSearch += Time.deltaTime;
            if (lastObjectSearch >= searchObjectCooldown)
            {
                lastObjectSearch = 0f;
                Target = FindInterestingObject();
            }
        }
            
        //  Движение
        lastJump += Time.deltaTime;
        if (lastJump >= jumpCooldown)
        {
            lastJump = 0f;
            if (Target && InterestedInTarget) // К найденной цели
            {
                //  Если цель уже рядом
                interestingObjectSensor.Pulse();
                List<InterestingObject> foundObjs = interestingObjectSensor.GetDetectedByComponent<InterestingObject>();
                if (foundObjs.Count > 0)
                {
                    foreach (InterestingObject obj in foundObjs)
                    {
                        if (!InterestingObjectTypes.Contains(obj.Type))
                            continue;
                        //  TODO: Взаимодействие с целью здесь
                        return;
                    }
                }
                //  Если цель далеко - продолжать движение
                JumpTo(Target.transform.position);
            }
            else // Бесцельная ходьба по полю
            {
                JumpTo(AnimalsArea.GetRandomPoint());
            }
        }
    }

    /// <summary>
    /// Поиск ближайшего объекта который интересует животное
    /// Может вернуть null
    /// </summary>
    public GameObject FindInterestingObject()
    {
        GameObject nearestObject = null;
        float nearestDistance = 100f;

        foreach (InterestingObject obj in InterestingObject.ObjectsOnMap)
        {
            if (!InterestingObjectTypes.Contains(obj.Type))
                continue;
            
            float distance = Vector3.Distance(gameObject.transform.position, obj.gameObject.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = obj.gameObject;
            }
        }
        
        return nearestObject;
    }

    /// <summary>
    /// Прыжок в направлении к переданной точке
    /// </summary>
    public void JumpTo(Vector3 position, bool waitRotation = true)
    {
        //  Поворот
        Vector3 lookPosition = position - transform.position;
        lookPosition.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationModifier);

        if (waitRotation && (Quaternion.Angle(transform.rotation, rotation) > 15f))
        {
            Rigidbody.AddForce(Vector3.up * jumpHeight * 0.5f, ForceMode.Impulse);
            return;
        }

        //  Перемещение
        Vector3 direction = (position - transform.position).normalized * jumpForceModifier;
        direction.y = jumpHeight;
        Rigidbody.AddForce(direction, ForceMode.Impulse);
    }
}