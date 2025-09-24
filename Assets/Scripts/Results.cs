using System;

[Serializable]
public class Result
{
    public string shotName;
    public bool hit;
    public float velocity;
    public float distance;
    public int cubesHit;
    public float flightTime;
    public float force;
    public float mass;
    public float angle;

    public Result(string shotName, bool hit, float velocity, float distance, int cubesHit, float flightTime, float force, float mass, float angle)
    {
        this.shotName = shotName;
        this.hit = hit;
        this.velocity = velocity;
        this.distance = distance;
        this.cubesHit = cubesHit;
        this.flightTime = flightTime;
        this.force = force;
        this.mass = mass;
        this.angle = angle;
    }
}
