using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [Header("Turrent")]
    public Transform turrentMesh;
    public float rotateTurrentTime = 5.0f;

    [Header("Tank")]
    public float rotateTankTime = 4.0f;
    public float moveTankTime = 5.0f;

    private Vector3 rayPosition;
    private float vertical, mouseX;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void GetAxis()
    {
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
    }

    private void GetRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {            
            //if (!hit.transform.CompareTag("Player"))
            rayPosition = hit.point;
        }
    }

    private void RotateTurrent()
    {
        Vector3 p = rayPosition;
        p.y = turrentMesh.position.y;
        p = p - turrentMesh.position;

        Quaternion rot = Quaternion.LookRotation(p, Vector3.up);
        turrentMesh.transform.rotation = Quaternion.Slerp(turrentMesh.transform.rotation,
        rot,
        rotateTurrentTime * Time.deltaTime);
    }

    private void RotateTank()
    {
        if (vertical <= 0)
            return;

        Vector3 p = rayPosition;
        p.y = this.transform.position.y;
        p = p - this.transform.position;

        Quaternion rot = Quaternion.LookRotation(p, Vector3.up);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        rot,
        rotateTankTime * Time.deltaTime);
    }

    public void Move()
    {
        GetAxis();
        GetRay();
        RotateTurrent();
        RotateTank();

        Vector3 pos = Vector3.forward * (vertical * moveTankTime) * Time.deltaTime;
        transform.Translate(pos);
    }
}