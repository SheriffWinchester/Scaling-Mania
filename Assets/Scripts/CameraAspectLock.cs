using UnityEngine;
 
public class CameraAspectLock : MonoBehaviour {
    Camera myCamera;
    float desiredAspect = 9/19f;
    void Start() {
        myCamera.GetComponent<Camera>();
    }
    void Update()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float ratio = screenAspect / desiredAspect;
        float margin = Mathf.Abs(ratio - 1f) * 0.5f;

        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        if (ratio > 1f) { // screen is wider
            margin /= ratio;
            Camera.main.rect = new Rect(margin, 0.0f, 1.0f - margin * 2.0f, 1.0f);//Some fucking magic i don't understand with the camera aspect ratio
            collider.size = new Vector2(Camera.main.orthographicSize * 4.2f * Camera.main.aspect, Camera.main.orthographicSize * 4.2f);//Resize the camera's box collider. 4.2 is a deliberate number, picked by trial
        }
        else {
            Camera.main.rect = new Rect(0.0f, margin, 1.0f, 1.0f - margin * 2.0f);
            collider.size = new Vector2(Camera.main.orthographicSize * 4.2f * Camera.main.aspect, Camera.main.orthographicSize * 4.2f);//Resize the camera's box collider
        }
    }
}