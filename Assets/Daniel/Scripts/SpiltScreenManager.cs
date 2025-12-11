using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    [SerializeField] private Camera[] playerCameras;

    [SerializeField] private GameObject BorderHorizontal;
    [SerializeField] private GameObject BorderVertical1;
    [SerializeField] private GameObject BorderVertical2;

    [Header("Active Players(1–4)")]
    [SerializeField] private int playerCount = 2;

    private void Start()
    {
        ApplySplitScreenLayout(playerCount);
    }

    private void ApplySplitScreenLayout(int count)
    {
        if (playerCameras == null || playerCameras.Length == 0)
        {
            Debug.LogWarning("No player cameras assigned to SplitScreenManager.");
            return;
        }

       
        count = Mathf.Clamp(count, 1, playerCameras.Length);

        
        for (int i = 0; i < playerCameras.Length; i++)
        {
            if (playerCameras[i] != null)
            {
                playerCameras[i].gameObject.SetActive(false);
            }
        }

        
        for (int i = 0; i < count; i++)
        {
            if (playerCameras[i] != null)
            {
                playerCameras[i].gameObject.SetActive(true);
            }
        }

        switch (count)
        {
            case 1:
                // Full screen for Player 1
                SetCamRect(0, new Rect(0f, 0f, 1f, 1f));
                BorderHorizontal.SetActive(false);
                BorderVertical1.SetActive(false);
                BorderVertical2.SetActive(false);
                break;

            case 2:
                // Vertical split: P1 left, P2 right
                SetCamRect(0, new Rect(0f, 0.5f, 1f, 0.5f)); // P1
                SetCamRect(1, new Rect(0f, 0f, 1f, 0.5f)); // P2
                BorderHorizontal.SetActive(true);
                BorderVertical1.SetActive(false);
                BorderVertical2.SetActive(false);

                break;

            case 3:
                // Two on top, one at bottom
                SetCamRect(0, new Rect(0f, 0.5f, 0.5f, 0.5f)); // P1
                SetCamRect(1, new Rect(0.5f, 0.5f, 0.5f, 0.5f)); // P2
                SetCamRect(2, new Rect(0f, 0f, 1f, 0.5f)); // P3
                BorderHorizontal.SetActive(true);
                BorderVertical1.SetActive(true);
                BorderVertical2.SetActive(false);
                break;

            case 4:
                // 2x2 grid
                SetCamRect(0, new Rect(0f, 0.5f, 0.5f, 0.5f)); // P1 top-left
                SetCamRect(1, new Rect(0.5f, 0.5f, 0.5f, 0.5f)); // P2 top-right
                SetCamRect(2, new Rect(0f, 0f, 0.5f, 0.5f)); // P3 bottom-left
                SetCamRect(3, new Rect(0.5f, 0f, 0.5f, 0.5f)); // P4 bottom-right
                BorderHorizontal.SetActive(true);
                BorderVertical1.SetActive(true);
                BorderVertical2.SetActive(true);
                break;
        }
    }

    private void SetCamRect(int index, Rect rect)
    {
        if (index < 0 || index >= playerCameras.Length) return;
        Camera cam = playerCameras[index];
        if (cam == null) return;

        cam.rect = rect;
    }
}
