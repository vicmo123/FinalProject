using UnityEngine;

public class Bucket : MonoBehaviour, IFlow, IUsable
{
    [HideInInspector]
    public Player player;

    [SerializeField] private GameObject fillingBarObject;
    private FillingBar fillingBarPlayer1;
    private FillingBar fillingBarPlayer2;
    [SerializeField] private Transform positionForFillingBar;

    private Highlight highlight;

    [SerializeField] private GameObject readyToUse;
    private Transform readyIcon;
    private float readyDelay = 0.2f;
    private float endDelay;

    [HideInInspector] public float sapAmount = 0.0f;
    private float maxSapAmount = 20.0f;
    private float sapGainSpeed = 3.0f;

    private float remainingTimeToClaim = 0.0f;
    private float timeToClaim = 3.0f;
    private float timeToSteal = 6.0f;
    private float lastUsedTime = 0.0f;

    private float cooldown = 1.0f;
    private float endOfCooldown = 0.0f;

    private Renderer bucketRenderer;

    public void Initialize() {
        Placement();

        bucketRenderer = GetComponentInChildren<Renderer>();

        fillingBarPlayer1.transform.SetParent(null);
        fillingBarPlayer2.transform.SetParent(null);

        highlight.Initialize();
    }

    public void PhysicsRefresh() {
        fillingBarPlayer1.PhysicsRefresh();
        fillingBarPlayer2.PhysicsRefresh();
    }

    public void PreInitialize() {
        highlight = GetComponent<Highlight>();
        highlight.PreInitialize();

        CreateFillingBars();
        CreateReadyIcon();
    }

    public void Refresh() {
        Sap();

        highlight.Refresh();

        if (Time.time >= endDelay)
            ToggleReady(false);
    }

    private void ToggleReady(bool val) {
        if (val) {
            endDelay = Time.time + readyDelay;
        }

        readyIcon.gameObject.SetActive(val);

    }

    private void CreateReadyIcon() {
        readyIcon = GameObject.Instantiate(readyToUse).transform;
        readyIcon.transform.SetParent(transform);
        readyIcon.position = positionForFillingBar.position + new Vector3(0, .3f, 0);
        readyIcon.gameObject.SetActive(false);
    }
    private void CreateFillingBars() {
        fillingBarPlayer1 = GameObject.Instantiate(fillingBarObject).GetComponent<FillingBar>();
        fillingBarPlayer1.gameObject.layer = 9;
        fillingBarPlayer1.transform.SetParent(transform);
        fillingBarPlayer1.transform.position = positionForFillingBar.position;
        fillingBarPlayer1.PreInitialize();


        fillingBarPlayer2 = GameObject.Instantiate(fillingBarObject).GetComponent<FillingBar>();
        fillingBarPlayer2.gameObject.layer = 10;
        fillingBarPlayer2.transform.SetParent(transform);
        fillingBarPlayer2.transform.position = positionForFillingBar.position;
        fillingBarPlayer2.PreInitialize();
    }

    public void Use(Player _player) {
        if (!player) {
            // If there is no owner yet, allows them to claim the bucket
            Claim(_player);
        }
        else if (_player == player) {
            CollectSap(_player);
        }
        else {
            // If it is not the owner, allows them to claim the bucket
            Claim(_player);
        }
    }

    private void Placement() {
        float yPos = .8f;

        Transform _parent = transform.parent;
        transform.SetParent(null);
        transform.localScale = new Vector3(1, 1, 1);

        Bounds treeBounds = _parent.gameObject.GetComponentInChildren<BoxCollider>().bounds;
        Bounds bucketBounds = transform.GetComponentInChildren<BoxCollider>().bounds;

        Vector3 newPos = Vector3.zero;
        newPos.y = treeBounds.center.y - treeBounds.extents.y + yPos;

        int temp = Random.Range(1, 4);
        if (temp <= 2) {
            newPos.x = treeBounds.center.x;
            if (temp == 1)
                newPos.z = treeBounds.center.z + treeBounds.extents.z + bucketBounds.extents.z;
            else
                newPos.z = treeBounds.center.z - treeBounds.extents.z - bucketBounds.extents.z;
        } else {
            newPos.z = treeBounds.center.z;
            if (temp == 3)
                newPos.x = treeBounds.center.x + treeBounds.extents.x + bucketBounds.extents.x;
            else
                newPos.x = treeBounds.center.x - treeBounds.extents.x - bucketBounds.extents.x;
        }

        transform.position = newPos;


        transform.SetParent(_parent);
    }

    public void Looked(Player _player) {
        highlight.ToggleHighlight(true);
        ToggleReady(true);
    }

    private void Claim(Player _player) {
        if (Time.time - lastUsedTime <= 1.0f) { // If the player is already claiming
            if (remainingTimeToClaim > 0.0f)
                remainingTimeToClaim -= Time.deltaTime;
            else {
                // Bucket is claimed
                Claimed(_player);
                endOfCooldown = Time.time + cooldown;
                GameObject partEffect = ParticleEffectManager.Instance.Create(ParticleEffectType.Fireworks);
                partEffect.transform.position = this.transform.position;
            }
        }
        else { // If the player starts to claim
            if (!player)
                remainingTimeToClaim = timeToClaim;
            else
                remainingTimeToClaim = timeToSteal;
        }

        lastUsedTime = Time.time;
    }

    private void ChangeColor(Color color) {
        bucketRenderer.material.color = color;
    }

    private void CollectSap(Player _player) {
        if (Time.time >= endOfCooldown) {
            SoundManager.Play(SoundListEnum.CollectSap);
            // If the playing using the bucket is the owner, gets sap
            sapAmount -= _player.playerBucket.AddSap(sapAmount);
            fillingBarPlayer1.Fill(sapAmount, maxSapAmount);
            fillingBarPlayer2.Fill(sapAmount, maxSapAmount);
            endOfCooldown = Time.time + cooldown;
        }
    }

    private void Claimed(Player _player) {
        SoundManager.Play(SoundListEnum.ClaimBucket);

        ChangeColor(_player.color);
        if (player)
        {

            Debug.Log("Bucket was stolen!");
        }
        else
        {
            Debug.Log("Bucket was claimed!");
        }

        player = _player;

        // Temporary change of color
    }

    private void Sap() {
        sapAmount = Mathf.Clamp(sapAmount + (sapGainSpeed * Time.deltaTime), 0, maxSapAmount);
        fillingBarPlayer1.Fill(sapAmount, maxSapAmount);
        fillingBarPlayer2.Fill(sapAmount, maxSapAmount);
    }

    public bool CheckParent() {
        if (transform.parent)
            if (transform.parent.CompareTag("Maple"))
                return true;
            else return false;
        else
            return false;
    }
}