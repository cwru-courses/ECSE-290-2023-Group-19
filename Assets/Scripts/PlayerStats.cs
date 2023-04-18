using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int totalMoney;
    public int startMoney = 400;

    public static int totalWood;
    public int startWood = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalMoney = startMoney;
        totalWood = startWood;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
