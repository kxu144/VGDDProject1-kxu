using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Editor Varaibles
    [SerializeField] private int m_maxhealth;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_damage;
    [SerializeField] private ParticleSystem m_death; 
    [SerializeField] private float m_healthpillrate;
    [SerializeField] private GameObject m_healthpill;
    [SerializeField] private int m_score;
    #endregion

    #region Private Variables
    private float p_health;
    #endregion

    #region Cached Components
    private Rigidbody cc_rb;
    #endregion

    #region Cached References
    private Transform cr_player;
    #endregion

    #region Initialization
    private void Awake() {
        p_health = m_maxhealth;

        cc_rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        cr_player = FindObjectOfType<PlayerController>().transform;
    }
    #endregion

    #region Main Updates
    private void FixedUpdate() {
        Vector3 dir = cr_player.position - transform.position;
        dir.Normalize();
        cc_rb.MovePosition(cc_rb.position + dir * m_speed * Time.fixedDeltaTime);
    }
    #endregion

    #region Collision
    private void OnCollisionStay(Collision collision) {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().DecreaseHealth(m_damage);
        }
    }
    #endregion

    #region Health/Dying
    public void DecreaseHealth(float amount) {
        p_health -= amount;
        if (p_health <= 0) {
            ScoreManager.singleton.IncreaseScore(m_score);
            if (Random.value < m_healthpillrate) {
                Instantiate(m_healthpill, transform.position, Quaternion.identity);
            }
            Instantiate(m_death, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    #endregion
}
