using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Camera cameraObject;

    public GameObject treePrefab;
    public float scaleX = 50;
    public float scaleZ = 35;

    public float Xoffset = 5;
    public float Zoffset = 5;

    private float environmentPositionX;
    private float environmentPositionZ;

    private int totalTrees = 0;
    public int treeCount = 10;
    public float interactionDistance = 0.5f;
    public int maxWoodPerTree = 2;
    public GameObject woodPrefab;

    private List<GameObject> trees = new List<GameObject>();
    private List<GameObject> woods = new List<GameObject>();

    public float delayTime = 1.0f;

    void Start()
    {
        environmentPositionX = target.position.x;
        environmentPositionZ = target.position.z;

        //spawn trees at the start
        for (int i = 0; i < treeCount; i++)
        {
            InstantiateGameObject();
        }
    }

    void InstantiateGameObject()
    {
            Vector3 locationOfTree = new Vector3(Random.Range(environmentPositionX + Xoffset, environmentPositionX + scaleX), 0,
            Random.Range(environmentPositionZ + Zoffset, environmentPositionZ + scaleZ));

        float distance = 2f; // the distance to cast the ray
        int layerMask = LayerMask.GetMask("Default"); // the layer(s) to check for collisions

        if (Physics.Raycast(locationOfTree, Vector3.down, out RaycastHit hit, distance, layerMask))
        {
            locationOfTree.y = hit.point.y;
            GameObject tree = Instantiate(treePrefab, locationOfTree, Quaternion.identity);
            trees.Add(tree);
            totalTrees++;
        }
    }


    void Update()
    {
        //spawning trees as they get cut down
        if (totalTrees < treeCount)
        {
            InstantiateGameObject();
        }

        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //collecting wood
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Collectible"))
                {
                    GameObject wood = hit.collider.gameObject;
                    PlayerStats.totalWood++;
                    woods.Remove(wood);
                    Destroy(wood);
                    Debug.Log("Collected wood");
                }
            }
        }

        //cutting down tree
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                foreach (GameObject tree in trees)
                {
                    if (hit.collider.gameObject == tree)
                    {
                        int woodCount = Random.Range(1, maxWoodPerTree + 1);
                        float woodHeightOffset = 0.5f;
                        float woodHorizontalOffset = 0.9f;

                        for (int i = 0; i < woodCount; i++)
                        {
                            Vector3 woodPosition = tree.transform.position + Vector3.up * woodHeightOffset * (i + 1) + Vector3.right * woodHorizontalOffset * i;
                            GameObject wood = Instantiate(woodPrefab, woodPosition, Quaternion.identity);
                            woods.Add(wood);

                            if (Physics.Raycast(wood.transform.position, Vector3.down, out hit, Mathf.Infinity))
                            {
                                wood.transform.position = hit.point + Vector3.up * (wood.transform.localScale.y / 2.0f);
                            }
                        }

                        trees.Remove(tree);
                        Destroy(tree);
                        totalTrees--;

                        Debug.Log("Cut down tree");
                        break;
                    }
                }
            }
        }
    }
}