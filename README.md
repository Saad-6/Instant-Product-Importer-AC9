# Instant Product Importer Plugin For Ablecommerce-9

## Description
The Instant Product Importer is a powerful plugin that allows you to seamlessly integrate products from various APIs into your store. With this plugin, you can define JSON key names for products, ensuring that products from any source are correctly mapped and handled within your store. This plugin supports consuming APIs from different providers, making it a versatile tool for product integration.

## Key Features
* **Generic API Consumption**: Supports APIs from various providers by allowing custom JSON key definitions.
* **Flexible Data Handling**: Can handle both single JSON objects and arrays of JSON objects.
* **Product Mapping**: Maps JSON response to product entities within the store.
* **Category Management**: Automatically creates categories if they do not exist.
* **Dynamic Product Addition**: Adds new products to the store based on API data.
* **Error Handling**: Provides detailed error messages for API call failures and invalid JSON responses.

## Installation

**1.** Download: Download the zip file from the build folder.  
**2.** Paste: Paste the zip file into the Ablecommerce/Website/Plugins directory.  
**3.** Install: Go to the admin dashboard, navigate to the plugins tab, and find the TPI Plugin under the general category. Click on install.  

## Usage
**1.** Access the Plugin: Navigate to TPI by going to the Third Party Tab in the admin dashboard to access the TPI management interface.  

**2.** Define API URL:
   * Enter the API URL that you wish to consume.  
   * Map JSON Keys: Use the provided form to define the JSON keys for product attributes such as name, price, description, and image.

**3.** Fetch and Map Data: Call the API and view the mapped data in various formats (e.g., Grid, Tabular, Singular).  

**4.** Add Products to Store: Use the mapped data to add new products to your store. The plugin will automatically map the entities and create categories if they do not exist.

