using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierCalculator : MonoBehaviour , IGridSizeListener ,INoOfBombsListener
{
    private static float baseMultiplier = 0.4f;
    private static int safeCellsRevealed = 0;
    private static FairPlayGenerator fairPlayGenerator;
    private static float RoundedoffMultiplier;
    private static int gridSizeFormul;
    private static int noofBombsM;

    void Start() {
        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.RegisterListener(this);
        }
        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.RegisterBlisteners(this);
        }
        int sessionSeed = Random.Range(0, 100000);
        fairPlayGenerator = new FairPlayGenerator(sessionSeed);
    }

    private void OnDisable() {
        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.UnregisterListener(this);
        }
        if (GridSizeNotifier.Instance != null) {
            GridSizeNotifier.Instance.UnregisterBlisteners(this);
        }
    }
    public static void IncrementSafeCellsRevealed() {
        safeCellsRevealed++;
    }
    public static void ResetSafeCellsRevealed() {
        safeCellsRevealed = 0;
    }
    public static void ResetMultiplier() {
        RoundedoffMultiplier = 0;
    }
    public static int getSafeCellsRevealed() {
        return safeCellsRevealed;
    }
    public static float getCalculatedMultiplier() {
        return RoundedoffMultiplier;
    }
    public static  float CalculateMultiplier() {
        float fairFactor = fairPlayGenerator.getFairFactor(safeCellsRevealed);

        float bombFactor = Mathf.Log10(noofBombsM + 1);
        float tileFactor = Mathf.Sqrt(gridSizeFormul);
        float safeFactor = 1 + (safeCellsRevealed * fairFactor * 0.8f);

        float calculatedMultiplier = baseMultiplier * bombFactor * tileFactor * safeFactor;

        calculatedMultiplier = Mathf.Max(calculatedMultiplier, baseMultiplier);

        RoundedoffMultiplier = Mathf.Round(calculatedMultiplier * 100f)/100f;

        return RoundedoffMultiplier;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGridSizeChanged(Vector2Int gridSize) {
        gridSizeFormul = 0;
        gridSizeFormul = gridSize.x*gridSize.x;
    }

    public void NoOfBombsChanged(int noOfBombs) {
        noofBombsM = 0;
        noofBombsM =  noOfBombs;
    }
}
