using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum BonusType
{
    Addition,
    Difference,
    Product,
    Division
}
public class Doors : MonoBehaviour
{


    [Header("Elements")]
    [SerializeField] private SpriteRenderer rightDoorRender;
    [SerializeField] private SpriteRenderer leftDoorRender;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private TextMeshPro leftDoorText;
    [SerializeField] private Collider colliders;

    [Header("Setting")]
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount;
    
    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount;

    [SerializeField] private Color bonusColor;
    [SerializeField] private Color PenaltyColor;

    void Start()
    {
        ConfigureDoors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConfigureDoors()
    {
        //Right Door Configuration
        switch (rightDoorBonusType) 
        {
            case BonusType.Addition:
                rightDoorRender.color = bonusColor;
                rightDoorText.text = "+" + rightDoorBonusAmount;
                break;
            case BonusType.Difference:
                rightDoorRender.color = PenaltyColor;
                rightDoorText.text = "-" + rightDoorBonusAmount;
                break;
            case BonusType.Product:
                rightDoorRender.color = bonusColor;
                rightDoorText.text = "*" + rightDoorBonusAmount;
                break;
            case BonusType.Division:
                rightDoorRender.color = PenaltyColor;
                rightDoorText.text = "/" + rightDoorBonusAmount;
                break;
        }

        //Left Door Configuration
        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                leftDoorRender.color = bonusColor;
                leftDoorText.text = "+" + leftDoorBonusAmount;
                break;
            case BonusType.Difference:
                leftDoorRender.color = PenaltyColor;
                leftDoorText.text = "-" + leftDoorBonusAmount;
                break;
            case BonusType.Product:
                leftDoorRender.color = bonusColor;
                leftDoorText.text = "*" + leftDoorBonusAmount;
                break;
            case BonusType.Division:
                leftDoorRender.color = PenaltyColor;
                leftDoorText.text = "/" + leftDoorBonusAmount;
                break;
        }
    }

    public int GetBonusAmount(float xPositon)
    {
        if(xPositon > 0)
        {
            return rightDoorBonusAmount;
        }
        else
        {
            return leftDoorBonusAmount;
        }
    }

    public BonusType GetBonusType(float xPositon)
    {
        if(xPositon > 0)
        {
            return rightDoorBonusType;
        }
        else
        {
            return leftDoorBonusType;
        }
    }

    public void DisableDoorCollider()
    {
        colliders.enabled = false;
    }
}
