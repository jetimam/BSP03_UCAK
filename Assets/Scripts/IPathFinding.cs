using System.Collections.Generic;
using UnityEngine;

public interface IPathFinding
{
    public List<Vector3> Search(Vector3 startingPosition, Vector3 destination);

    public List<Vector3> GenerateChildren(Vector3 parent);

    public List<Vector3> BackTrack(Vector3 cell);
}