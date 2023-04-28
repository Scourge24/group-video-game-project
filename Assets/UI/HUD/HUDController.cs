using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public Texture2D EmptyHealthbarSegmentTexture;
    public Texture2D FilledHealthbarSegmentTexture;
    private float _CurrentHealth;
    public float CurrentHealth {
        get {
            return _CurrentHealth;
        }
        set {
            bool needsUpdate = value != _CurrentHealth;
            _CurrentHealth = value;
            if (needsUpdate)
            {
                UpdateHealthSegments();
            }
        }
    }

    private float _MaxHealth;
    public float MaxHealth {
        get {
            return _MaxHealth;
        }
        set {
            bool needsUpdate = value != _MaxHealth;
            _MaxHealth = value;
            if (needsUpdate)
            {
                UpdateHealthSegments();
            }
        }
    }

    private VisualElement HealthbarSegmentsContainer;

    private void Start()
    {
        HealthbarSegmentsContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("healthbar-segments-container");
    }

    private void UpdateHealthSegments()
    {
        HealthbarSegmentsContainer.Clear();
        for (int i = 0; i < MaxHealth; i++)
        {
            VisualElement segment = new();

            Texture2D textureToUse;
            if (CurrentHealth <= i)
            {
                textureToUse = EmptyHealthbarSegmentTexture;
            }
            else
            {
                textureToUse = FilledHealthbarSegmentTexture;
            }

            Background newBackground = segment.style.backgroundImage.value;
            newBackground.texture = textureToUse;
            segment.style.backgroundImage = newBackground;

            segment.style.width = 60;
            segment.style.height = 40;
            segment.style.marginRight = 10;

            HealthbarSegmentsContainer.Add(segment);
        }
    }
}
