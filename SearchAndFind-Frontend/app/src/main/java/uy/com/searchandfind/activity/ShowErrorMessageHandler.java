package uy.com.searchandfind.activity;

import android.app.Activity;
import android.widget.Toast;

import uy.com.searchandfind.user.UserRegisterActivity;

/**
 * Created by strat on 09/10/2017.
 */

public class ShowErrorMessageHandler {

    public static void showMessageError(Activity activity, String message, Integer duration){
        Toast errorToast = Toast.makeText(activity, message, duration);
        errorToast.show();
    }
}
