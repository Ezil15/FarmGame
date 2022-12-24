using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance {get; private set;}

    public TMP_Text Text;
    public int Coins {get; private set;}
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

    public void addMoney(int Coins)
    {
        this.Coins += Coins;
        Text.text = Coins.ToString();
    }
}
