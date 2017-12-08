package uy.com.searchandfind.service.cloud.message;


import android.app.Activity;
import android.app.ActivityManager;
import android.app.IntentService;
import android.app.NotificationManager;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.content.WakefulBroadcastReceiver;
import android.util.Log;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import uy.com.searchandfind.CacheManager;
import uy.com.searchandfind.R;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.client.activity.ClientHistoryActivity;
import uy.com.searchandfind.login.LoginActivity;
import uy.com.searchandfind.main.MainActivity;
import uy.com.searchandfind.query.QueryDTO;
import uy.com.searchandfind.query.activity.QueryResponseActivity;
import uy.com.searchandfind.saler.activity.SalerActivity;
import uy.com.searchandfind.tender.TenderDTO;

/**
 * Created by strat on 02/11/2017.
 */

public class SearchAndFindBroadcastReceiver extends BroadcastReceiver {
    private NotificationManager notificationManager ;
    private Map<String,Class> activityByTag;
    @Override
    public void onReceive(Context context, Intent intent) {
        Bundle bundle = intent.getExtras();
        String tag = String.valueOf(bundle.get("tag"));
        processMessageByTag(context, bundle, tag);
    }

    private void processMessageByTag(Context context, Bundle bundle, String tag) {

        if(tag.equals(context.getString(R.string.query_tag))){
            processMessageAsQueryNotification(bundle, context);
        }else if(tag.equals(context.getString(R.string.tender_from_saler_tag))){
            processMessageAsTenderNotification(bundle, context);
        }else{
            Class activity=getActivityFromTag(tag);
            if(activity!=null)
                MessageHandler.sendNotification(context, getNotificationManager(context ),
                        activity, MainActivity.class, getTitleFromTag(context, tag), getBodyFromTag(context, tag));
        }
    }

    private String getBodyFromTag(Context context, String tag) {
        if(tag.equals("confirmTender")){
            return context.getString(R.string.confirmTenderBody);
        }else if(tag.equals("reviewFromSaler")){
            return context.getString(R.string.refiewFromSalerBody);
        }
        return context.getString(R.string.reviewFromClientBody);
        
    }

    private String getTitleFromTag(Context context, String tag) {
        if(tag.equals("confirmTender")){
            return context.getString(R.string.confirmTenderTitle);
        }else if(tag.equals("reviewFromSaler")){
            return context.getString(R.string.reviewFromSalerTitle);
        }
        return "Valoración de vendedor";

    }

