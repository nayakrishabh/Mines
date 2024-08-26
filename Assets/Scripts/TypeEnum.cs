using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTag : MonoBehaviour {
    public enum Type {
        DAIMOND,
        BOMB
    };

    public Type objectType; // This will appear in the Inspector for you to set
}
