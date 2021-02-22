using Game.Graphics;
using System;
using UnityEngine;

public class ShadowEffect : MonoBehaviour
{
    public Sprite sprite;
    public Material material;
    private DayNightCycleManager dayNightCycleManager;

    private GameObject _shadow;
    private SpriteRenderer sr;

    void Awake()
    {
        
    }

    void Start()
    {
        dayNightCycleManager = DayNightCycleManager.getInstance();
        _shadow = new GameObject("Shadow");
        _shadow.transform.parent = transform;
        
        _shadow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));

        sr = _shadow.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingLayerName = "Shadow";
        sr.sortingOrder = 0;
        sr.material = material;
        _shadow.transform.localPosition = new Vector3(0, 0, 0);

        calculateShadow();
    }

    void Update()
    {
        calculateShadow();
    }

    private void calculateShadow()
    {
        _shadow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, dayNightCycleManager.getShadowAngle()));
        sr.color = new Color(0, 0, 0, dayNightCycleManager.getShadowAlpha());
        sr.transform.localScale = new Vector3(1, dayNightCycleManager.getShadowYScale(), 0);
    }
}