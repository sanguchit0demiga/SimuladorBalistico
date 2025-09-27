using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public GameObject cubePrefab;

    private List<GameObject> cubes = new List<GameObject>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private List<Quaternion> initialRotations = new List<Quaternion>();

 
    [System.Serializable]
    public class JointInfo
    {
        public int cubeIndex;
        public int connectedCubeIndex;
    }
    private List<JointInfo> initialJoints = new List<JointInfo>();

    void Start()
    {
        cubes.Clear();
        initialPositions.Clear();
        initialRotations.Clear();
        initialJoints.Clear();

        GameObject[] foundCubes = GameObject.FindGameObjectsWithTag("Cube");

        for (int i = 0; i < foundCubes.Length; i++)
        {
            GameObject cube = foundCubes[i];
            cubes.Add(cube);
            initialPositions.Add(cube.transform.position);
            initialRotations.Add(cube.transform.rotation);

            FixedJoint fixedJoint = cube.GetComponent<FixedJoint>();
            if (fixedJoint != null && fixedJoint.connectedBody != null)
            {
                int connectedIndex = System.Array.IndexOf(foundCubes, fixedJoint.connectedBody.gameObject);
                if (connectedIndex != -1)
                {
                    initialJoints.Add(new JointInfo
                    {
                        cubeIndex = i,
                        connectedCubeIndex = connectedIndex
                    });
                }
            }
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
            if (cube != null)
            {
                Destroy(cube);
            }
        }
        cubes.Clear();

        yield return null; 

        List<GameObject> instantiatedCubes = new List<GameObject>();
        for (int i = 0; i < initialPositions.Count; i++)
        {
            GameObject newCube = Instantiate(cubePrefab, initialPositions[i], initialRotations[i]);
            instantiatedCubes.Add(newCube);
        }
        cubes = instantiatedCubes;

        yield return null; 

        foreach (JointInfo ji in initialJoints)
        {
            if (cubes[ji.cubeIndex] != null && cubes[ji.connectedCubeIndex] != null)
            {
                Rigidbody connectedRb = cubes[ji.connectedCubeIndex].GetComponent<Rigidbody>();
                if (connectedRb != null)
                {
                    FixedJoint newJoint = cubes[ji.cubeIndex].AddComponent<FixedJoint>();
                    newJoint.connectedBody = connectedRb;
                }
            }
        }

        ReportManager rm = FindFirstObjectByType<ReportManager>();
        if (rm != null)
            rm.ResetHits();
    }
}