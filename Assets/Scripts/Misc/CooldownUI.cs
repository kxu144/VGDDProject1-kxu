using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    [SerializeField] private PlayerAttackInfo attackInfo;
    private Text abilityText;

    private void Awake() {
        abilityText = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateText(0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText(attackInfo.Cooldown);
    }

    private void UpdateText(float cd) {
        abilityText.text = cd.ToString();
    }
}
