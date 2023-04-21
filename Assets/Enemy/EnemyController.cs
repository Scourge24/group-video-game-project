using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject ProjectilePrefab;

    private const float FireCooldown = 0.25f;
    private float TimeLastFired;

    private void Update()
    {
        if (Time.time - TimeLastFired > FireCooldown)
        {
            Fire();
        }
    }

    private void Fire()
    {
        TimeLastFired = Time.time;

        GameObject proj = Instantiate(ProjectilePrefab);
        proj.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
