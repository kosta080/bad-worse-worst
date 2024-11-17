using UnityEngine;

public class ScriptWithUpdateLoop : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(10,10,10) * Time.deltaTime);
    }
}
