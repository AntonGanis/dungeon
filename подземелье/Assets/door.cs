using UnityEngine;

public class door : MonoBehaviour
{
    private GameObject objectToRotate;
    private bool shouldRotate = false;
    private float rotationStartTime;
    private const float rotationDuration = 1.0f;
    private const float maxRotationAngle = 75.0f;

    private float initialYRotation;
    private float currentRotationAngle = 0.0f;

    public bool end;

    private void Start()
    {
        initialYRotation = transform.eulerAngles.y;
        currentRotationAngle = 0.0f; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (end == false)
        {
            if (other.gameObject.GetComponent<Move>() != null)
            {
                PlarOpen(other.transform);
            }
        }
        else
        {
            if (other.gameObject.GetComponent<door>() || other.gameObject.GetComponent<FakeWall>())
            {
                Destroy(other.gameObject);
            }
            else if (other.gameObject.GetComponent<Move>() && other.gameObject.GetComponent<Move>().key)
            {
                PlarOpen(other.transform);
            }
        }
    }
    void PlarOpen(Transform other)
    {
        Vector3 directionToOther = other.position - transform.position;
        float dotProduct = Vector3.Dot(transform.forward, directionToOther);

        objectToRotate = other.gameObject;
        shouldRotate = true;
        rotationStartTime = Time.time;
    }
    private void Update()
    {
        if (shouldRotate)
        {
            if (Time.time - rotationStartTime < rotationDuration)
            {
                float rotationSpeed = 15f;
                Vector3 directionToOther = objectToRotate.transform.position - transform.position;
                float dotProduct = Vector3.Dot(transform.forward, directionToOther);

                float rotationAmount = rotationSpeed * Time.deltaTime;
                if (dotProduct > 0)
                {
                    currentRotationAngle -= rotationAmount;
                }
                else
                {
                    currentRotationAngle += rotationAmount;
                }

                float targetAngle = Mathf.Clamp(initialYRotation + currentRotationAngle, initialYRotation - maxRotationAngle, initialYRotation + maxRotationAngle);

                transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            }
            else
            {
                shouldRotate = false;
            }
        }
    }
}
