using UnityEngine;

public class ItemScript : DropItem, IItem
{
    private Item item;
    [SerializeField]
    private string skill;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer outlineRenderer;
    [SerializeField]
    private Color[] colors;

    private void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    public void Init(Item item)
    {
        this.item = item;
        if (skill != "") this.item.skill = DataManager.skillDB[skill];
        if (item.itemImage.Contains("_"))
        {
            string[] splitStr = item.itemImage.Split('_');
            Sprite[] sprites = Resources.LoadAll<Sprite>(splitStr[0]);
            Sprite[] outlines = Resources.LoadAll<Sprite>(splitStr[0] + "_outline");
            spriteRenderer.sprite = sprites[int.Parse(splitStr[1])];
            outlineRenderer.sprite = outlines[int.Parse(splitStr[1])];
        }
        else spriteRenderer.sprite = Resources.Load<Sprite>(item.itemImage);

        SetOutlineColor();

        Diffusion(item.rarity);
    }

    public void Use()
    {
        InventoryManager.instance.AddItem(item);

        if (effect)
            Instantiate(effect, transform.position, Quaternion.identity);

        if (sound)
            SoundEffectManager.SoundEffect(sound);

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void SetOutlineColor()
    {
        switch (item.rarityType)
        {
            case null: outlineRenderer.color = Color.clear; break;
            case "Normal": outlineRenderer.color = Color.clear; break;
            case "HiQuality": outlineRenderer.color = colors[1]; break;
            case "Magic": outlineRenderer.color = colors[2]; break;
            case "Rare": outlineRenderer.color = colors[3]; break;
            case "Unique": outlineRenderer.color = colors[4]; break;
            case "Legendary": outlineRenderer.color = colors[5]; break;
        }
    }
}
