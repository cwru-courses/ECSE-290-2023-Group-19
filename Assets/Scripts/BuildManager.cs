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
            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition() + new Vector3(0.0f, 0.3f, 0.0f), Quaternion.identity);
            node.turret = turret;

            Debug.Log("Money left: " + PlayerStats.totalMoney);
        }
       
    }

    public void BuildPropOn(EnemyPathNode node)
    {
        if (PlayerStats.totalBomb < turretToBuild.bombs)
        {
            Debug.Log("Not Enough Bombs");
        }

        else
        {
            PlayerStats.totalBomb -= turretToBuild.bombs;
            // else we build the bomb
            GameObject bombDown = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.prop = bombDown;

            Debug.Log("Bombs left: " + PlayerStats.totalBomb);
        }

    }

    public void SelectTurretoBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
