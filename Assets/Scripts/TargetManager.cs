using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public GameObject cubePrefab; // asigná tu prefab en el inspector

    private List<GameObject> cubes = new List<GameObject>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private List<Quaternion> initialRotations = new List<Quaternion>();

    void Start()
    {
        // Guardar referencia de los cubos iniciales en la escena
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
        // 1. Destruir todos los cubos actuales
        foreach (GameObject cube in cubes)
        {
            Destroy(cube);
        }

        // 2. Limpiar lista de cubos
        cubes.Clear();

        // 3. Esperar un frame antes de recrearlos
        yield return null;

        // 4. Reinstanciar los cubos desde el prefab
        for (int i = 0; i < initialPositions.Count; i++)
        {
            GameObject newCube = Instantiate(cubePrefab, initialPositions[i], initialRotations[i]);
            cubes.Add(newCube);
        }
    }
}
