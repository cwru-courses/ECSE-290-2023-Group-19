using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    private BombBlueprint bombToBuild;

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

    public bool CanBuildTurret { get { return turretToBuild != null; } }

    public bool CanBuildProp { get { return bombToBuild != null; } }

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
        if (PlayerStats.totalBomb < bombToBuild.bombs)
        {
            Debug.Log("Not Enough Bombs");
        }

        else
        {
            PlayerStats.totalBomb -= bombToBuild.bombs;
            // else we build the bomb
            GameObject bombDown = (GameObject)Instantiate(bombToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.prop = bombDown;

            Debug.Log("Bombs left: " + PlayerStats.totalBomb);
        }

    }

    public void SelectTurretoBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void SelectBombtoBuild(BombBlueprint bomb)
    {
        bombToBuild = bomb;
    }

    
}
