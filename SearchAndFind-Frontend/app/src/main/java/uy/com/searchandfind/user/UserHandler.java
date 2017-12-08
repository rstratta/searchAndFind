package uy.com.searchandfind.user;
import android.content.Context;
import android.widget.Toast;

import com.google.gson.Gson;

import uy.com.searchandfind.CacheManager;
import uy.com.searchandfind.SearchAndFindAuthenticationException;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.client.ClientDTO;
import uy.com.searchandfind.client.ClientResponse;
import uy.com.searchandfind.client.ClientResponseBuilder;
import uy.com.searchandfind.login.LoginActivity;
import uy.com.searchandfind.saler.SalerDTO;
import uy.com.searchandfind.saler.SalerResponse;
import uy.com.searchandfind.saler.SalerResponseBuilder;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.R;

public class UserHandler {

    private static Gson gson;

    public static String getCurrentUserId(Context context){
        String  currentUser= SharedPreferenceHandler.getValue(context, context.getString(R.string.current_user));
        UserDTO user = getGson().fromJson(currentUser,UserDTO.class);
        return user.getId();
    }

    private static Gson getGson(){
        if(gson==null){
            gson=new Gson();
        }
        return gson;
    }

}
