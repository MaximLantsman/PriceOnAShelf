# Mocart_MaximLantsman
 A home assignment for mocart

# Running the Project Locally
This WebGL project is optimized for both desktop and mobile input support.

## Getting Started
1. Load the scene in Unity
2. Locate the "Pull" button at the scene's start
3. Click/tap the button to pull products and populate the shelf
4. You can either:
   - Pull new items
   - Update existing items in the scene


# Assets & Libraries Used
## 3D Models & Environment
- **Shelf**: Shelves01 Pack
  - Source: [Unity Asset Store](https://assetstore.unity.com/packages/3d/props/shelves01-pack-289927)
- **Stage Decoration**: Interior House Assets (URP)
  - Source: [Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/interior-house-assets-urp-257122)

## Audio
- **Update Sound Effect**: FREE Casual Game SFX Pack
  - Source: [Unity Asset Store](https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116)

## UI & Text
- **Text Rendering**: TextMeshPro Package
  - Documentation: [Unity Manual](https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/index.html)


# Architecture Overview
## ProductData
- Serialized class managing pulled products
- Handles JSON data caching
- Provides data structure for on-screen item display

## PullJSONFromServer
- Manages JSON data retrieval
- Implements async functionality with cancellation token support
- Fetches 1-3 products and forwards data to ProductManager

## ProductManager
- Handles product data integration with ProductDisplayPrefab
- Uses GameObject activation instead of instantiation for performance optimization
- Manages update popup screen for product data modification

## ProductDisplayPrefab
- Renders product data in the 3D environment
- Implements IPointerHandler interface for input handling
  - Chosen over new Input System for cleaner, simpler implementation
  - Ensures compatibility with WebGL and mobile platforms
