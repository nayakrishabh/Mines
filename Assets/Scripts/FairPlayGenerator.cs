using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class FairPlayGenerator : MonoBehaviour
{
    private int sessionSeed;

    public FairPlayGenerator(int seed) {
        sessionSeed = seed; 
    }

    public float getFairFactor(int step) {
        return 0.05f + (step * 0.002f);
    }
}
