using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // The color that the grass cube will change to
    public Color hoverColor;

    // The turret on this grass tube
    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        // if we already have a turret on it
        if (turret != null)
        {
            Debug.Log("You can't build it here, there is already a turret there!");
        } else
        {
            // else we build a turret
            GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild, transform.position + new Vector3(0.9f, 1.5f, -0.9f), transform.rotation);
        }
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
