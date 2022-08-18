using UnityEngine;

public class ImageRotate : MonoBehaviour
{
    private RectTransform rec_image;
    private Vector3 vec_axis;
    
    // Start is called before the first frame update
    void Start()
    {
        if (rec_image == null)
        {
            rec_image = gameObject.GetComponent<RectTransform>();
        }
        vec_axis = new Vector3(0, 0, -2f);
    }

    // Update is called once per frame
    void Update()
    {
        rec_image.Rotate(vec_axis);
    }
}
