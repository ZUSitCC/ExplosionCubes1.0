using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class InputHandler : MonoBehaviour
{
    [SerializeField, Range(1, 6)] private int _countClonesMin = 2;
    [SerializeField, Range(1, 6)] private int _countClonesMax = 6;

    private Spawner _spawner;

    private Exploder _exploder;

    private float _chanceToSplit = 1;

    public void SetSplitChance(float splitChance)
    {
        _chanceToSplit = splitChance;
    }

    private void OnValidate()
    {
        if (_countClonesMin >= _countClonesMax)
            _countClonesMax = _countClonesMax - 1;
    }

    private void OnMouseUpAsButton()
    {
        int clonesCount = Random.Range(_countClonesMin, _countClonesMax + 1);

        _spawner = GetComponent<Spawner>();
        _exploder = GetComponent<Exploder>();

        if (_chanceToSplit >= Random.value)
        {
            _spawner.Spawn(clonesCount, _chanceToSplit);
            _exploder.Explode(_spawner.GetExplodibleObjects(), transform.position);
        }

        Destroy(gameObject);
    }
}
