using UnityEngine;
using TMPro;
using System.Drawing;

public class Shop : MonoBehaviour
{ 
    public TurretBlueprint cannon;
    public TurretBlueprint arrowTower;
    public TurretBlueprint slowingTower;
    public BombBlueprint bomb;
    BuildManager buildManager;

   public void SelectCannon()
    {
        Debug.Log("Cannon Selected");
        buildManager.SelectTurretoBuild(cannon);
    }

    public void SelectArrowTurret()
    {
        Debug.Log("Arrow Turret Selected");
        buildManager.SelectTurretoBuild(arrowTower);
    }

    public void SelectSlowingTurret()
    {
        Debug.Log("Slowing Turret Selected");
        buildManager.SelectTurretoBuild(slowingTower);
    }

    public void SelectBomb()
    {
        Debug.Log("BombSelected");
        buildManager.SelectBombtoBuild(bomb);
    }

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
