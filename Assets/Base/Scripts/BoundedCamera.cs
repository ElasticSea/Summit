using System;
using System.Linq;
using Assets.Shared.Scripts;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

[ExecuteInEditMode]
public class BoundedCamera : MonoBehaviour
{
    public Transform Ground;
    public Rect? Bounds;
    public Vector2 MinimumCameraRect;

    private Vector3 validCameraPosition;
    private bool max;

    private void LateUpdate()
    {
        if (Ground == null || Bounds == null)
            throw new InvalidOperationException("Ground or Bounds are not set. Script will not be executed.");

        var cameraRect = GetGroundFrustumAABB();
        if (cameraRect.width < MinimumCameraRect.x || cameraRect.height < MinimumCameraRect.y)
        {
            transform.position = validCameraPosition;
        }
        else if (cameraRect.width > Bounds.Value.width)
        {
            CameraTooFarVertical();
            AlignVertically(cameraRect);
        }
        else if (cameraRect.height > Bounds.Value.height)
        {
            CameraTooFarHorizontal();
            AlignHorizontally(cameraRect);
        }
        else
        {
            max = false;
            AlignHorizontally(cameraRect);
            AlignVertically(cameraRect);
        }

        validCameraPosition = transform.position;
    }

    private void CameraTooFarHorizontal()
    {
        if (max)
        {
            if (transform.position.y > validCameraPosition.y)
            {
                transform.SetX(validCameraPosition.x);
                transform.SetY(validCameraPosition.y);
            }

            Debug.Assert(Bounds != null, "Bounds != null");
            transform.SetZ(Bounds.Value.center.y);
        }
        else
        {
            max = true;
        }
    }

    private void CameraTooFarVertical()
    {
        if (max)
        {
            if (transform.position.y > validCameraPosition.y)
            {
                transform.SetZ(validCameraPosition.z);
                transform.SetY(validCameraPosition.y);
            }

            transform.SetX(Bounds.Value.center.x);
        }
        else
        {
            max = true;
        }
    }

    private void AlignVertically(Rect cameraRect)
    {
        if (cameraRect.yMin < Bounds.Value.yMin)
            transform.AddZ(Bounds.Value.yMin - cameraRect.yMin);
        else if (cameraRect.yMax > Bounds.Value.yMax)
            transform.AddZ(Bounds.Value.yMax - cameraRect.yMax);
    }

    private void AlignHorizontally(Rect cameraRect)
    {
        if (cameraRect.xMin < Bounds.Value.xMin)
            transform.AddX(Bounds.Value.xMin - cameraRect.xMin);
        else if (cameraRect.xMax > Bounds.Value.xMax)
            transform.AddX(Bounds.Value.xMax - cameraRect.xMax);
    }

    private Rect GetGroundFrustumAABB()
    {
        var validPoints = GetGroundFrustum().Where(p => p.HasValue).Select(p => p.Value);

        var groundFrustumAABB = new Rect(validPoints.Center().FromXZ(), Vector2.zero);
        foreach (var point in validPoints)
        {
            var point2D = point.FromXZ();
            groundFrustumAABB.xMax = Mathf.Max(groundFrustumAABB.xMax, point2D.x);
            groundFrustumAABB.yMax = Mathf.Max(groundFrustumAABB.yMax, point2D.y);
            groundFrustumAABB.xMin = Mathf.Min(groundFrustumAABB.xMin, point2D.x);
            groundFrustumAABB.yMin = Mathf.Min(groundFrustumAABB.yMin, point2D.y);
        }
        return groundFrustumAABB;
    }

    private Vector3?[] GetGroundFrustum()
    {
        var plane = new Plane(Vector3.up, Ground.position);
        var cam = GetComponent<Camera>();

        var groundFrustum = new Vector3?[4];
        groundFrustum[0] = cam.PointOnGround(new Vector2(0, 0), plane);
        groundFrustum[1] = cam.PointOnGround(new Vector2(cam.pixelWidth, 0), plane);
        groundFrustum[2] = cam.PointOnGround(new Vector2(cam.pixelWidth, cam.pixelHeight), plane);
        groundFrustum[3] = cam.PointOnGround(new Vector2(0, cam.pixelHeight), plane);
        return groundFrustum;
    }

    private void OnDrawGizmosSelected()
    {
        DrawGroundFrustum();
        DrawGroundFrustumAABB();
        DrawGroundBounds();
    }

    private void DrawGroundFrustum()
    {
        Gizmos.color = Color.red;
        var groundFrustum = GetGroundFrustum();
        for (var i = 0; i < groundFrustum.Length; i++)
        {
            var a = groundFrustum[i];
            var b = groundFrustum[(i + 1)%groundFrustum.Length];
            if (a.HasValue && b.HasValue)
                Gizmos.DrawLine(a.Value, b.Value);
        }
    }

    private void DrawGroundFrustumAABB()
    {
        Gizmos.color = Color.blue;
        var groundFrustumAABB = GetGroundFrustumAABB();
        Gizmos.DrawWireCube(groundFrustumAABB.center.ToXZ(), groundFrustumAABB.size.ToXZ());
    }

    private void DrawGroundBounds()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Bounds.Value.center.ToXZ(), Bounds.Value.size.ToXZ());
    }
}