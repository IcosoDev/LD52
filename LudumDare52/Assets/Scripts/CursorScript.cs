using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position += new Vector3(0, 0, 10);
    }
}
