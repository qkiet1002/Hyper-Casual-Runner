using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
public enum Maths
{
    Addition,
    Difference,
    Product,
    Division,
    Wrong
}

public class Block : MonoBehaviour
{
    [Header("Size & Color")]
    [SerializeField] private int MaxA;
    [SerializeField] private int MaxB;
    [SerializeField] private int intSUM;
    [SerializeField] private Material[] blockColor;


    [Header("References")]
    [SerializeField] private GameObject completeBlock;
    //[SerializeField] private GameObject brokenBlock;
    [SerializeField] private TextMeshPro a;
    [SerializeField] private TextMeshPro b;
    [SerializeField] private TextMeshPro sum;
    [SerializeField] private TextMeshPro signMath;
    public bool MathfTrue = false;
    public bool MathfCham = false;



    [SerializeField] private Maths blockBonusType;

    [SerializeField] private float searchRadius;


    private void Start()
    {
        ApplyRandomMaterial();
        Maft();
    }

    public void Update()
    {

        SearchForTarget();
        
    }
    private void ApplyRandomMaterial()
    {
        // Kiểm tra mảng blockColor có Material nào không
        if (blockColor.Length > 0)
        {
            int randomIndex = Random.Range(0, blockColor.Length);

            Renderer renderer = completeBlock.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material = blockColor[randomIndex];
            }
        }
    }



        private void Maft()
    {
        // Ngẫu nhiên hóa intA và intB với giá trị trong khoảng 1 đến 100
        int intA = Random.Range(1, MaxA);
        int intB = Random.Range(1, MaxB);
        a.text = intA.ToString();
        b.text = intB.ToString();
        switch (blockBonusType)
        {
            case Maths.Addition:
                intSUM = intA + intB;
                signMath.text = " + ";
                sum.text = intSUM.ToString();
                MathfTrue = true;
                break;
            case Maths.Product:
                intSUM = intA * intB;
                signMath.text = " * ";
                sum.text = intSUM.ToString();
                MathfTrue = true;
                break;
            case Maths.Difference:
                intSUM = intA - intB;
                signMath.text = " - ";
                sum.text = intSUM.ToString();
                MathfTrue = true;
                break;
            case Maths.Division:
                // Kiểm tra tránh chia cho 0
                if (intB != 0)
                {
                    intSUM = intA / intB;
                }
                else
                {
                    intSUM = 0;
                }
                signMath.text = " / ";
                sum.text = intSUM.ToString();
                break;
            case Maths.Wrong:
                // Ngẫu nhiên hóa intSUM và phép toán sai
                intSUM = Random.Range(intA + 5, intB + 5); // Đảm bảo intB lớn hơn intA
                string[] mathSigns = { " + ", " - ", " * ", " / " };
                int randomIndex = Random.Range(0, mathSigns.Length);
                signMath.text = mathSigns[randomIndex];
                sum.text = intSUM.ToString();
                break;
        
    }

}

    private void SearchForTarget()
        {
            Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);
            for (int i = 0; i < detectedColliders.Length; i++)
            {
                if (detectedColliders[i].TryGetComponent(out Runner runner))
                {
                    if (runner.IsTaget())
                        continue;

                    runner.SetTaget();
                    CrowdSystem crowdSystem = runner.GetComponentInParent<CrowdSystem>();
                    int players = crowdSystem.Runners();
                    if (MathfTrue)
                    {
                        Destroy(gameObject);
                    MathfCham = true;


                    }
                    else
                    {
                        GameManager.instance.SetGameState(GameManager.GameState.GameOver);
                    }



                }
            }
        }
}
