package uy.com.searchandfind;

import android.text.TextUtils;

import uy.com.searchandfind.R;

/**
 * Created by Luis on 2/10/2017.
 */

public class FieldValidator {

    public static boolean isAValidPassword(String password){
        return validatePassword(password);
    }
    public static boolean isNotNull(Object field){return field!=null;}
    public static boolean isFieldEmpty(String field){
        return (TextUtils.isEmpty(field));
    }
    public static boolean isFieldEmpty(int field){
        return (field == 0);
    }
    public static boolean isFieldEmpty(double field){
        return (field == 0);
    }
    public static boolean isValidHour(int hour){return hour >= 0 && hour <= 24;}
    public static boolean isValidMail(String email){ return  email.contains("@") ; }

    private static boolean validatePassword(String password) {
        return  !isFieldEmpty(password) && password.length() > 4;
    }
}
