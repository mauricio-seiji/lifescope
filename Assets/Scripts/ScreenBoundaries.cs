using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        if (GlobalVariables.SCREENBOUNDS != null)
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, -GlobalVariables.SCREENBOUNDS.x, GlobalVariables.SCREENBOUNDS.x);
            viewPos.y = Mathf.Clamp(viewPos.y, -GlobalVariables.SCREENBOUNDS.y, GlobalVariables.SCREENBOUNDS.y);
            transform.position = viewPos;
        }
    }
}
