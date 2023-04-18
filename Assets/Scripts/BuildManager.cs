using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
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
        if (PlayerStats.totalMoney < turretToBuild.cost)
        {
            Debug.Log("Not Enough Money");
        }

        else
        {
            PlayerStats.totalMoney -= turretToBuild.cost;
            // else we build a turret
            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;

            Debug.Log("Money left: " + PlayerStats.totalMoney);
        }
       
    }

    // SEEEEEEEEEEEE here !!!!!!!!!!!!!!!!!!! @Lara
    public void BuildPropOn(EnemyPathNode node)       // this type here is revised
    {
        if (PlayerStats.totalMoney < turretToBuild.cost)
        {
            Debug.Log("Not Enough Money");
        }

        else
        {
            PlayerStats.totalMoney -= turretToBuild.cost;
            // else we build a turret
            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.prop = turret;

            Debug.Log("Money left: " + PlayerStats.totalMoney);
        }

    }

    public void SelectTurretoBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
