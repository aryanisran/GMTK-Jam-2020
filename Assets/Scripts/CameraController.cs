using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target  ;
    public float followAhead;
    //public float look; // define distance to look up and down, can be changed in Unity

    private Vector3 targetPosition;
    private float targetX;
    private float targetY;

    public float smoothing;

    public bool followTarget;

    //private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        followTarget = true;
        //audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            if (Input.GetAxisRaw("Vertical") > 0) // if player presses W or Up, look up
            {
                targetY = target.transform.position.y;
            }

            else if (Input.GetAxisRaw("Vertical") < 0) // if player presses S or Down, look Down
            {
                targetY = target.transform.position.y;
            }

            else
            {
                targetY = target.transform.position.y;
            }

            if (target.transform.localScale.x > 0.0f)
            {
                targetX = target.transform.position.x;
                //targetPosition = new Vector3(target.transform.position.x + followAhead, target.transform.position.y, transform.position.z);
            }
            else
            {
                targetX = target.transform.position.x;
                //targetPosition = new Vector3(target.transform.position.x - followAhead, target.transform.position.y, transform.position.z);
            }

            targetPosition = new Vector3(targetX, targetY, transform.position.z);
            //transform.position = targetPosition;

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
