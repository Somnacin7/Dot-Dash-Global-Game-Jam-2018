using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour {

    public delegate void SplineDone();
    public static event SplineDone OnSplineDone;

    public BezierSpline spline;

    public float duration;
    public bool lookForward;
    private  bool finished = false;

    private float progress;

    private void Update()
    {
        if (!finished && spline != null)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                progress = 1f;
                finished = true;
                if (OnSplineDone != null)
                {
                    OnSplineDone();
                }
            }
            Vector3 pos = spline.GetPoint(progress);
            transform.localPosition = pos;
            if (lookForward)
            {
                transform.LookAt(pos + spline.GetDirection(progress));
            }
        }
    }

    public void ResetSpline()
    {
        progress = 0f;
        finished = false;
    }
}
