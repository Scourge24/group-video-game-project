using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public GameObject ProjectilePrefab;

    public const float PlayAreaWidth = 20;
    public const float PlayAreaHeight = 10;

    private const float MovementSpeed = 10f;
    private const float FireCooldown = 0.05f;

    private float TimeLastFired;

    private Damageable Damageable;
    private HUDController HUD;

    private void Start()
    {
        HUD = FindObjectOfType<HUDController>();
        Damageable = GetComponent<Damageable>();

        Damageable.OnDeath.AddListener(OnDeath);
    }

    private void OnDeath()
    {
        // Once our health hits zero we'll be destroyed before updating again, so give the HUD a final update manually.
        HUD.CurrentHealth = Damageable.Health;

        // Kick the player to the main menu after a delay for now.
        SessionManager.Instance.StartCoroutine(KickPlayerToMainMenuAfterDelay());
    }

    private IEnumerator KickPlayerToMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        HUD.CurrentHealth = Damageable.Health;
        HUD.MaxHealth = Damageable.MaxHealth;

        PollMovementInput();
        PollFireInput();
    }

    private void PollMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position + MovementSpeed * Time.deltaTime * new Vector3(horizontal, vertical).normalized;

        newPosition.x = Mathf.Clamp(newPosition.x, -PlayAreaWidth / 2, PlayAreaWidth / 2);
        newPosition.y = Mathf.Clamp(newPosition.y, -PlayAreaHeight / 2, PlayAreaHeight / 2);

        transform.position = newPosition;
    }

    private void PollFireInput()
    {
        if (Input.GetButton("Fire1") && Time.time - TimeLastFired > FireCooldown)
        {
            Fire();
        }
    }

    private void Fire()
    {
        TimeLastFired = Time.time;

        GameObject proj = Instantiate(ProjectilePrefab);
        proj.GetComponent<Ownable>().Team = GetComponent<Ownable>().Team;
        proj.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
