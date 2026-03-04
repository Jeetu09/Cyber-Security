using UnityEngine;

public class CameraCotroller : MonoBehaviour
{
    public Transform Targate;
    public float SmoothSpeed = 8f;
    public Vector3 offset;

    void Update()
    {
        if (Targate == null) return;
        Vector3 desiredPosition = new Vector3(Targate.position.x + offset.x, Targate.position.y + offset.y, Targate.position.z + offset.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }

}
