using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDisappearedEvent : UnityEvent<GameObject> {}

public class ResourceComponent : MonoBehaviour
{
    public GameObject Object;
    public OnDisappearedEvent OnDisappearedEvent;
    public int Price;
    public int Size;
    public bool Despawnable = true;
    private bool isTaken = false;
    [SerializeField]
    private float disappearTickCooldown;
    private float currentTick = 0f;
    

    private void FixedUpdate() 
    {
        if(!Despawnable)
            return;
        if(isTaken)
        {
            if(disappearTickCooldown - currentTick <= 1f)
                currentTick = 1.5f;
            return;
        }
        currentTick += Time.deltaTime;

        if(currentTick >= disappearTickCooldown)
            OnDisappearedEvent.Invoke(Object);
    }

    public void TakeOrDrop() => isTaken = !isTaken;

    public void OnDisappeared(GameObject Object) => Destroy(Object);
}
