using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance {get; private set;}

    public TMP_Text Text;
    private int coins = 0;
    public int Coins {get{return coins;} set{
        coins = value;
        Text.text = coins.ToString();
    }}
    void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }

    public void ModifyCoins(int count)
    {
        Coins += count;
    }
}
