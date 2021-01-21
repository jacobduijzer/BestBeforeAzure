provider "azurerm" {
  version = "=2.34.0"
  skip_provider_registration = true
  features {}
}

resource "azurerm_resource_group" "rg" {
  name = "BestBefore"
  location = "westeurope"
}

resource "azurerm_storage_account" "terraformstorage" {
  name                     = "sabestbeforeterraform"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "RAGRS"
  allow_blob_public_access = true
  min_tls_version          = "TLS1_2"
}

# resource "azurerm_app_service_plan" "service-plan" {
#   name                = "plan-bestbefore"
#   location            = azurerm_resource_group.rg.location
#   resource_group_name = azurerm_resource_group.rg.name
#   kind                = "Linux"
#   reserved            = true

#   sku {
#     tier = "Basic"
#     size = "B1"
#   }
# }