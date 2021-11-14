using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    public float holep;
    public int w, h, x, y;
    public bool[,] hwalls, vwalls;
    public Transform Level, Player, Goal;
    public GameObject Floor, Wall;

    void Start()
    {
        hwalls = new bool[w+1, h];
        vwalls = new bool[w, h+1];
        var st = new int[w, h];

        void dfs(int x, int y)
        {
            st[x, y] = 1;
            Instantiate(Floor, new Vector3(x, y), Quaternion.identity, Level);

            var dirs = new[]
            {
                (x - 1, y, hwalls, x, y, Vector3.right, 90, KeyCode.A),
                (x + 1, y, hwalls, x + 1, y, Vector3.right, 90, KeyCode.D),
                (x, y - 1, vwalls, x, y, Vector3.up, 0, KeyCode.S),
                (x, y + 1, vwalls, x, y + 1, Vector3.up, 0, KeyCode.W)
            };
            foreach (var (nextX, nextY, wall, wallX, wallY, sh, angle, key) in dirs.OrderBy(d => Random.value))
            {
                if (!(0 <= nextX && nextX < w && 0 <= nextY && nextY < h) || (st[nextX, nextY] == 2 && Random.value > holep))
                {
                    wall[wallX, wallY] = true;
                    Instantiate(Wall, new Vector3(wallX, wallY) - sh / 2, Quaternion.Euler(0, 0, angle), Level);
                }
                else if (st[nextX, nextY] == 0)
                    dfs(nextX, nextY);
            }
            st[x, y] = 2;
        }
        dfs(0, 0);

        x = Random.Range(0, w);
        y = Random.Range(0, h);
        Player.position = new Vector3(x, y);
        do Goal.position = new Vector3(Random.Range(0, w), Random.Range(0, h));
        while (Vector3.Distance(Player.position, Goal.position) < (w+h) / 4);

    }
}