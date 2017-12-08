package uy.com.searchandfind.service.cloud.register;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import com.google.firebase.iid.FirebaseInstanceId;
import com.google.firebase.iid.FirebaseInstanceIdService;

import uy.com.searchandfind.FieldValidator;
import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.SearchAndFindAuthenticationException;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.user.SimpleUserResponseBuilder;
import uy.com.searchandfind.user.UserRequest;

/**
 * Created by strat on 30/09/2017.
 */

public class ServiceCloudRegister extends FirebaseInstanceIdService {
    private ServerBackendInvoker<UserRequest, Response> serverBackendInvoker;


    public ServiceCloudRegister(){
        serverBackendInvoker=new ServerBackendInvoker<UserRequest,Response>(new SimpleUserResponseBuilder());
    }


    @Override
    public void onTokenRefresh() {
        String refreshedToken = FirebaseInstanceId.getInstance().getToken();
        SharedPreferenceHandler.storeValue(getApplicationContext(),getString(R.string.device_token), refreshedToken);
        notifyBackend(refreshedToken);
    }


    private void notifyBackend(String newDeviceId){
        UserRequest request=new UserRequest();
        request.setUserId(SharedPreferenceHandler.getValue(getApplicationContext(), getString(R.string.user_id)));
        request.setDeviceId(newDeviceId);
        try {
            String url=URLBuilder.buildPostURL(getResource(),SharedPreferenceHandler.getValue(getApplicationContext(), getString(R.string.change_device_operation)));
            Response response = serverBackendInvoker.invokeWithAuthentication(getApplicationContext(), url, request, "POST");
            if (!response.isSuccess()) {
                Log.e("Error", response.getMessage());
            }
        }catch(SearchAndFindAuthenticationException e){
            Log.e("Error", e.getMessage());
        }catch(Exception e) {
            Log.e("Error", e.getMessage());
        }
    }


    private String getResource() throws Exception {
        String resource=SharedPreferenceHandler.getValue(getApplicationContext(), getString(R.string.current_profile));
        if(FieldValidator.isFieldEmpty(resource)){
            throw new Exception("Aun no se ha seleccionado perfil. No se notifica id de dispositivo");
        }
        return resource;
    }
}
