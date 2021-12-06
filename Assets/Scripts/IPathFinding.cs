using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinding
{
    public void Search();

    public Vector3 GetDestination(Vector3 startingPosition);

    public Vector3 Update(Vector3 startingPosition);
}