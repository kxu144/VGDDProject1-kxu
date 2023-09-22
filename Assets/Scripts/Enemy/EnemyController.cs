using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Editor Varaibles
    [SerializeField] protected int m_maxhealth;
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_damage;
    [SerializeField] protected ParticleSystem m_death; 
    [SerializeField] protected float m_healthpillrate;
    [SerializeField] protected GameObject m_healthpill;
    [SerializeField] protected int m_score;
    #endregion

    #region Private Variables
    protected float p_health;
    #endregion

    #region Cached Components
    protected Rigidbody cc_rb;
    #endregion

    #region Cached References
    protected Transform cr_player;
    #endregion

    #region Initialization
    protected void Awake() {
        p_health = m_maxhealth;

        cc_rb = GetComponent<Rigidbody>();
    }

    protected void Start() {
        cr_player = FindObjectOfType<PlayerController>().transform;
    }
    #endregion

    #region Main Updates
    protected virtual void FixedUpdate() {
        Vector3 dir = cr_player.position - transform.position;
        dir.Normalize();
        cc_rb.MovePosition(cc_rb.position + dir * m_speed * Time.fixedDeltaTime);
    }
    #endregion

    #region Collision
    protected void OnCollisionStay(Collision collision) {
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
