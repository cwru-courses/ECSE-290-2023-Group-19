using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint cannon;
    BuildManager buildManager;

   public void SelectCannon()
    {
        Debug.Log("Cannon Selected");
        buildManager.SelectTurretoBuild(cannon);
    }

    public void SelectAnotherTurret()
    {
        Debug.Log("Another Turret Purchased");
        //buildManager.SelectTurretoBuild(buildManager.anotherTurretPrefab);
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
