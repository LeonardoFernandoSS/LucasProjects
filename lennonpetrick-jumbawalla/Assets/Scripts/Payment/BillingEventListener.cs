using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BillingEventListener : MonoBehaviour {
	/*
	public StoreToothItem item01, 
			     		  item02, 
				     	  item03, 
			     		  item04;

	void OnEnable(){

		// Listen to all events for illustration purposes
#if UNITY_ANDROID
		GoogleIABManager.queryInventorySucceededEvent += android_productListReceivedEvent;
		GoogleIABManager.queryInventoryFailedEvent += failedEvent;
		GoogleIABManager.purchaseSucceededEvent += android_purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += failedEvent;
#elif UNITY_IPHONE
		StoreKitManager.productListReceivedEvent += iOS_productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent += failedEvent;		
		StoreKitManager.purchaseSuccessfulEvent += iOS_purchaseSucceededEvent;
		StoreKitManager.purchaseCancelledEvent += failedEvent;
		StoreKitManager.purchaseFailedEvent += failedEvent;
#endif

	}

	void OnDisable(){

		// Remove all event handlers
#if UNITY_ANDROID
		GoogleIABManager.queryInventorySucceededEvent -= android_productListReceivedEvent;
		GoogleIABManager.queryInventoryFailedEvent -= failedEvent;
		GoogleIABManager.purchaseSucceededEvent -= android_purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= failedEvent;
#elif UNITY_IPHONE
		StoreKitManager.productListReceivedEvent -= iOS_productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent -= failedEvent;		
		StoreKitManager.purchaseSuccessfulEvent -= iOS_purchaseSucceededEvent;
		StoreKitManager.purchaseCancelledEvent -= failedEvent;
		StoreKitManager.purchaseFailedEvent -= failedEvent;
#endif

	}

	void failedEvent(string error){
		
		Message.sharedMessage().setText(error).show();
		
	}

#if UNITY_ANDROID
	void android_productListReceivedEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus){
		
		if(skus.Count > 0){
			
			foreach(GoogleSkuInfo sku in skus){
				
				if(sku.productId == BillingItemsIDs.teeth_01){
					
					item01.init(sku.productId, sku.price);
					
				}
				else if(sku.productId == BillingItemsIDs.teeth_02){
					
					item02.init(sku.productId, sku.price);
					
				}
				else if(sku.productId == BillingItemsIDs.teeth_03){
					
					item03.init(sku.productId, sku.price);
					
				}
				else if(sku.productId == BillingItemsIDs.teeth_04){
					
					item04.init(sku.productId, sku.price);
					
				}
				
			}
			
		}
		
	}
	
	void android_purchaseSucceededEvent(GooglePurchase purchase)
	{
			if(purchase.productId == BillingItemsIDs.teeth_01) {
				
				MoneyTeeth.addTeeth(1000f);
				
			}
			else if(purchase.productId == BillingItemsIDs.teeth_02) {
				
				MoneyTeeth.addTeeth(10000f);
				
			}
			else if(purchase.productId == BillingItemsIDs.teeth_03) {
				
				MoneyTeeth.addTeeth(100000f);
				
			}
			else if(purchase.productId == BillingItemsIDs.teeth_04) {
				
				MoneyTeeth.addTeeth(1000000f);
				
			}
			
			BillingManager.consumeItem(purchase.productId);
			
			LoadStore.shareLoadStore().loadTeethAmount();
	}
#endif

#if UNITY_IPHONE
	void iOS_productListReceivedEvent(List<StoreKitProduct> productList){

		if(productList.Count > 0){
			
			foreach(StoreKitProduct skp in productList){
				
				if(skp.productIdentifier == BillingItemsIDs.teeth_01){
					
					item01.init(skp.productIdentifier, skp.price);
					
				}
				else if(skp.productIdentifier == BillingItemsIDs.teeth_02){
					
					item02.init(skp.productIdentifier, skp.price);
					
				}
				else if(skp.productIdentifier == BillingItemsIDs.teeth_03){
					
					item03.init(skp.productIdentifier, skp.price);
					
				}
				else if(skp.productIdentifier == BillingItemsIDs.teeth_04){
					
					item04.init(skp.productIdentifier, skp.price);
					
				}
				
			}
			
		}

	}

	void iOS_purchaseSucceededEvent(StoreKitTransaction transaction){
			
		if(transaction.productIdentifier == BillingItemsIDs.teeth_01) {
			
			MoneyTeeth.addTeeth(1000f);
			
		}
		else if(transaction.productIdentifier == BillingItemsIDs.teeth_02) {
			
			MoneyTeeth.addTeeth(10000f);
			
		}
		else if(transaction.productIdentifier == BillingItemsIDs.teeth_03) {
			
			MoneyTeeth.addTeeth(100000f);
			
		}
		else if(transaction.productIdentifier == BillingItemsIDs.teeth_04) {
			
			MoneyTeeth.addTeeth(1000000f);
			
		}
				
		LoadStore.shareLoadStore().loadTeethAmount();
			
	}
#endif
	*/

}