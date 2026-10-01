using Unity.VisualScripting;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public GameObject cube;
    public Material redMaterial;
    public Material blueMaterial;

    void Start()
    {
        _ = ChageColorAfterDelay();


    }

    void Update()
    {
        cube.transform.Rotate(Vector3.up, 45f * Time.deltaTime);

    }

    async Awaitable ChageColorAfterDelay()
    {
        cube.GetComponent<Renderer>().material = redMaterial;
        await Awaitable.WaitForSecondsAsync(3f);
        cube.GetComponent<Renderer>().material = blueMaterial;
        await Awaitable.WaitForSecondsAsync(3f);
        cube.GetComponent<Renderer>().material = redMaterial;
    }
}
