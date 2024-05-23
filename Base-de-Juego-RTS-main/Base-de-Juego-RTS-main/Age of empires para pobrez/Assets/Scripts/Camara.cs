using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float rotationSpeed = 5.0f;

    private bool isRotating = false;

    void Update()
    {
        HandleMovementInput();
        HandleRotationInput();
    }

    void HandleMovementInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.Self); // Cambio aquí para mover relativo a la cámara
    }

    void HandleRotationInput()
    {
        if (Input.GetMouseButtonDown(2)) // Middle mouse button
        {
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetMouseButtonUp(2))
        {
            isRotating = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (isRotating)
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.up, rotationX, Space.World);
            transform.Rotate(Vector3.left, rotationY, Space.Self);
        }
    }
}
