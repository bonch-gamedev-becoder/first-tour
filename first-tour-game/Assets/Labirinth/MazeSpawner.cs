using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public static MazeSpawner instance;

    public AStar aStar;

    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);
    public HintRenderer HintRenderer;

    public Maze maze;

    private void Start()
    {
        instance = this;

        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);
                c.WallLeftBreak.SetActive(false);
                c.WallBottomBreak.SetActive(false);
                int randnum = Random.Range(0, 6);

                if (randnum == 1)
                    c.WallLeftBreak.SetActive(maze.cells[x, y].WallLeft);

                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);


                if (randnum == 2)
                    c.WallBottomBreak.SetActive(maze.cells[x, y].WallBottom);

                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);


                if (c.WallLeftBreak.active)
                    c.WallLeft.SetActive(false);
                if (c.WallBottomBreak.active)
                    c.WallBottom.SetActive(false);
            }
        }

        if (HintRenderer != null)
            HintRenderer.DrawPath();
    }
}