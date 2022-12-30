using UnityEngine;

public class ResourceComponent : MonoBehaviour
{
    public GameObject Object;
    public int Price;
    public bool Despawnable = true;
    public bool isTaken = false;
    public int size;
    [SerializeField]
    private float disappearTickCooldown;
    

    private void FixedUpdate() 
    {
        if(isTaken)
            return;
        if(!Despawnable)
            return;
        disappearTickCooldown -= Time.deltaTime;

        if(disappearTickCooldown <= 0f)
            Destroy(Object);
    }

    private void TakeOrDrop() => isTaken = !isTaken;
}
