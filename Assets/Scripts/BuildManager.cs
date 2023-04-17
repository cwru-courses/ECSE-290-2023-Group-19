using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{
    // can be referenced everywhere
    public static BuildManager instance;
    // default turret to build 
    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;
    private TurretBlueprint turretToBuild;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager in scene!");
        }
        instance = this;
    }

    void Start()
    {

    }

    public bool CanBuild { get { return turretToBuild != null; } }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not Enough Money");
        }

        else
        {
            PlayerStats.Money -= turretToBuild.cost;
            // else we build a turret
            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;

            Debug.Log("Money left: " + PlayerStats.Money);
        }
       
    }

    // SEEEEEEEEEEEE here !!!!!!!!!!!!!!!!!!! @Lara
    public void BuildPropOn(EnemyPathNode node)       // this type here is revised
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not Enough Money");
        }

        else
        {
            PlayerStats.Money -= turretToBuild.cost;
            // else we build a turret
            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.prop = turret;

            Debug.Log("Money left: " + PlayerStats.Money);
        }

    }

    public void SelectTurretoBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
