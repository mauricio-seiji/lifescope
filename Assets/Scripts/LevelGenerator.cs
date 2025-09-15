using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GreenCoccus greenCoccusPrefab;
    [SerializeField] private RedCoccus redCoccusPrefab;
    [SerializeField] private CellNucleus redCellNucleusPrefab;
    [SerializeField] private CellNucleus blueCellNucleusPrefab;
    [SerializeField] private StreptococcusNucleus RedstreptococcusNucleus;
    [SerializeField] private int greenCoccusPopulationSize = 10;
    [SerializeField] private int redCoccusPopulationSize = 5;
    [SerializeField] private int redCellPopulationSize = 2;
    [SerializeField] private int blueCellPopulationSize = 2;
    [SerializeField] private int redStreptococcusPopulationSize = 3;

    private int XPosition = 0;
    private int YPosition = 0;

    private void Start()
    {
        GlobalVariables.SCREENBOUNDS = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        CreatePopulation(greenCoccusPopulationSize, greenCoccusPrefab);
        CreatePopulation(redCoccusPopulationSize, redCoccusPrefab);
        CreatePopulation(redCellPopulationSize, redCellNucleusPrefab);
        CreatePopulation(blueCellPopulationSize, blueCellNucleusPrefab);
        CreatePopulation(redStreptococcusPopulationSize, RedstreptococcusNucleus);
    }

    void CreateRandomPosition()
    {   
        XPosition = Random.Range((int)-GlobalVariables.SCREENBOUNDS.x, (int)GlobalVariables.SCREENBOUNDS.x);
        YPosition = Random.Range((int)-GlobalVariables.SCREENBOUNDS.y, (int)GlobalVariables.SCREENBOUNDS.y);
    }

    void CreatePopulation<T>(int populationSize, T prefab) where T : MonoBehaviour
    { 
        for (int i = 0; i < populationSize; i++)
        {
            CreateRandomPosition();
            Instantiate(prefab, new Vector3(XPosition, YPosition, 0), Quaternion.identity);
        }
    }
}
