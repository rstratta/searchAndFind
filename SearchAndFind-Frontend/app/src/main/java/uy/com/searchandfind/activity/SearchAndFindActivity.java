package uy.com.searchandfind.activity;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;

import uy.com.searchandfind.CacheManager;
import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.SearchAndFindAuthenticationException;
import uy.com.searchandfind.client.ClientDTO;
import uy.com.searchandfind.client.ClientResponse;
import uy.com.searchandfind.client.ClientResponseBuilder;
import uy.com.searchandfind.login.LoginActivity;
import uy.com.searchandfind.saler.SalerDTO;
import uy.com.searchandfind.saler.SalerResponse;
import uy.com.searchandfind.saler.SalerResponseBuilder;
import uy.com.searchandfind.service.AsyncResponse;
import uy.com.searchandfind.service.backend.ServerBackendController;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.user.UserHandler;
import uy.com.searchandfind.user.UserRequest;

/**
 * Created by strat on 31/10/2017.
 */

public abstract class SearchAndFindActivity extends AppCompatActivity implements AsyncResponse{
    private ProgressDialog progressDialog;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }


    public void invokeBackend(final Object[] params, final String message, final Context context){
        final ServerBackendController controller=new ServerBackendController(this);
        progressDialog= new ProgressDialog(context,
                R.style.Theme_AppCompat_Light_Dialog);
        progressDialog.setIndeterminate(false);
        progressDialog.setMessage(message);
        progressDialog.show();
        new android.os.Handler().post(new Runnable() {
            @Override
            public void run() {
                controller.execute(params);
            }
        });
    }

    public void processResponse(Response response) {
        progressDialog.dismiss();
        processConcreteResponse(response);
    }

    protected abstract void processConcreteResponse(Response response);

    protected void logoutUser(Context context){
        CacheManager.getInstance().reset();
        cleanSharedPreferences(context);
    }

    public static void processAuthenticationError (Context context) {
        cleanSharedPreferences(context);
        ActivityHandler.changeActivity(context, LoginActivity.class);
    }

    private static void cleanSharedPreferences(Context context){
        SharedPreferenceHandler.removeValue(context, context.getString(R.string.current_user));
        SharedPreferenceHandler.removeValue(context, context.getString(R.string.token));
    }
}
