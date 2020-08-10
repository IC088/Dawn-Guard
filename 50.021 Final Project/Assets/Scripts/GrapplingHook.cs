using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public DistanceJoint2D joint;
    public LineRenderer line;
    Vector3 targetPos;
    RaycastHit2D hit;

    private float distance = 10f;
    public LayerMask mask;
    private float step = 0.2f;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (joint.distance > 1f)
        { joint.distance -= step; }
        else
        { line.enabled = false;
            joint.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            hit = Physics2D.Raycast(transform.position, targetPos - transform.position,distance, mask);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>()!= null)
            {
                line.enabled = true;
                anim.SetBool("Jump", true);
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);
                Debug.Log(hit.collider.gameObject.GetComponent<Rigidbody2D>());

                line.SetPosition(0, transform.position);

                line.SetPosition(1, hit.point);


                /*
                 * Add delay
                 */
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            line.SetPosition(0, transform.position);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            joint.enabled = false;
            line.enabled = false;
        }
    }
}
