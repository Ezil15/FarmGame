using UnityEngine;
using UnityEngine.Events;
public class ResourceComponent : MonoBehaviour
{
    public UnityEvent OnDisappearedEvent;
    public int Price;
    public int Size;
    public bool Despawnable = true;
    private bool isTaken = false;
    public bool IsTaken {
    get { return isTaken; }
    set {
        isTaken = value;
        if (isTaken && (disappearTickCooldown - currentTick <= 1f))
           currentTick = -1.5f;      
        }
    }
    [SerializeField]
    private float disappearTickCooldown;
    private float currentTick = 0f;

    private void FixedUpdate() 
    {
        if(!Despawnable || IsTaken)
            return;
        currentTick += Time.deltaTime;

        if(currentTick >= disappearTickCooldown)
        {
            OnDisappearedEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
