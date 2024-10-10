using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Настройка спавнера.")]
    [Tooltip("Компонент взрыватель.")]
    [SerializeField] private Exploder _exploder;

    [Tooltip("Минимальное количество клонов.")]
    [SerializeField, Range(1, 6)] private int _countClonesMin = 2;
    [Tooltip("Максимальное количество клонов.")]
    [SerializeField, Range(1, 6)] private int _countClonesMax = 6;

    private int _clonesCount;

    private float _dispersion = 0.04f;

    private void OnValidate()
    {
        if (_countClonesMin >= _countClonesMax)
            _countClonesMax = _countClonesMax - 1;
    }

    private void OnEnable()
    {
        Cube.Clicked += Spawn;
    }

    private void OnDisable()
    {
        Cube.Clicked -= Spawn;
    }

    public void Spawn(Cube cube)
    {
        _clonesCount = Random.Range(_countClonesMin, _countClonesMax + 1);

        if (cube.SplitChance >= Random.value)
            _exploder.Explode(Create(_clonesCount, cube), cube.transform.position);
    }

    public List<Rigidbody> Create(int count, Cube cube)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        for (int i = 0;  i < count; i++)
        {
            Cube cloneCube = Instantiate(
                cube, RandomizePosition(cube.transform.position), Quaternion.identity
                );

            cloneCube.Initialization(cube.SplitChance);

            rigidbodies.Add(cloneCube.Rigidbody);
        }

        return rigidbodies;
    }
    
    private Vector3 RandomizePosition(Vector3 position)
    {
        return new Vector3(
            Disperce(position.x), Disperce(position.y), Disperce(position.z)
            );
    }

    private float Disperce(float number)
    {
        return number + Random.Range(-_dispersion, _dispersion);
    }
}
