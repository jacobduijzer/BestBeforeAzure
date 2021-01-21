terraform {
  backend "azurerm" {
    resource_group_name   = "BestBefore" 
    storage_account_name  = "sabestbeforeterraform"
    container_name        = "tfstate"
    key                   = "terraform.tfstate"
  }
}
