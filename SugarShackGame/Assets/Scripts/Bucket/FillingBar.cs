using UnityEngine;

public class FillingBar : MonoBehaviour, IFlow
{
    [SerializeField] private Transform filled;
    [SerializeField] private Transform max;

    // MAX BAR STATS
    private float maxBarLenght = .5f;
    private float maxBarWidth = .1f;
    private float maxBarHeight = .1f;

    // FILLED BAR STATS
    private float filledBarMaxLenght = .51f;
    private float filledBarLenght = .0f;
    private float filledBarWidth = .11f;
    private float filledBarHeight = .11f;

    public void Fill(float fillAmount, float maxAmount) {
        if (fillAmount > 0)
            filled.gameObject.SetActive(true);
        if (fillAmount < 0)
            return;

        filledBarLenght = (fillAmount / maxAmount) * filledBarMaxLenght;

        filled.localScale = new Vector3(filledBarWidth, filledBarHeight, filledBarLenght);
        filled.position = transform.position;
        filled.localPosition += new Vector3(0, 0, (filledBarMaxLenght / 2) - (filledBarLenght / 2));

    }

    public void Rotate(Player _player) {
        transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
        transform.Rotate(new Vector3(0, 90, 0));
    }

    public void Initialize() {
    }

    public void PhysicsRefresh() {
    }

    public void PreInitialize() {
        max.localScale = new Vector3(maxBarWidth, maxBarHeight, maxBarLenght);
        filled.localScale = new Vector3(filledBarWidth, filledBarHeight, filledBarLenght);
        filled.gameObject.SetActive(false);
    }

    public void Refresh() {
    }
}
