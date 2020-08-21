using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        transform.position += new Vector3(dx * speed * Time.deltaTime, 0, dz * speed * Time.deltaTime);

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out hit,LayerMask.GetMask("Floor")))
        {
            Vector3 lookPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPoint, Vector3.up);
        }
    }
}
