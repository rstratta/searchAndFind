package uy.com.searchandfind.activity;

import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.support.v4.app.TaskStackBuilder;

/**
 * Created by strat on 01/10/2017.
 */

public class ActivityHandler {


    public static void changeActivity(Context context, Class activityDestination) {
        Intent resultIntent = new Intent(context, activityDestination);
        resultIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        context.startActivity(resultIntent);
    }

    public static void closeAllAndGo(Context context, Class ActivityDestination) {
        Intent intent = new Intent(context, ActivityDestination);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        context.startActivity(intent);
    }

    public static void notificationActivity(Context context, Class activityDestination) {
        Intent resultIntent = new Intent(context, activityDestination);
        resultIntent.setAction(Intent.ACTION_MAIN);
        resultIntent.addCategory(Intent.CATEGORY_LAUNCHER);
        context.startActivity(resultIntent);
    }
}
