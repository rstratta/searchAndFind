package uy.com.searchandfind.sharedpreference;

import android.content.Context;
import android.content.SharedPreferences;

import com.google.gson.Gson;

import uy.com.searchandfind.R;
import uy.com.searchandfind.client.ClientDTO;
import uy.com.searchandfind.saler.SalerDTO;

/**
 * Created by strat on 07/10/2017.
 */

public class SharedPreferenceHandler {
    private static Gson gson;

    public static void storeValue(Context context, String key, String value){
        SharedPreferences sharedPreferences=context.getSharedPreferences(context.getString(R.string.main_package), Context.MODE_PRIVATE);
        SharedPreferences.Editor sharedPreferenceEditor=sharedPreferences.edit();
        sharedPreferenceEditor.putString(key,value);
        //sharedPreferenceEditor.commit();
        sharedPreferenceEditor.apply();
    }

    public static String getValue(Context context, String key){
        SharedPreferences sharedPreferences=context.getSharedPreferences(context.getString(R.string.main_package), Context.MODE_PRIVATE);
        return sharedPreferences.getString(key, "");
    }

    public static void cleanAll(Context context) {
        SharedPreferences prefs = context.getSharedPreferences(context.getString(R.string.main_package), Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = prefs.edit();
        editor.clear();
        editor.commit();
    }

    public static String getClientMail(Context context){
        ClientDTO dto=getGson().fromJson(getValue(context,context.getString(R.string.current_user)),ClientDTO.class);
        return dto.getMailAddress();
    }
    public static String getSalerMail(Context context){
        SalerDTO dto=getGson().fromJson(getValue(context, context.getString(R.string.current_user)),SalerDTO.class);
        return dto.getMailAddress();
    }

    private static Gson getGson(){
        if(gson==null){
            gson=new Gson();
        }
        return gson;
    }

    public static void removeValue(Context context,String key) {
        SharedPreferences prefs = context.getSharedPreferences(context.getString(R.string.main_package), Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = prefs.edit();
        editor.remove(key);
        editor.apply();
    }
}
