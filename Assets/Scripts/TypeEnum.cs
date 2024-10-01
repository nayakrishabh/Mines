using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTag : MonoBehaviour {
    public enum Type {
        DAIMOND,
        BOMB
    };
    public enum RevelType {
        UNREVELED,
        REVELED
    }
    public Type objectType;
    public RevelType revelType;
}
