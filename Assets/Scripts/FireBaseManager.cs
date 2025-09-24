using UnityEngine;

using UnityEngine.UI;

using Proyecto26;

using System.Collections.Generic;

using System.Collections;

using TMPro;



public class FirebaseManager : MonoBehaviour

{

    private string baseUrl = "https://simulador-balistico-798bb-default-rtdb.firebaseio.com/";

    public static int shotNumber = 0;



    [Header("Referencia al ReportManager")]

    public ReportManager reportManager;



    [Header("Referencia al Content de la tabla")]

    public Transform tableContent; // Content del ScrollView



    [Header("Prefab de la fila de la tabla")]

    public GameObject rowPrefab; // Prefab de la fila



    // Método para guardar el último disparo

    public void SaveShotFromUI()

    {

        if (reportManager == null)

        {

            Debug.LogWarning("ReportManager no asignado!");

            return;

        }



        float.TryParse(reportManager.angleText.text.Replace("Angle: ", "").Replace("°", ""), out float angle);

        float.TryParse(reportManager.forceText.text.Replace("Force: ", "").Replace(" N", ""), out float force);

        float.TryParse(reportManager.massText.text.Replace("Mass: ", "").Replace(" kg", ""), out float mass);

        float.TryParse(reportManager.flightTimeText.text.Replace("Flight Time: ", "").Replace(" s", ""), out float flightTime);

        float.TryParse(reportManager.velocityText.text.Replace("Impact Velocity: ", "").Replace(" m/s", ""), out float velocity);

        float.TryParse(reportManager.distanceText.text.Replace("Distance: ", "").Replace(" m", ""), out float distance);

        int.TryParse(reportManager.cubesHitText.text.Replace("Cubes Hit: ", ""), out int cubesHit);



        bool hit = reportManager.resultText.text.Contains("YES");



        shotNumber++;

        Result result = new Result("Shot " + shotNumber, hit, velocity, distance, cubesHit, flightTime, force, mass, angle);



        RestClient.Put(baseUrl + "shots/" + shotNumber + ".json", result)

          .Then(response =>

          {

              Debug.Log("Resultado del disparo #" + shotNumber + " guardado en Firebase");

          })

          .Catch(error =>

          {

              Debug.LogError("Error al guardar en Firebase: " + error);

          });

    }



    // Método para obtener y mostrar los resultados de Firebase en la tabla

    public void GetAllResults()

    {

        // Limpiar la tabla antes de agregar nuevas filas

        foreach (Transform child in tableContent)

        {

            Destroy(child.gameObject);

        }



        RestClient.Get<Dictionary<string, Result>>(baseUrl + "shots.json")

          .Then(results =>

          {

              if (results == null || results.Count == 0)

              {

                  Debug.Log("No hay resultados guardados todavía.");

                  return;

              }



              // Recorrer los resultados y mostrarlos en la tabla

              foreach (var kvp in results)

              {

                  Result shotData = kvp.Value;



                  // Instanciar una nueva fila

                  GameObject newRow = Instantiate(rowPrefab, tableContent);



                  // Obtener los componentes TextMeshProUGUI de los hijos del prefab

                  var shotText = newRow.transform.Find("Shot").GetComponent<TextMeshProUGUI>();

                  var velocityText = newRow.transform.Find("Velocity").GetComponent<TextMeshProUGUI>();

                  var distanceText = newRow.transform.Find("Distance").GetComponent<TextMeshProUGUI>();

                  var cubesHitText = newRow.transform.Find("CubesHit").GetComponent<TextMeshProUGUI>();

                  var flightTimeText = newRow.transform.Find("FlightTime").GetComponent<TextMeshProUGUI>();

                  var forceText = newRow.transform.Find("Force").GetComponent<TextMeshProUGUI>();

                  var massText = newRow.transform.Find("Mass").GetComponent<TextMeshProUGUI>();

                  var angleText = newRow.transform.Find("Angle").GetComponent<TextMeshProUGUI>();



                  if (shotText != null) shotText.text = shotData.shotName;

                  if (velocityText != null) velocityText.text = shotData.velocity.ToString("F2");

                  if (distanceText != null) distanceText.text = shotData.distance.ToString("F2");

                  if (cubesHitText != null) cubesHitText.text = shotData.cubesHit.ToString();

                  if (flightTimeText != null) flightTimeText.text = shotData.flightTime.ToString("F2");

                  if (forceText != null) forceText.text = shotData.force.ToString("F2");

                  if (massText != null) massText.text = shotData.mass.ToString("F2");

                  if (angleText != null) angleText.text = shotData.angle.ToString("F2");

              }

          })

          .Catch(error =>

          {

              Debug.Log("No hay resultados guardados, la base de datos está vacía.");

              Debug.LogError("Error al obtener datos de Firebase: " + error);

          });

    }

}