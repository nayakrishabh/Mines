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
        string input = sessionSeed + ":" + step;

        using(SHA256 sha256 = SHA256.Create()) {
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            int hashValue = System.BitConverter.ToInt32(hash, 0);

            return 0.05f + (Mathf.Abs(hashValue % 100) / 1000.0f);
        }
    }
}
