using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] abilityIcons;
    [SerializeField] private Ability[] abilities;
    [SerializeField] private List<Player> cars;
    public Transform finishLine;
    public UIManager UImanager;
    [SerializeField] private float animationSpeed;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        cars.Sort((c1, c2) => c1.CalculateRank().CompareTo(c2.CalculateRank()));
        foreach (Player car in cars)
        {
            car.rank = cars.IndexOf(car) + 1;
        }
    }

    public void getRandomAbility(Player player)
    {
        UImanager.abilitySprite.gameObject.SetActive(true);
        player.setAbility(null);
        StartCoroutine(AbilityChooser(player));
    }
    IEnumerator AbilityChooser(Player player)
    {
        float x = animationSpeed;

        while (animationSpeed < 0.65f)
        {
            foreach (Sprite i in abilityIcons)
            {
                UImanager.abilitySprite.sprite = i;
                yield return new WaitForSeconds(animationSpeed);
                animationSpeed *= 1.2f;

            }


        }
        animationSpeed = x;
        Ability ability = abilities[Random.Range(0, 4)];
        UImanager.abilitySprite.sprite = ability.abilityIcon;
        UImanager.abilitySprite.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ability.castTimes.ToString();
        player.setAbility(ability);
        yield return new WaitForSeconds(0.5f);


    }

    public void StartSoloRace()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
