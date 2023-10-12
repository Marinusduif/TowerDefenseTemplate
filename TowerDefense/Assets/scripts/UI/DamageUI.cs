using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    public Image[] damageImages; // Reference to an array of UI Image components.

    private bool showingDamage;
    private int currentImageIndex = 0;

    private void Start()
    {
        HideAllDamageUI();
    }

    public void ShowNextDamageUI()
    {
        if (damageImages.Length == 0)
            return;

        Image damageImage = damageImages[currentImageIndex];

        showingDamage = true;
        damageImage.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);

        currentImageIndex = (currentImageIndex + 1) % damageImages.Length;
    }

    public void HideAllDamageUI()
    {
        showingDamage = false;

        foreach (var image in damageImages)
        {
            image.enabled = false;
        }

        transform.GetChild(0).gameObject.SetActive(false); 
    }
}