    private void processMessageAsTenderNotification(Bundle bundle, Context context) {
        String tenderId=String.valueOf(bundle.get(context.getString(R.string.idObject)));
        String latitudeFromData =String.valueOf(bundle.get(context.getString(R.string.saler_latitude)));
        String lengthFromData =String.valueOf(bundle.get(context.getString(R.string.saler_length)));
        double latitude;
        double length;
        try{
            latitude=getDoubleForMultipleLanguajes(latitudeFromData);
        }
        catch (NumberFormatException exception){
            String latitudeWithPoint = latitudeFromData.replace(',','.');
            latitude=getDoubleForMultipleLanguajes(latitudeWithPoint);
        }
        try{
            length=getDoubleForMultipleLanguajes(lengthFromData);
        }
        catch (NumberFormatException exception){
            String lengthWithPoint = lengthFromData.replace(',','.');
            length =getDoubleForMultipleLanguajes(lengthWithPoint);
        }
        String queryId=String.valueOf(bundle.get("queryId"));
        String shopName=String.valueOf(bundle.get(context.getString(R.string.shop_name)));
        String shopPhone=String.valueOf(bundle.get(context.getString(R.string.shop_phone)));
        double amount=getDoubleForMultipleLanguajes(String.valueOf(bundle.get(context.getString(R.string.tender_amount))));
        String description=String.valueOf(bundle.get(context.getString(R.string.tender_description)));
        int reviewNumber=Integer.valueOf(String.valueOf(bundle.get("numberOfReview")));
        double reviewAverage=getDoubleForMultipleLanguajes(String.valueOf(bundle.get("averageReview")));
        TenderDTO tenderDTO=new TenderDTO();
        tenderDTO.setTenderId(tenderId);
        tenderDTO.setQueryId(queryId);
        tenderDTO.getSalerDTO().setLength(length);
        tenderDTO.getSalerDTO().setLatitude(latitude);
        tenderDTO.getSalerDTO().setShopName(shopName);
        tenderDTO.getSalerDTO().setShopPhone(shopPhone);
        tenderDTO.getSalerDTO().setAverageReview(reviewAverage);
        tenderDTO.getSalerDTO().setNumberOfReview(reviewNumber);
        tenderDTO.setTenderAmount(amount);
        tenderDTO.setDescription(description);
        List<TenderDTO> tenders= null;
        if(CacheManager.getInstance().existKey(context.getString(R.string.tender_of_query))){
            tenders = (List<TenderDTO>) CacheManager.getInstance().lookup(context.getString(R.string.tender_of_query));
        } else {
            tenders=new ArrayList<>();
        }
        tenders.add(tenderDTO);
        CacheManager.getInstance().bind(context.getString(R.string.tender_of_query),tenders);
        if(!CacheManager.getInstance().hasObserver(context.getString(R.string.tender_of_query))) {
            ActivityHandler.changeActivity(context, QueryResponseActivity.class);
        }else {
            fireIntent(context, context.getString(R.string.tender_of_query));
        }
    }

    private void processMessageAsQueryNotification(Bundle bundle, Context context) {
        String queryId=String.valueOf(bundle.get(context.getString(R.string.idObject)));
        String categoryName=String.valueOf(bundle.get("categoryName"));
        String categoryDescription=String.valueOf(bundle.get("categoryDescription"));
        QueryDTO dto=new QueryDTO();
        dto.setDescription(categoryDescription);
        dto.setCategoryName(categoryName);
        dto.setId(queryId);
        List<QueryDTO> queries;
        if(CacheManager.getInstance().existKey(context.getString(R.string.pending_queries))){
            queries = (List<QueryDTO>) CacheManager.getInstance().lookup(context.getString(R.string.pending_queries));
        } else {
            queries=new ArrayList();
        }
        queries.add(dto);
        CacheManager.getInstance().bind(context.getString(R.string.pending_queries),queries);
        if(!CacheManager.getInstance().hasObserver(context.getString(R.string.pending_queries)))
            ActivityHandler.notificationActivity(context, SalerActivity.class);
        else
            fireIntent(context, context.getString(R.string.pending_queries));
    }

    private double getDoubleForMultipleLanguajes(String numberToConvert){
        double doubleNumber = 0;
        try{
            doubleNumber =  Double.valueOf(numberToConvert);
        }
        catch (NumberFormatException exception){
            numberToConvert = numberToConvert.replace(',','.');
            doubleNumber =  Double.valueOf(numberToConvert);
        }
        return doubleNumber;
    }

    private Class getActivityFromTag(String tag){
        if(getActivityByTag().containsKey(tag)){
            return getActivityByTag().get(tag);
        }
        return LoginActivity.class;
    }

    private Map<String,Class> getActivityByTag(){
        if(activityByTag==null){
            activityByTag= new HashMap<>();
            initMap();
        }
        return activityByTag;
    }

    private void initMap(){
        activityByTag.put("confirmTender", ClientHistoryActivity.class);
        activityByTag.put("reviewFromSaler", ClientHistoryActivity.class);
        activityByTag.put("reviewFromClient", ClientHistoryActivity.class);
    }

    private NotificationManager getNotificationManager(Context context){
        if(notificationManager==null){
            notificationManager =(NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
        }
        return notificationManager;
    }

    private void fireIntent(Context context, String tag) {
        Intent intent = new Intent(tag);
        context.sendBroadcast(intent);
    }
}
