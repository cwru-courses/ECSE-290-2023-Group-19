using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSell : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;
    [Header("Sell")]
    [Range(0, 1)]
    [SerializeField] private float sellPert;

    public float SellPerc { get; set; }

    public int UpgradeCost { get; set; }

    public int Level { get; set; }

    private ShootingTower _cannon;

    // Start is called before the first frame update
    void Start()
    {
        _cannon = GetComponent<ShootingTower>();
        UpgradeCost = upgradeInitialCost;

        SellPerc = sellPert;
        Level = 1;
        
    }

    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPerc);
        return sellValue;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
