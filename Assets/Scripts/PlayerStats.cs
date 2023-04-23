using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int totalMoney;
    public int startMoney = 400;

    public static int totalWood;
    public int startWood = 0;

    public static int totalBomb;
    public int startBomb = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalMoney = startMoney;
        totalWood = startWood;
        totalBomb = startBomb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
