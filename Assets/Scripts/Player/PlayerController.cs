using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("speed of player")]
    private float m_speed;

    [SerializeField]
    [Tooltip("transform of camera")]
    private Transform m_camera_transform;

    [SerializeField] private PlayerAttackInfo[] m_attacks;

    [SerializeField] private int m_maxhealth;

    [SerializeField] private HUDController m_HUD;
    #endregion

    #region Cached References
    private Animator cr_anim;
    private Renderer cr_render;
    #endregion

    #region Cached Components
    private Rigidbody cc_rb;
    #endregion

    #region Private Variables
    private Vector2 p_velocity;
    private float p_frozentimer;
    private Color p_defaultcolor;
    private float p_curhealth;
    #endregion

    #region Initialization
    private void Awake() {
        p_velocity = Vector2.zero;
        cc_rb = GetComponent<Rigidbody>();
        cr_anim = GetComponent<Animator>();
        cr_render = GetComponentInChildren<Renderer>();
        p_defaultcolor = cr_render.material.color;

        p_frozentimer = 0;
        p_curhealth = m_maxhealth;

        for (int i = 0; i < m_attacks.Length; i++) {
            PlayerAttackInfo attack = m_attacks[i];
            attack.Cooldown = 0;
            
            if (attack.WindUpTime > attack.FrozenTime) {
                Debug.LogError(attack.AttackName + " has wind up time > frozen time");
            }
        }
    }
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region Main Updates
    private void Update() {
        if (p_frozentimer > 0) {
            p_velocity = Vector2.zero;
            p_frozentimer -= Time.deltaTime;
            return;
        } else {
            p_frozentimer = 0;
        }

        bool haveAttacked = false;
        for (int i = 0; i < m_attacks.Length; i++) {
            PlayerAttackInfo attack = m_attacks[i];
            if (attack.IsReady()) {
                if (!haveAttacked && Input.GetButtonDown(attack.Button)) {
                    haveAttacked = true;
                    p_frozentimer = attack.FrozenTime;
                    DecreaseHealth(attack.HealthCost);
                    StartCoroutine(UseAttack(attack));
                }
            } else if (attack.Cooldown > 0) {
                attack.Cooldown -= Time.deltaTime;
            }
        }

        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        cr_anim.SetFloat("Speed", Mathf.Clamp01(Mathf.Abs(forward) + Mathf.Abs(right)));

        float moveThreshold = 0.3f;

        if (forward > 0 && forward < moveThreshold) {
            forward = 0;
        } else if (forward < 0 && forward > -moveThreshold) {
            forward = 0;
        }
        if (right > 0 && right < moveThreshold) {
            right = 0;
        } else if (right < 0 && right > -moveThreshold) {
            right = 0;
        }
        p_velocity.Set(right, forward);
    }

    private void FixedUpdate() {
        cc_rb.MovePosition(cc_rb.position + m_speed * Time.fixedDeltaTime * transform.forward * p_velocity.magnitude);
        cc_rb.angularVelocity = Vector3.zero;
        if (p_velocity.sqrMagnitude > 0) {
            float angleToRotCam = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, p_velocity);
            Vector3 camForward = m_camera_transform.forward;
            Vector3 newRot = new Vector3(Mathf.Cos(angleToRotCam) * camForward.x - Mathf.Sin(angleToRotCam) * camForward.z, 0, Mathf.Cos(angleToRotCam) * camForward.z - Mathf.Sin(angleToRotCam) * camForward.x);
            float theta = Vector3.SignedAngle(transform.forward, newRot, Vector3.up);
            cc_rb.rotation = Quaternion.Slerp(cc_rb.rotation, cc_rb.rotation * Quaternion.Euler(0, theta, 0), 0.2f);
        }
    }
    #endregion

    #region Health/Dying
    public void DecreaseHealth(float amount) {
        p_curhealth -= amount;
        m_HUD.UpdateHealth(1.0f * p_curhealth / m_maxhealth);
        if (p_curhealth <= 0) {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void IncreaseHealth(float amount) {
        p_curhealth += amount;
        if (p_curhealth > m_maxhealth) {
            p_curhealth = m_maxhealth;
        }
        m_HUD.UpdateHealth(1.0f * p_curhealth / m_maxhealth);
    }
    #endregion

    #region Attack Methods
    private IEnumerator UseAttack(PlayerAttackInfo attack) {
        cc_rb.rotation = Quaternion.Euler(0, m_camera_transform.eulerAngles.y, 0);
        cr_anim.SetTrigger(attack.Trigger);
        IEnumerator toColor = ChangeColor(attack.AbilityColor, 10);
        yield return new WaitForSeconds(attack.WindUpTime);

        Vector3 offset = transform.forward * attack.Offset.z + transform.right * attack.Offset.x + transform.up * attack.Offset.y;
        GameObject go = Instantiate(attack.Ability, transform.position + offset, cc_rb.rotation);
        go.GetComponent<Ability>().Use(transform.position + offset);
        yield return new WaitForSeconds(attack.Cooldown);

        StopCoroutine(toColor);
        StartCoroutine(ChangeColor(p_defaultcolor, 50));
        attack.ResetCooldown();
    }
    #endregion

    #region Misc Methods
    private IEnumerator ChangeColor(Color newColor, float speed) {
        Color curColor = cr_render.material.color;
        while (curColor != newColor) {
            curColor = Color.Lerp(curColor, newColor, speed / 100);
            cr_render.material.color = curColor;
            yield return null;
        }
    }
    #endregion

    #region Collision Methods
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("HealthPill")) {
            IncreaseHealth(other.GetComponent<HealthPill>().HealthGain);
            Destroy(other.gameObject);
        }
    }
    #endregion
}
