using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCanvas : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
    }
}
