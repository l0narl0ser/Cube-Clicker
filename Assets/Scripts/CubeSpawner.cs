using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CubeSpawner : MonoBehaviour
{
    public GameObject CubePrefabs;
    public List<GameObject> gameObjectList;
    public float scalingFactor = 0.99995f;
    public int numCubes = 0;

    [SerializeField]
    private float _minimalScale = 0.9f;

    void Start()
    {
        gameObjectList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObjectSpawn();
        }
        UpdateCubesStates();
    }

    private void UpdateCubesStates()
    {
        UpdateCubesSize();
        TryRemoveCubes();
    }

    private void TryRemoveCubes()
    {
        GameObject[] copiedGameObjectList = gameObjectList.ToArray();
        foreach (GameObject goTemp in copiedGameObjectList)
        {
            if (goTemp.transform.localScale.x < _minimalScale)
            {
                gameObjectList.Remove(goTemp);
                Destroy(goTemp);
            }

        }
    }

    private void UpdateCubesSize()
    {

        foreach (GameObject goTemp in gameObjectList)
        {
            SizeReducing(goTemp);
        }
    }

    private void GameObjectSpawn()
    {
        numCubes++;
        gameObjectList.Add(GameObjectCreate());


    }
     
    

    private void SizeReducing(GameObject cube)
    {
        float scale = cube.transform.localScale.x;
        scale *= scalingFactor;
        cube.transform.localScale = Vector3.one * scale; // 0,95 * (1 , 1 , 1) =  (0,95 , 0,95 , 0,95)
        
    }

    private GameObject GameObjectCreate()
    {
        
        GameObject cube = Instantiate<GameObject>(CubePrefabs);
        cube.name = "Cube" + numCubes;
        Color color = new Color(Random.value, Random.value, Random.value);
        cube.GetComponent<Renderer>().material.color = color;
        cube.transform.position = Random.insideUnitSphere + new Vector3(0, Random.RandomRange(1, 3), 0);
        return cube;
    }
}
