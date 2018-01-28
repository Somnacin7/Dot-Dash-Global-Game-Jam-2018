using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour {

    public BezierSpline spline;

    public float duration;
    public bool lookForward;

    private float progress;

    private void Update()
    {
        progress += Time.deltaTime / duration;
        if (progress > 1f)
        {
            progress = 1f;
        }
        Vector3 pos = spline.GetPoint(progress);
        transform.localPosition = pos;
        if (lookForward)
        {
            transform.LookAt(pos + spline.GetDirection(progress));
        }
    }
}
