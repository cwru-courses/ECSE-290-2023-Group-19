using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    private BombBlueprint bombToBuild;
    public TextMeshProUGUI CoinsAlert;
    public TextMeshProUGUI WoodsAlert;

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
            StartCoroutine(ShowAndHideCoinsAlert());
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

    IEnumerator ShowAndHideCoinsAlert()
    {
        // show the text object
        CoinsAlert.gameObject.SetActive(true);
        Debug.Log(1);
        // wait for three seconds
        yield return new WaitForSeconds(2f);
        // hide the text object
        CoinsAlert.gameObject.SetActive(false);
    }

    public IEnumerator ShowAndHideWoodsAlert()
    {
        // show the text object
        WoodsAlert.gameObject.SetActive(true);
        Debug.Log(1);
        // wait for three seconds
        yield return new WaitForSeconds(2f);
        // hide the text object
        WoodsAlert.gameObject.SetActive(false);
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
