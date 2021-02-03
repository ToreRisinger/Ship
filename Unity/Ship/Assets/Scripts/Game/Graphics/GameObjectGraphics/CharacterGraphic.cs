using Ship.Game.Model;
using UnityEngine;

public class CharacterGraphic : MonoBehaviour
{

    private Character character;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position = new Vector2(character.position.X, character.position.Y);
    }

    public void setCharacter(Character character)
    {
        this.character = character;
    }
}
