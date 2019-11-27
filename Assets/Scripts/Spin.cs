using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float SpinSpeed;

    private void Start()
    {
        StartSpinning();
    }

    public void StartSpinning()
    {
        StartCoroutine(SpinSelf());
    }

    private IEnumerator SpinSelf()
    {
        while (true)
        {
            transform.Rotate(Vector3.up * SpinSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
