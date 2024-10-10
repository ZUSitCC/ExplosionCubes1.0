using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public static event Action<Cube> Clicked;

    private int _scaleDevider = 2;
    private int _chanceDevider = 2;

    private Material _material;

    [SerializeField] public float SplitChance { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    public void Initialization(float splitChance)
    {
        Rigidbody.useGravity = true;
        Rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

        SplitChance = splitChance / _chanceDevider;
        transform.localScale /= _scaleDevider;

        _material.color = Random.ColorHSV();
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;

        SplitChance = 1;
    }

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);

        Destroy(gameObject);
    }
}