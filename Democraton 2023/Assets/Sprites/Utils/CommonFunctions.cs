using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunctions
{

    public static bool IsColliderWithTag(Collider collider, string tagToCompare)
    {
        return collider.gameObject.tag == tagToCompare;
    }

    public static bool IsColliderWithPlayer(Collider collider)
    {
        return collider.gameObject.tag == Tags.PLAYER;
    }

    public static bool IsCollisionWithPlayer(Collision collision)
    {
        return collision.gameObject.tag == Tags.PLAYER;
    }

    public static bool IsCollisionWithTag(Collision collision, string tagToCompare)
    {
        return collision.gameObject.tag == tagToCompare;
    }

    public static bool IsRaycastHitWithTag(RaycastHit hit, string tagToCompare)
    {
        return hit.transform.tag == tagToCompare;
    }

    public static Collider IsThereObjectsAroundWithTag(Vector3 position, float radius, string tagToCompare)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (IsColliderWithTag(hitCollider, Tags.WALL))
            {
                return hitCollider;
            }
        }
        return null;
    }

    public static RaycastHit GetRayHit(Vector3 rayOrigin, Vector3 rayDirection, float distance)
    {
        RaycastHit hit;
        Physics.Raycast(rayOrigin, rayDirection, out hit, distance);
        Debug.DrawRay(rayOrigin, rayDirection * distance, Color.red);
        return hit;
    }

    public static void SetParticleRateOverTimeEmission(ParticleSystem effect, float value)
    {
        var emission = effect.emission;
        emission.rateOverTime = value;
    }
    public static void SetParticleRateOverTimeEmissionAndPlay(ParticleSystem effect, float value)
    {
        var emission = effect.emission;
        emission.rateOverTime = value;
        effect.Play();
    }

    public static bool IsClose(Vector3 target, Vector3 detector, float distance, float heightOffset = 0.0f)
    {
        Vector3 eyePos = detector + Vector3.up * heightOffset;
        Vector3 toPlayer = target - eyePos;
        Vector3 toPlayerFlat = toPlayer;
        toPlayerFlat.y = 0;
        if (toPlayerFlat.sqrMagnitude <= distance * distance)
        {
            return true;
        }
        return false;
    }
}

public class float_lerp
{
    private float start;
    private float end;
    private Timer timer;

    public float_lerp(float start = 0.0f, float end = 0.0f)
    {
        this.start = start;
        this.end = end;
        timer = new Timer(1.0f);
    }
    public void setStart(float start)
    {
        this.start = start; 
    }
    public void setEnd(float end)
    {
        this.end = end;
    }
    public void StartLerp()
    {
        timer.SetTimerTime(1.0f);
        timer.ActivateTimer();
    }
    public float UpdateTimer(float amout)
    {
            timer.SubtractTimerByValue(amout);
            return Mathf.Lerp(start, end, timer.GetCurrentTime());
    }
}
