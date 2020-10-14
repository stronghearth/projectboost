using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //prevents accidentally adding more than one copy of script on a GameObject
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Vector3 movementVector;

    // todo remove from inspector
    [Range(0, 1)]
    [SerializeField]
    float moventFactor; // 0 for not moved, 1 for fully moves

    Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * moventFactor;
        transform.position = startingPosition + offset;
    }
}
