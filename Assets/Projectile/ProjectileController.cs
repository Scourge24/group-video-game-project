using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private const float ProjectileSpeed = 50f;
    private const float Damage = 5f;

    private void Update()
    {
        transform.position += new Vector3(ProjectileSpeed, 0) * Time.deltaTime;

        // Despawn when far enough offscreen.
        if (Mathf.Abs(transform.position.x) > 20f || Mathf.Abs(transform.position.y) > 20f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collisions with other projectiles.
        if (collision.gameObject.GetComponent<ProjectileController>() != null)
        {
            return;
        }

        var enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}
