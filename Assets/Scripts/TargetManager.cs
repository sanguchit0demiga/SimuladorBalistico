using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public GameObject cubePrefab; // prefab de cubo
    private List<GameObject> cubes = new List<GameObject>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private List<Quaternion> initialRotations = new List<Quaternion>();

    void Start()
    {
        // Guardar cubos iniciales en la escena
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag("Cube"))
        {
            cubes.Add(cube);
            initialPositions.Add(cube.transform.position);
            initialRotations.Add(cube.transform.rotation);
        }
    }

    public void ResetCubes()
    {
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
       
        foreach (GameObject cube in cubes)
        {
            cube.SetActive(false);
        }

        cubes.Clear();

        yield return null;

        
        for (int i = 0; i < initialPositions.Count; i++)
        {
            GameObject newCube = Instantiate(cubePrefab, initialPositions[i], initialRotations[i]);
            cubes.Add(newCube);
        }

        ReportManager rm = FindFirstObjectByType<ReportManager>();
        if (rm != null)
            rm.ResetHits();
    }
}
