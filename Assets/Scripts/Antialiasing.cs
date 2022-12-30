using UnityEngine;
using UnityEngine.XR;

public class Antialiasing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XRSettings.eyeTextureResolutionScale = 2.0f;
    }
}
