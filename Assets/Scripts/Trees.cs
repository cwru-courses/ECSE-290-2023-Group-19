using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Camera cameraObject;

    public GameObject treePrefab;
    public float spawnRadius = 10f;
    public int treeCount = 10;
    public float cutDownDistance = 2f;
    public int maxWoodPerTree = 2;
    public GameObject woodPrefab;

    private List<GameObject> trees = new List<GameObject>();

    void Start()
    {
        //spawn trees
        for (int i = 0; i < treeCount; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 0;
            Vector3 spawnPosition = target.position + randomPosition;
            GameObject tree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
            trees.Add(tree);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject tree in trees)
                {
                    if (Vector3.Distance(tree.transform.position, hit.point) < cutDownDistance)
                    {
                        int woodCount = Random.Range(1, maxWoodPerTree + 1);
                        for (int i = 0; i < woodCount; i++)
                        {
                            Vector3 woodPosition = tree.transform.position + Vector3.up * (i + 1);
                            GameObject wood = Instantiate(woodPrefab, woodPosition, Quaternion.identity);
                        }

                        trees.Remove(tree);
                        Destroy(tree);
                        break;
                    }
                }
            }
        }
    }
}
