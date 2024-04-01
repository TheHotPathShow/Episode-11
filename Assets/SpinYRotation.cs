using UnityEngine;

public class SpinYRotation : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float waveSpeed = 1;
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(Mathf.Sin(Time.time*waveSpeed)*5, speed * Time.time, 0);
    }
}
