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

    [Header("UI de prueba")]
    public TextMeshProUGUI debugText;
    public TMP_InputField shotIndexInput;

    [Header("Referencia al ReportManager")]
    public ReportManager reportManager;

    private void Start()
    {
        GetShotNumber();
    }

    private void GetShotNumber()
    {
        RestClient.Get<Dictionary<string, Result>>(baseUrl + "shots.json")
            .Then(results =>
            {
                if (results != null)
                {
                    foreach (var key in results.Keys)
                    {
                        int currentShot = 0;
                        if (int.TryParse(key, out currentShot) && currentShot > shotNumber)
                        {
                            shotNumber = currentShot;
                        }
                    }
                }
            })
            .Catch(error => {
                Debug.Log("No hay datos en Firebase, shotNumber se mantendrá en 0.");
            });
    }

    private void SaveToDataBase(Result result)
    {
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

        SaveToDataBase(result);
    }

    public void GetSingleResult()
    {
        string shotIndex = shotIndexInput.text;
        if (string.IsNullOrEmpty(shotIndex))
        {
            debugText.text = "Por favor, introduce un número de disparo.";
            return;
        }

        RestClient.Get<Result>(baseUrl + "shots/" + shotIndex + ".json")
            .Then(response =>
            {
                if (response != null)
                {
                    debugText.text =
                        $"Shot #{shotIndex}\n" +
                        $"Velocity: {response.velocity:F2} m/s\n" +
                        $"Distance: {response.distance:F2} m\n" +
                        $"Impact: {(response.hit ? "Yes" : "No")}\n" +
                        $"Cubes Hit: {response.cubesHit}\n" +
                        $"Flight time: {response.flightTime:F2} s";
                }
                else
                {
                    debugText.text = "No se encontró el disparo.";
                }
            })
            .Catch(error =>
            {
                debugText.text = "Error al obtener datos: " + error.Message;
                Debug.LogError("Error al obtener datos: " + error);
            });
    }
}