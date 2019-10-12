using System.Collections.Generic;
using UnityEngine;
public class TrajectoryRenderer : MonoBehaviour
{
    [Header("Requirements")]
    public LineRenderer lineRenderer;
    [Header("Things Needed")]
    public Vector3 startingPos = Vector3.zero;
    public float accelerationDueToGravity = 1f;
    public float initialialVelocity = 1f;
    public float angleOfInitVelocityFromHorizontalPos = 45f;
    [Header("Others")]
    public bool enableDebugMode = false;
    public void SetTrajectory(LineRenderer lineRenderer,Vector3 startingPosition, float accelerationDueToGravity, float initialialVelocity, float angleOfInitVelocityFromHorizontalPos){
        //this method will set the trajectory things
        this.lineRenderer = lineRenderer;
        this.startingPos = startingPosition;
        this.accelerationDueToGravity = accelerationDueToGravity;
        this.initialialVelocity = initialialVelocity;
        this.angleOfInitVelocityFromHorizontalPos = angleOfInitVelocityFromHorizontalPos;
        Debug.Log("Setted Trajectory!");
    }
    public void DrawTrajectoryArc(float lineLength, float xAddStep)
    {
        /*
            To Do:
            1. clear the lineRenderer
            2. make a positions of points for the linerenderer
            3. and apply the things inside
        */
        //make a list of vars
        List<Vector3> positions = new List<Vector3>();
        //clear
        lineRenderer.positionCount = 0;
        //this method will draw the trajectory arc using the variables
        for (float x = 0; x < lineLength; x += xAddStep)
        {
            //inside this for loop we will use the line renderer's things
            float FormulaLeft = x * Mathf.Tan(angleOfInitVelocityFromHorizontalPos);
            float FormulaRightUp = accelerationDueToGravity * x * x;
            float FormulaRightDown = 2 * initialialVelocity * initialialVelocity * (1 - Mathf.Cos(2 * angleOfInitVelocityFromHorizontalPos) / 2);
            float FormulaRight = FormulaRightUp / FormulaRightDown;
            float newY = FormulaLeft - FormulaRight;

            positions.Add(startingPos + new Vector3(x, newY));
        }
        //when finishing calculating // add the things to the linerenderer
        Vector3[] FinPoses = positions.ToArray();
        //apply
        lineRenderer.positionCount = FinPoses.Length;
        lineRenderer.SetPositions(FinPoses);
        //debug log
        if (enableDebugMode)
        {
            Debug.Log("Finish!");
        }
    }
}
