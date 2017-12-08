package uy.com.searchandfind.service.cloud.message;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.media.RingtoneManager;
import android.net.Uri;
import android.support.v4.app.NotificationCompat;
import android.support.v4.app.TaskStackBuilder;

import uy.com.searchandfind.R;
import uy.com.searchandfind.activity.ActivityHandler;

/**
 * Created by strat on 30/09/2017.
 */

public class MessageHandler {

    public static void sendNotification(Context context, NotificationManager notificationManager,Class activity,Class parentActivity, String title, String body){
        Uri defaultSoundUri= RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);
        NotificationCompat.Builder mBuilder =
                new NotificationCompat.Builder(context)
                        .setContentTitle(title)
                        .setSmallIcon(R.drawable.syflogo)
                        .setAutoCancel(true)
                        .setSound(defaultSoundUri)
                        .setContentText(body).setVibrate(new long[] {1000, 1000, 1000, 1000, 1000}).setLights(Color.RED, 3000, 3000);
        Intent resultIntent = new Intent(context,activity);
        resultIntent.setAction(Intent.ACTION_MAIN);
        resultIntent.addCategory(Intent.CATEGORY_LAUNCHER);
        TaskStackBuilder stackBuilder = TaskStackBuilder.create(context);
        stackBuilder.addParentStack(parentActivity);
        stackBuilder.addNextIntent(resultIntent);
        mBuilder.setContentIntent(stackBuilder.getPendingIntent(0,PendingIntent.FLAG_UPDATE_CURRENT));
        notificationManager.notify(NotificationIdProvider.getID(), mBuilder.build());
    }
}
