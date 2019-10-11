using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    public GunController theGun;

    public int selectedTool = 0;
       
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();

        SelectTool();
    }
    void SelectTool ()
    {
        int i = 0;
        foreach (Transform tool in transform)
        {
            if (i == selectedTool)
                tool.gameObject.SetActive(true);
            else
                tool.gameObject.SetActive(false);
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if(Input.GetMouseButtonDown(0))
            theGun.isFiring = true;
        if (Input.GetMouseButtonUp(0))
            theGun.isFiring = false;

        int previousSelectedTool = selectedTool;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedTool >= transform.childCount - 1)
                selectedTool = 0;
            else
                selectedTool++;
                    }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedTool <= 0)
                selectedTool = transform.childCount - 1;
            else
                selectedTool--;
        }
        if (previousSelectedTool != selectedTool)
        {
            SelectTool();
        }
    }

    
     void FixedUpdate() {
        myRigidbody.velocity = moveVelocity;
    }

}
