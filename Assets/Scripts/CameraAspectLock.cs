using UnityEngine;
 
public class CameraAspectLock : MonoBehaviour {
 
    void Start() {
        myCamera.GetComponent<Camera>();
    }
 
    Camera myCamera;
    float desiredAspect = 9/19f;
 
    void Update() {
        float screenAspect = (float)Screen.width / (float)Screen.height;
 
        float ratio = screenAspect / desiredAspect;
 
        float margin = Mathf.Abs(ratio - 1f) * 0.5f;
 
        if (ratio > 1f) { // screen is wider
            margin /= ratio;
            Camera.main.rect = new Rect(margin, 0.0f, 1.0f - margin * 2.0f, 1.0f);
        }
        else {
            Camera.main.rect = new Rect(0.0f, margin, 1.0f, 1.0f - margin * 2.0f);
        }
    }
}