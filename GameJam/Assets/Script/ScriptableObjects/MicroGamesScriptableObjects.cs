using UnityEngine;

[CreateAssetMenu(fileName = "MicroGame", menuName = "Scriptable Objects/MicroGames")]
public class MicroGamesScriptableObjects : ScriptableObject
{
    public string name;
    public string description;
    public GameObject prefab;
}
