using UnityEngine;
using TMPro;

public class VillageInformation : MonoBehaviour
{
    public GameObject panelVillager;
    public TMP_Text NameVillagerTXT; // reference to the TextMeshProUGUI text field
    public TMP_Text ProfessionTXT; // reference to the TextMeshProUGUI text field

    public GameObject panel; // reference to the panel game object
    public TMP_Text NameTXT; // reference to the TextMeshProUGUI text field
    public TMP_Text PopulationTXT; // reference to the TextMeshProUGUI text field
    public TMP_Text MoneyTXT; // reference to the TextMeshProUGUI text field

    private float panelDisplayDuration = 1.0f; // How long the panel should be displayed
    private float panelHideTimeVillager ; // Time to hide the villager panel
    private float panelHideTimeVillage ; // Time to hide the village panel
    private bool isVillagerPanelVisible; // Flag to track if the villager panel is visible
    private bool isVillagePanelVisible; // Flag to track if the village panel is visible

    private void Update()
    {
        // Create a ray that shoots from the middle of the camera view
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Check if the raycast hits anything
        if (Physics.Raycast(ray, out hit))
        {
            InformationOfVillage parentVillageInfo = hit.collider.gameObject.GetComponentInParent<InformationOfVillage>();
            Information parentInfo = hit.collider.gameObject.GetComponentInParent<Information>();

            if (parentVillageInfo != null)
            {   
                isVillagePanelVisible = true;
                panel.SetActive(true);

                string Name = parentVillageInfo.GetInformationName();
                NameTXT.text = Name;
                int Population = parentVillageInfo.GetInformationPopulation();
                PopulationTXT.text = "Population: " + Population;
                int Money = parentVillageInfo.GetInformationMoney();
                MoneyTXT.text = "Money: " + Money;

                
                panelHideTimeVillage = Time.time + panelDisplayDuration;
            }
            else
            {
                isVillagePanelVisible = false;// If the parent InformationOfVillage component is not found, hide the panel
                if (!isVillagePanelVisible && Time.time > panelHideTimeVillage)
                {
                    panel.SetActive(false);
                }
                
            }

            if (parentInfo != null)
            {
                isVillagerPanelVisible = true;
                panelVillager.SetActive(true);
                string NameVillager = parentInfo.GetInformationName();
                NameVillagerTXT.text = NameVillager;
                string ProfessionName = parentInfo.GetInformationProfession();
                ProfessionTXT.text = ProfessionName;

                
                panelHideTimeVillager = Time.time + panelDisplayDuration;
            }
            else
            {
                // If the parent Information component is not found, hide the panel
                
                isVillagerPanelVisible = false;
                if (!isVillagerPanelVisible && Time.time > panelHideTimeVillager)
                {
                    panelVillager.SetActive(false);
                }
            }
        }
        else
        {
            // If the raycast does not hit anything, hide the panels
            isVillagePanelVisible = false;// If the parent InformationOfVillage component is not found, hide the panel
            if (!isVillagePanelVisible && Time.time > panelHideTimeVillage)
            {
                panel.SetActive(false);
            }
            isVillagerPanelVisible = false;
            if (!isVillagerPanelVisible && Time.time > panelHideTimeVillager)
            {
                panelVillager.SetActive(false);
            }       
 
        }

        // Hide the panels if their respective display duration has passed and the mouse is not pointing at them
       
    }
}
