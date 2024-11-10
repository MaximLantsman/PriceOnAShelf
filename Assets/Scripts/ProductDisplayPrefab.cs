using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//Product display in scene
public class ProductDisplayPrefab : MonoBehaviour, IPointerClickHandler
{
    public ProductData productData;
    
    [Header("Product Text placeholders")]
    [SerializeField]private TextMeshPro productNameText;
    [SerializeField]private TextMeshPro productDescriptionText;
    [SerializeField]private TextMeshPro productPriceText;
    
    [Header("Update object and sound")]
    [SerializeField]private AudioSource updateButtonPressSound;
    [SerializeField]private GameObject updatedSign;

    private Vector3 buttonStartPosition;
    private ProductManager productManager;
    
    private const string priceFormat = "0.00";
    public void OnValidate()
    {
        Debug.Assert(productNameText != null, "Name text must be connected in editor of the product prefab");
        Debug.Assert(productDescriptionText != null, "Description text must be connected in editor of the product prefab");
        Debug.Assert(productPriceText != null, "Price Text must be connected in editor of the product prefab");
        Debug.Assert(updateButtonPressSound != null, "Update Sound must be connected in editor of the product prefab");
        Debug.Assert(updatedSign != null, "Updated sign must be connected in editor of the product prefab");
    }

    public void Init(ProductManager productManagerConnection, ProductData newData)
    {
        updatedSign.SetActive(false);
        productManager = productManagerConnection;
        productData = newData;
        
        UpdateTextOnProduct();
    }

    public void UpdatePrices(string newName, float newPrice)
    {
        productData.name = newName;
        productData.price = newPrice;
        
        updatedSign.SetActive(true);
        UpdateTextOnProduct();
    }
    
    private void UpdateTextOnProduct()
    {
        productNameText.text = productData.name;
        productDescriptionText.text = productData.description;
        productPriceText.text = productData.price.ToString(priceFormat);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        updateButtonPressSound.Play();
        productManager.ActivateUpdatePopup(this);
    }
    
}
