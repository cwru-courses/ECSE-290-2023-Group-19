using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    // The color that the grass cube will change to
    public Color hoverColor;

    [Header("Optional")]
    // The turret on this grass tube
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + new Vector3(0.9f, 1.5f, -0.9f);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

            if (!buildManager.CanBuildTurret)
        {
            return;
        }

        // if we already have a turret on it
        if (turret != null)
        {
            Debug.Log("You can't build it here, there is already a turret there!");
            return;
        }
        else
        {

            buildManager.BuildTurretOn(this);
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuildTurret)
        {
            return;
        }

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
