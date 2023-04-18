using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPathNode : MonoBehaviour
{
    // The color that the grass cube will change to
    public Color hoverColor;

    [Header("Optional")]
    // The object on this sand tube
    public GameObject prop;

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

        if (!buildManager.CanBuild)
        {
            return;
        }

        // if we already have a prop on it
        if (prop != null)
        {
            Debug.Log("You can only put them on enemy's path!!!");
            return;
        }
        else
        {

            buildManager.BuildPropOn(this);
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
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
