using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Ability ability;
    public int rank = 1;
    private float currentValue = 3;


    private void Start()
    {
        StartRace();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && ability != null)
        {
            UseAbility();
        }
        if (GetComponent<CarController>().enabled == true)
            Debug.Log(rank);
    }

    public float CalculateRank()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.finishLine.position);
    }

    public void setAbility(Ability ability)
    {
        this.ability = ability;
    }
    public Ability getAbility()
    {
        return ability;
    }

    public void UseAbility()
    {
        UIManager ui = FindObjectOfType<UIManager>();
        ability.DoEffect(this.gameObject);
        ability.currentCastTimes--;
        ui.abilitySprite.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ability.currentCastTimes.ToString();
        if (ability.currentCastTimes == 0)
        {
            ability.currentCastTimes = ability.castTimes;
            ability = null;
            ui.abilitySprite.sprite = null;
            ui.abilitySprite.gameObject.SetActive(false);
            ui.abilitySprite.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }

    }
    public void GotHit(Vector3 position)
    {
        StartCoroutine(GotHitTimer(position));
    }
    public IEnumerator GotHitTimer(Vector3 position)
    {
        GetComponent<CarController>().enabled = false;
        yield return new WaitForSeconds(2f);
        transform.position = position;
        GetComponent<CarController>().enabled = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;


    }
    public void StartRace()
    {
        InvokeRepeating("Decrement", 1f, 1f);
    }
    public void Decrement()
    {
        currentValue--;
        TextMeshProUGUI t = FindObjectOfType<UIManager>().counterText;
        t.text = currentValue.ToString();

        if (currentValue <= 0)
        {
            // Stop the decrementing process or perform any other desired actions.
            t.gameObject.SetActive(false);
            CancelInvoke("Decrement");
            GetComponent<CarController>().enabled = true;

        }

    }


}
