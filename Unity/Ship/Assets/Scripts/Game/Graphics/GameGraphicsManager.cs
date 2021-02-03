using Ship.Game.Model;
using Ship.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class GameGraphicsManager : MonoBehaviour
{

    private Dictionary<int, CharacterGraphic> characterGraphics;

    #region prefabs

    public UnityEngine.GameObject characterPrefab;

    #endregion

    private void Awake()
    {
        Log.debug("GameGraphicsManager.Awake");

        characterGraphics = new Dictionary<int, CharacterGraphic>();
    }

    void Update()
    {
        
    }

    public void SpawnCharacter(Character character)
    {
        UnityEngine.GameObject gameObject = Instantiate(characterPrefab, new Vector2(character.position.X, character.position.Y), UnityEngine.Quaternion.Euler(UnityEngine.Vector3.forward));
        CharacterGraphic characterGraphic = gameObject.GetComponent<CharacterGraphic>();
        characterGraphic.setCharacter(character);
        characterGraphics.Add(character.id, characterGraphic);
    }


}
