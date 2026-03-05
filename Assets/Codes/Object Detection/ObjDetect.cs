using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class InteractableUIEntry
{
    public Transform Interactable;
    public Image[] InteractableUI;
}

public class ObjDetect : MonoBehaviour
{
    public bool mobileLocked = false;

    [Header("Game Menu")]
    public Image MainMenue;
    public Image EscUI;

    [Header("Player & Camera")]
    public GameObject Epopup;
    public GameObject MainCamera;

    [Header("Interactables")]
    public InteractableUIEntry[] Entries;
    public float DetectionRange = 2f;

    [Header("Phone UI")]
    public Image MobileUI;

    public bool[] uiOpen;
    private bool isPaused = false;

    private PlayerController player;
    private NavMeshAgent agent;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();

        if (Entries == null) Entries = new InteractableUIEntry[0];
        uiOpen = new bool[Entries.Length];

        for (int i = 0; i < Entries.Length; i++)
            HideEntryUI(i);

        if (MobileUI != null) MobileUI.gameObject.SetActive(false);
        if (MainMenue != null) MainMenue.gameObject.SetActive(false);


    }

    private void Update()
    {
        // ESC → Open pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !IsAnyUIOpen())
        {
            OpenMainMenu();
            return;
        }

        // Block everything if paused
        if (isPaused) return;

        // Q → Mobile UI

        // Interactable detection
        for (int i = 0; i < Entries.Length; i++)
        {
            var entry = Entries[i];
            if (entry == null || entry.Interactable == null) continue;

            float distance = Vector3.Distance(transform.position, entry.Interactable.position);

            if (distance <= DetectionRange)
            {
                Epopup.SetActive(true);
                Epopup.transform.LookAt(MainCamera.transform);
                Epopup.transform.Rotate(0, 180, 0);

                if (Input.GetKeyDown(KeyCode.E) && !uiOpen[i])
                {
                    CloseAllUI();
                    SetEntryUIActive(i, true);
                }
            }
            else
            {
                if (uiOpen[i])
                    SetEntryUIActive(i, false);

                Epopup.SetActive(false);
            }
        }
    }

    // ---------- MENU ----------
    private void OpenMainMenu()
    {
        isPaused = true;
        CloseAllUI();
        MainMenue.gameObject.SetActive(true);
        EscUI.gameObject.SetActive(false);
        DisablePlayer();
    }

    public void ResumeGame()
    {
        isPaused = false;
        MainMenue.gameObject.SetActive(false);
        EscUI.gameObject.SetActive(true);
        EnablePlayer();
    }

    public void QuitGame(string sceneName)
    {
        SceneManager.LoadScene("Home Page Scene");
    }

    // ---------- UI ----------
    private void OpenMobileUI()
    {
        MobileUI.gameObject.SetActive(true);
        DisablePlayer();
    }

    private void SetEntryUIActive(int index, bool active)
    {
        foreach (var img in Entries[index].InteractableUI)
            if (img != null) img.gameObject.SetActive(active);

        uiOpen[index] = active;

        if (active) DisablePlayer();
        else EnablePlayer();
    }

    private void HideEntryUI(int index)
    {
        foreach (var img in Entries[index].InteractableUI)
            if (img != null) img.gameObject.SetActive(false);
    }

    public void CloseAllUI()
    {
        EnablePlayer();
        if (MobileUI != null) MobileUI.gameObject.SetActive(false);

        for (int i = 0; i < uiOpen.Length; i++)
        {
            HideEntryUI(i);
            uiOpen[i] = false;
        }
    }

    public bool IsAnyUIOpen()
    {
        if (MobileUI != null && MobileUI.gameObject.activeSelf)
            return true;

        for (int i = 0; i < uiOpen.Length; i++)
            if (uiOpen[i]) return true;

        return false;
    }

    // ---------- PLAYER ----------
    public void DisablePlayer()
    {
        if (player != null)
        {
            player.enabled = false;
            player.input.Disable();
        }

        if (agent != null)
            agent.isStopped = true;
    }

    private void EnablePlayer()
    {
        if (player != null)
        {
            player.enabled = true;
            player.input.Enable();
        }

        if (agent != null)
            agent.isStopped = false;
    }
}
