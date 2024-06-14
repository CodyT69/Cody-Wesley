using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableUI : MonoBehaviour
{
    // Instance attribute for ourselves
    public static CollectableUI Instance { get; protected set; }

    protected TextMeshProUGUI _coinText;
    protected TextMeshProUGUI _gemText;


    private void Awake()
    {
        Instance = this;

        // Get the text objects for our coins and gems
        for( int i=0; i<this.transform.childCount; i++ )
        {
            var child = this.transform.GetChild(i);
            if( child.name == "Coin Text")
            {
                _coinText = child.GetComponent<TextMeshProUGUI>();
            }
            else if( child.name == "Gem Text")
            {
                _gemText = child.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    public void UpdateValues(CollectableController controller)
    {
        if( _coinText != null )
        {
            int coins = controller.GetTotalForCollectableType(CollectableType.Coin);
            _coinText.text = "Coins: " + coins;
        }

        if( _gemText != null )
        {
            int gems = controller.GetTotalForCollectableType(CollectableType.Gem);
            _gemText.text = "Gems: " + gems;
        }
    }
}
