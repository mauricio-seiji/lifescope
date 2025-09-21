using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GreenCoccus greenCoccusPrefab;
    [SerializeField] private RedCoccus redCoccusPrefab;
    [SerializeField] private PinkCoccus pinkCoccusPrefab;
    [SerializeField] private CellNucleus redCellNucleusPrefab;
    [SerializeField] private CellNucleus blueCellNucleusPrefab;
    [SerializeField] private CellNucleus greenCellNucleusPrefab;
    [SerializeField] private PinkCellNucleus pinkCellNucleusPrefab;
    [SerializeField] private int greenCoccusPopulationSize = 10;
    [SerializeField] private int redCoccusPopulationSize = 5;
    [SerializeField] private int redCellPopulationSize = 2;
    [SerializeField] private int blueCellPopulationSize = 2;
    [SerializeField] private int greenCellPopulationSize = 1;
    [SerializeField] private int pinkCellPopulationSize = 1;
    [SerializeField] private int pinkCoccusPopulationSize = 3;

    [SerializeField] private GameObject TutorialPanel;
    [SerializeField] private TMP_Text TutorialTMP;

    private string tutorialMessage;
    private int tutorialStage = 0;

    private int XPosition = 0;
    private int YPosition = 0;

    private void Start()
    {
        GlobalVariables.SCREENBOUNDS = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Screen.fullScreen = !Screen.fullScreen;

        CreatePopulation(greenCoccusPopulationSize, greenCoccusPrefab);
        CreatePopulation(greenCellPopulationSize, greenCellNucleusPrefab);

        if (GlobalVariables.ShowTutorial)
        {
            tutorialStage = 1;
            TutorialPanel.SetActive(true);
            tutorialMessage = "This is a relaxing microscopic life simulator\n";
            tutorialMessage += "Best enjoyed in full screen and with headphones";
            TutorialTMP.text = tutorialMessage;
        }
        else
        {
            CreatePopulation(redCoccusPopulationSize, redCoccusPrefab);
            CreatePopulation(redCellPopulationSize, redCellNucleusPrefab);
            CreatePopulation(blueCellPopulationSize, blueCellNucleusPrefab);
            CreatePopulation(pinkCellPopulationSize, pinkCellNucleusPrefab);
            CreatePopulation(pinkCoccusPopulationSize, pinkCoccusPrefab);
        }
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

    public void ShowTutorialSteps()
    {
        switch (tutorialStage)
        {
            case 1:
                tutorialStage = 2;
                tutorialMessage = "Single Photosynthetic Cells (translucent green)\n";
                tutorialMessage += "Gain energy through photosynthesis\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "May be eaten by Macrophage cells (red)";
                TutorialTMP.text = tutorialMessage;
                break;
            case 2:
                tutorialStage = 3;
                tutorialMessage = "Clustered Photosynthetic Cells (brilliant green) (immortal)\n";
                tutorialMessage += "Gain energy through photosynthesis\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "Clusters are immortal";
                TutorialTMP.text = tutorialMessage;
                break;
            case 3:
                tutorialStage = 4;
                tutorialMessage = "Clustered Peacekeeper Cells (brilliant blue) (immortal)\n";
                tutorialMessage += "May be used as a protection for photosynthetic cells (green)\n";
                tutorialMessage += "Clusters are immortal";
                TutorialTMP.text = tutorialMessage;
                break;
            case 4:
                tutorialStage = 5;
                CreatePopulation(redCoccusPopulationSize, redCoccusPrefab);
                CreatePopulation(redCellPopulationSize, redCellNucleusPrefab);
                tutorialMessage = "Single Macrophage Cells (translucent red)\n";
                tutorialMessage += "Gain energy by eating photosynthetic cells (green)\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "May die of starvation";
                TutorialTMP.text = tutorialMessage;
                break;
            case 5:
                tutorialStage = 6;
                tutorialMessage = "Clustered Macrophage Cells (brilliant red) (immortal)\n";
                tutorialMessage += "Gain energy by eating photosynthetic cells (green)\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "Clusters are immortal";
                TutorialTMP.text = tutorialMessage;
                break;
            case 6:
                tutorialStage = 7;
                CreatePopulation(pinkCellPopulationSize, pinkCellNucleusPrefab);
                CreatePopulation(pinkCoccusPopulationSize, pinkCoccusPrefab);
                tutorialMessage = "Single Phagocytic Cells (translucent pink)\n";
                tutorialMessage += "Gain energy by eating excretion (black)\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "May see an excretion and move towards it";
                TutorialTMP.text = tutorialMessage;
                break;
            case 7:
                tutorialStage = 8;
                tutorialMessage = "Clustered Phagocytic Cells (brilliant pink) (immortal)\n";
                tutorialMessage += "Gain energy by eating excretion (black)\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "May see an excretion and move towards it";
                TutorialTMP.text = tutorialMessage;
                break;
            case 8:
                tutorialStage = 9;
                TutorialPanel.SetActive(false);
                break;
            default:
                break;
        }
    }
}
