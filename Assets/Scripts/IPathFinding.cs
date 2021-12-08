using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinding
{
    public List<Vector3> GetPath(Vector3 startingPosition);

    public Vector3 GetDestination(Vector3 startingPosition);
}