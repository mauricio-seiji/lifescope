using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Timeline.DirectorControlPlayable;

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

    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text tutorialTitleTMP;
    [SerializeField] private TMP_Text tutorialBodyTMP;

    public static int cellsCount = 0;

    private string tutorialMessage;
    private int tutorialStage = 0;

    private int XPosition = 0;
    private int YPosition = 0;
    
    private bool isPaused = false;
    private InputAction pauseAction;

    private void Awake()
    {
        pauseAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        pauseAction.performed += ctx => TogglePause();
    }
    private void Start()
    {
        GlobalVariables.SCREENBOUNDS = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Screen.fullScreen = true;

        CreatePopulation(greenCoccusPopulationSize, greenCoccusPrefab);
        CreatePopulation(greenCellPopulationSize, greenCellNucleusPrefab);

        if (GlobalVariables.showTutorial)
        {
            tutorialStage = 1;
            tutorialPanel.SetActive(true);
            tutorialMessage = "This is a relaxing microscopic life simulator\n";
            tutorialMessage += "Best enjoyed in full screen and with headphones";
            tutorialTitleTMP.text = tutorialMessage;
        }
        else
        {
            CreatePopulation(greenCoccusPopulationSize, greenCoccusPrefab);
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
            Vector3 randomPosition = new Vector3(XPosition, YPosition, 0);
            if (prefab is Coccus coccusPrefab)
                coccusPrefab.CreateCoccus(prefab.gameObject, randomPosition);
            else if (prefab is CellNucleus cellNucleusPrefab)
                Instantiate(cellNucleusPrefab, randomPosition, Quaternion.identity);
        }
    }

    public void ShowTutorialSteps()
    {
        switch (tutorialStage)
        {
            case 1:
                tutorialStage = 2;
                tutorialTitleTMP.text = "Single Autotroph Cells [mortal] [green]";
                tutorialMessage = "Gain energy through photosynthesis\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "May be eaten by Macrophage cells [red]";
                tutorialBodyTMP.text = tutorialMessage;
                break;
            case 2:
                tutorialStage = 3;
                tutorialTitleTMP.text = "Clustered Autotroph Cells [immortal] [green]";
                tutorialMessage = "Gain energy through photosynthesis\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "Clusters are immortal";
                tutorialBodyTMP.text = tutorialMessage;
                break;
            case 3:
                tutorialStage = 4;
                tutorialTitleTMP.text = "Clustered Peacekeeper Cells [immortal] [blue]";
                tutorialMessage = "May be used as a protection for Autotroph cells [green[\n";
                tutorialMessage += "Clusters are immortal";
                tutorialBodyTMP.text = tutorialMessage;
                break;
            case 4:
                tutorialStage = 5;
                CreatePopulation(redCoccusPopulationSize, redCoccusPrefab);
                CreatePopulation(redCellPopulationSize, redCellNucleusPrefab);
                tutorialTitleTMP.text = "Single Macrophage Cells [mortal] [red]";
                tutorialMessage = "Gain energy by eating Autotroph cells [green]\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "May die from starvation";
                tutorialBodyTMP.text = tutorialMessage;
                break;
            case 5:
                tutorialStage = 6;
                tutorialTitleTMP.text = "Clustered Macrophage Cells [immortal] [red]";
                tutorialMessage = "Gain energy by eating Autotroph cells [green]\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "Clusters are immortal";
                tutorialBodyTMP.text = tutorialMessage;
                break;
            case 6:
                tutorialStage = 7;
                CreatePopulation(pinkCellPopulationSize, pinkCellNucleusPrefab);
                CreatePopulation(pinkCoccusPopulationSize, pinkCoccusPrefab);
                tutorialTitleTMP.text = "Single Phagocytic Cells [mortal] [pink]";
                tutorialMessage = "Gain energy by eating excretion [black]\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "May see an excretion and move towards it";
                tutorialBodyTMP.text = tutorialMessage;
                break;
            case 7:
                tutorialStage = 8;
                tutorialTitleTMP.text = "Clustered Phagocytic Cells [immortal] [pink]";
                tutorialMessage = "Gain energy by eating excretion [black]\n";
                tutorialMessage += "May reproduce when they get enough energy\n";
                tutorialMessage += "May see an excretion and move towards it";
                tutorialBodyTMP.text = tutorialMessage;
                break;
            case 8:
                tutorialStage = 9;
                tutorialBodyTMP.text = "";
                tutorialMessage = "If you left click some cells they might reproduce\n";
                tutorialMessage += "If there are too many cells, reprodution will stop for a while";
                tutorialTitleTMP.text = tutorialMessage;
                break;
            case 9:
                tutorialStage = 10;
                tutorialPanel.SetActive(false);
                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        pauseAction.Enable();
    }
    private void OnDisable()
    {
        pauseAction.Disable();
    }
    private void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}
