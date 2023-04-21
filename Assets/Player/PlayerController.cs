using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private const float MovementSpeed = 10f;
    private const float FireCooldown = 0.05f;
    private float TimeLastFired;

    public GameObject ProjectilePrefab;

    private ProgressBar HealthBarHUDElement;

    private void Start()
    {
        var uidoc = GameObject.Find("UI").GetComponent<UIDocument>();
        HealthBarHUDElement = uidoc.rootVisualElement.Q<ProgressBar>("HealthBar");

        GetComponent<Damageable>().OnDeath.AddListener(OnDeath);
    }

    private void OnDeath()
    {
        HealthBarHUDElement.value = 0;
        HealthBarHUDElement.title = "UR DED";
    }

    private void Update()
    {
        var damageable = GetComponent<Damageable>();
        HealthBarHUDElement.highValue = damageable.MaxHealth;
        HealthBarHUDElement.value = damageable.Health;

        PollMovementInput();
        PollFireInput();
    }

    private void PollMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position + MovementSpeed * Time.deltaTime * new Vector3(horizontal, vertical).normalized;

        float horizontalCameraBound = Camera.main.orthographicSize * Screen.width / Screen.height;
        float verticalCameraBound = Camera.main.orthographicSize * Screen.height / Screen.width;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalCameraBound, horizontalCameraBound);
        newPosition.y = Mathf.Clamp(newPosition.y, -verticalCameraBound, verticalCameraBound);

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
        proj.transform.position = transform.position;
    }
}
