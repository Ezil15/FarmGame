using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InterestingObjectType
{
    Test
}

public class InterestingObject : MonoBehaviour
{
    [HideInInspector]
    public static List<InterestingObject> ObjectsOnMap = new();

    public InterestingObjectType Type;

    private bool locked;
    /// <summary>
    /// Если true - животные игнорируют этот объект
    /// </summary>
    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            locked = value;
            if (locked)
                ObjectsOnMap.Remove(this);
            else
                ObjectsOnMap.Add(this);
        }
    }

    void Start()
    {
        Locked = false;
    }

    private void OnDestroy() {
        ObjectsOnMap.Remove(this);
    }
}
