using UnityEngine;
using TMPro;
using System.Drawing;

using System.Collections;
public class Shop : MonoBehaviour
{
    public TurretBlueprint cannon;
    public TurretBlueprint arrowTower;
    public TurretBlueprint slowingTower;
    public BombBlueprint bomb;
    BuildManager buildManager;
    public GameObject cannoninfo;
    public GameObject arrowinfo;
    public GameObject slowinfo;
    public GameObject bombinfo;



    public void SelectCannon()
    {
        Debug.Log("Cannon Selected");
        buildManager.SelectTurretoBuild(cannon);
        StartCoroutine(ShowAndHideCannonInfo());
    }

    IEnumerator ShowAndHideCannonInfo()
    {
        HideAllInfos();
        cannoninfo.SetActive(true);
        yield return new WaitForSeconds(3f);
        cannoninfo.SetActive(false);
    }
        IEnumerator ShowAndHideArrowInfo()
    {
        HideAllInfos();
        arrowinfo.SetActive(true);
        yield return new WaitForSeconds(3f);
        arrowinfo.SetActive(false);
    }
        IEnumerator ShowAndHideSlowInfo()
    {
        HideAllInfos();
        slowinfo.SetActive(true);
        yield return new WaitForSeconds(3f);
        slowinfo.SetActive(false);
    }
        IEnumerator ShowAndHideBombInfo()
    {
        HideAllInfos();
        bombinfo.SetActive(true);
        yield return new WaitForSeconds(3f);
        bombinfo.SetActive(false);
    }

    private void HideAllInfos()
    {
        cannoninfo.SetActive(false);
        arrowinfo.SetActive(false);
        slowinfo.SetActive(false);
        bombinfo.SetActive(false);
    }

    public void SelectArrowTurret()
    {
        Debug.Log("Arrow Turret Selected");
        buildManager.SelectTurretoBuild(arrowTower);
        StartCoroutine(ShowAndHideArrowInfo());
    }

    public void SelectSlowingTurret()
    {
        Debug.Log("Slowing Turret Selected");
        buildManager.SelectTurretoBuild(slowingTower);
StartCoroutine(ShowAndHideSlowInfo());
    }

    public void SelectBomb()
    {
        Debug.Log("BombSelected");
        buildManager.SelectBombtoBuild(bomb);
StartCoroutine(ShowAndHideBombInfo());
    }

    // Start is called before the first frame update
    void Start()
    {

        buildManager = BuildManager.instance;
        HideAllInfos();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
