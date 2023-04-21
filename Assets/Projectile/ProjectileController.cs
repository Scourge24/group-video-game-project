using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float ProjectileSpeed = 50f;
    public float Damage = 5f;

    private void Update()
    {
        Vector3 translation = transform.rotation * new Vector3(ProjectileSpeed, 0);
        transform.position += translation * Time.deltaTime;

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

        // Ignore collisions with friendly objects.
        var ownable = collision.gameObject.GetComponent<Ownable>();
        var selfOwnable = GetComponent<Ownable>();
        if (ownable != null && selfOwnable != null && ownable.Team == selfOwnable.Team)
        {
            return;
        }

        var damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}
