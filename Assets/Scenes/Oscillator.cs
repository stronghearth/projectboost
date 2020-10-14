using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //prevents accidentally adding more than one copy of script on a GameObject
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    // todo remove from inspector
    [Range(0, 1)]
    [SerializeField]
    float moventFactor; // 0 for not moved, 1 for fully moved

    Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; } //protect against period = 0

        float cycles = Time.time / period; //grows continually from 0 the longer game play time lasts

        const float tau = Mathf.PI * 2f; // about 6.28

        float rawSin = Mathf.Sin(cycles * tau);

        moventFactor = rawSin / 2f + 0.5f;
        Vector3 offset = movementVector * moventFactor;
        transform.position = startingPosition + offset;
    }
}
