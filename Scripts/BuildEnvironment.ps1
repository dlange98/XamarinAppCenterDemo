#Login-AzureRmAccount

$SubName = "Dan Lange DevTest"
$StorageARMTemplateLoc = "C:\Users\estormann\Source\Repos\Ent_HiveReferenceArchitecture\Scripts\Templates\Storage.json"

$NewResourceGroupName = "ARMDeploymentTest03"
$StorageName = "TESTStorage"
# Get-AzureRMSubscription

Select-AzureRmSubscription -SubscriptionName $SubName
Get-AzureRmContext
New-AzureRmResourceGroup -Name $NewResourceGroupName -Location "East US"

#New-AzureRmResourceGroupDeployment -Name TESTDeployment -ResourceGroupName $NewResourceGroupName -TemplateFile $StorageARMTemplateLoc -storageNamePrefix $StorageName.ToLower() -storageSKU Standard_LRS

#$StorageAccount = Get-AzureRmResource -ResourceGroupName $NewResourceGroupName -ResourceType Microsoft.Storage/storageAccounts | Select-Object -first 1
write-host $StorageAccount.ResourceId