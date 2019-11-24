using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Vector3 OpenPosition = new Vector3(-1.5f, 0f, 0f);
    [SerializeField] private float OpenTime = 0.5f;

    public void Open()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 startingPos = transform.position;
        Vector3 destination = transform.position + OpenPosition;
        float elapsedTime = 0f;
        while (elapsedTime < OpenTime)
        {
            transform.position = Vector3.Lerp(startingPos, destination, elapsedTime / OpenTime);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        transform.position = destination;
    }


}
