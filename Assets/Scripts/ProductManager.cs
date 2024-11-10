using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Product manager, for spawn and update
public class ProductManager : MonoBehaviour
{
    [Header("Product Prefab")] [SerializeField]
    private List<ProductDisplayPrefab> productDisplayPrefabs;

    [Header("Update Popup References")] [SerializeField]
    private GameObject updatePopup;

    [SerializeField] private TextMeshProUGUI placeholderCurrentNameText;
    [SerializeField] private TextMeshProUGUI placeholderCurrentPriceText;
    [SerializeField] private TextMeshProUGUI placeholderErrorMessage;
    [SerializeField] private TMP_InputField newNameInputField;
    [SerializeField] private TMP_InputField newPriceInputField;

    private ProductDisplayPrefab productToChange;

    private const string nameErrorMessage = "Invalid product name. Please enter a name with at least 3 characters.";
    private const string priceErrorMessage = "Invalid product price. Please enter a valid number.";
    
    private void OnValidate()
    {
        Debug.Assert(productDisplayPrefabs != null,
            "No product prefabs assigned, please assign it to the ProductManager in the inspector");
        Debug.Assert(updatePopup != null,
            "No update popup assigned, please assign it to the ProductManager in the inspector");
        Debug.Assert(placeholderCurrentNameText != null,
            "Placeholder for current name text must be connected in editor of the prefab");
        Debug.Assert(placeholderCurrentPriceText != null,
            "Placeholder for current price text must be connected in editor of the prefab");
        Debug.Assert(placeholderErrorMessage != null,
            "Placeholder for error text must be connected in editor of the prefab");
        Debug.Assert(newNameInputField != null,
            "input field for new name text must be connected in editor of the prefab");
        Debug.Assert(newPriceInputField != null,
            "input field for new price text must be connected in editor of the prefab");
    }

    //Creates a new product
    public void CreateProducts(ProdcutsPulledFromJSON productsData)
    {
        ClearProductDisplay();

        for (int i = 0; i < productsData.products.Count; i++)
        {
            productDisplayPrefabs[i].gameObject.SetActive(true);
            productDisplayPrefabs[i].Init(this, productsData.products[i]);
        }
    }

    private void ClearProductDisplay()
    {
        foreach (ProductDisplayPrefab product in productDisplayPrefabs)
        {
            product.gameObject.SetActive(false);
        }
    }

    //Activates Update popup for the clicked product
    public void ActivateUpdatePopup(ProductDisplayPrefab productToUpdate)
    {
        newNameInputField.text = "";
        newPriceInputField.text = "";
        placeholderErrorMessage.text = "";
        updatePopup.SetActive(true);
        productToChange = productToUpdate;
        placeholderCurrentNameText.text = productToUpdate.productData.name;
        placeholderCurrentPriceText.text = productToUpdate.productData.price.ToString();
    }

    //Updates the products
    public void UpdateProductAfterInput()
    {
        // Check if the new name is a string with more than 2 letters
        if (string.IsNullOrWhiteSpace(newNameInputField.text) || newNameInputField.text.Length <= 2)
        {
            placeholderErrorMessage.text = nameErrorMessage;
            return;
        }

        if (!float.TryParse(newPriceInputField.text, out float newPrice))
        {
            placeholderErrorMessage.text = priceErrorMessage;
            return;
        }

        updatePopup.SetActive(false);
        productToChange.UpdatePrices(newNameInputField.text, Convert.ToInt32(newPriceInputField.text));
    }
}