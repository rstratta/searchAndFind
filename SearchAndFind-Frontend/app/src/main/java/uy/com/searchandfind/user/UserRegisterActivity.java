package uy.com.searchandfind.user;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.google.firebase.iid.FirebaseInstanceId;
import com.google.gson.Gson;

import butterknife.ButterKnife;
import butterknife.InjectView;
import uy.com.searchandfind.FieldValidator;
import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.SearchAndFindAuthenticationException;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.SearchAndFindActivity;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.client.ClientDTO;
import uy.com.searchandfind.client.ClientResponse;
import uy.com.searchandfind.client.ClientResponseBuilder;
import uy.com.searchandfind.client.activity.ClientActivity;
import uy.com.searchandfind.login.LoginActivity;
import uy.com.searchandfind.saler.SalerDTO;
import uy.com.searchandfind.saler.SalerResponse;
import uy.com.searchandfind.saler.SalerResponseBuilder;
import uy.com.searchandfind.saler.SettingsSalerActivity;
import uy.com.searchandfind.saler.activity.SalerActivity;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;

public class UserRegisterActivity extends SearchAndFindActivity {


    @InjectView(R.id.input_email) EditText signUpMailText;
    @InjectView(R.id.input_password) EditText signUpPasswordText;
    @InjectView(R.id.btn_register) Button signUpButton;
    @InjectView(R.id.input_password_repeat) TextView passwordRepeatText;
    @InjectView(R.id.input_name) TextView inputName;
    @InjectView(R.id.input_lastname) TextView inputLastName;


    public UserRegisterActivity(){

    }
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_user_register);
        ButterKnife.inject(this);
        setTitle(getString(R.string.user_register));
        signUpButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(validateForm()){
                    signUpUser();
                }

            }
        });
    }

    private void signUpUser() {
        String resource=getResource();
        UserRequest request=buildUserRequest();
        if(resource.equals(getString(R.string.client_profile))){
            signupAsClient(resource, request);
        }else{
            signupAsSaler(resource, request);
        }
    }

    private void signupAsSaler(String resource, UserRequest request) {
        ServerBackendInvoker<UserRequest,SalerResponse> serverBackendInvoker=new ServerBackendInvoker(new SalerResponseBuilder());
        signUpUser(resource, request, serverBackendInvoker);
    }

    private void signupAsClient(String resource, UserRequest request) {
        ServerBackendInvoker<UserRequest,ClientResponse> serverBackendInvoker=new ServerBackendInvoker(new ClientResponseBuilder());
        signUpUser(resource, request, serverBackendInvoker);
    }

    private void signUpUser(String resource, UserRequest request, ServerBackendInvoker serverBackendInvoker) {
        String url= URLBuilder.buildPostURL(resource, "signup");
        Object[] params= ServerBackendControllerUtil.buildQueryParams(serverBackendInvoker,url,getApplicationContext(),"POST",request);
        invokeBackend(params,getString(R.string.creating_account),UserRegisterActivity.this);
    }

    private void saveProfileAndChangeActivity(String profile, Response response) {
        SharedPreferenceHandler.storeValue(getApplicationContext(), getString(R.string.current_profile),profile);
        SharedPreferenceHandler.storeValue(getApplicationContext(), getString(R.string.token),getCurrentToken(profile,response));
        SharedPreferenceHandler.storeValue(getApplicationContext(), getString(R.string.current_user),getJsonUserDTO(profile,response));
        SharedPreferenceHandler.storeValue(getApplicationContext(),getString(R.string.authentication_type),getString(R.string.search_and_find_authentication));
        if(profile.equals(getString(R.string.client_profile))){
            ActivityHandler.closeAllAndGo(getApplicationContext(),ClientActivity.class);
        }else{
            ActivityHandler.closeAllAndGo(getApplicationContext(),SalerActivity.class);
            finish();
        }
    }
    private String getJsonUserDTO(String profile, Response response) {
        Gson gson=new Gson();
        if(profile.equals(getString(R.string.client_profile))){
            return gson.toJson(getUserDTO(profile,response),ClientDTO.class);
        }
        return gson.toJson(getUserDTO(profile,response), SalerDTO.class);
    }

    private String getCurrentToken(String profile,Response response) {
        return getUserDTO(profile,response).getCurrentToken();
    }

    private UserDTO getUserDTO(String profile, Response response){
        if(profile.equals(getString(R.string.client_profile))){
            return ((ClientResponse)response).getClientDTO();
        }
        return ((SalerResponse)response).getSalerDTO();
    }
    private void showMessageError(String message) {
        ShowErrorMessageHandler.showMessageError(UserRegisterActivity.this,message, Toast.LENGTH_LONG);
    }

    private UserRequest buildUserRequest() {
        UserRequest request=new UserRequest();
        request.setMail(signUpMailText.getText().toString());
        request.setPassword(signUpPasswordText.getText().toString());
        request.setName(inputName.getText().toString());
        request.setLastName(inputLastName.getText().toString());
        request.setDeviceId(registInCloud());
        return request;
    }

    private String getResource() {
        return SharedPreferenceHandler.getValue(getApplicationContext(),getString(R.string.temporal_profile));
    }

    private boolean validateUserMail() {
        if(!FieldValidator.isValidMail(signUpMailText.getText().toString())){
            signUpMailText.setError(getString(R.string.error_invalid_email));
            signUpMailText.requestFocus();

            return false;
        }
        return true;
    }

    private boolean validatePassword() {
        if(!FieldValidator.isAValidPassword(signUpPasswordText.getText().toString())){
            signUpPasswordText.setError(getString(R.string.error_invalid_password));
            signUpPasswordText.requestFocus();
            return false;
        }
        return true;
    }

    private boolean validatePasswordRepeat() {
        if(!FieldValidator.isAValidPassword(passwordRepeatText.getText().toString())){
            passwordRepeatText.setError(getString(R.string.error_invalid_password));
            passwordRepeatText.requestFocus();
            return false;
        }
        if(!passwordRepeatText.getText().toString().equals(signUpPasswordText.getText().toString())){
            passwordRepeatText.setError(getString(R.string.error_different_password));
            passwordRepeatText.requestFocus();
            return false;
        }
        return true;
    }

    private boolean validateNameAndLastName() {
        if(FieldValidator.isFieldEmpty(inputName.getText().toString())){
            inputName.setError(getString(R.string.error_field_required));
            inputName.requestFocus();

            return false;
        }
        if(FieldValidator.isFieldEmpty(inputLastName.getText().toString())){
            inputLastName.setError(getString(R.string.error_field_required));
            inputLastName.requestFocus();

            return false;
        }
        return true;
    }

    /*@Override
    public void onBackPressed() {
        ActivityHandler.changeActivity(getApplicationContext(),LoginActivity.class);

        //finish();
    }*/

    private boolean validateForm(){
        return validateUserMail()&&validatePassword()&&validatePasswordRepeat()&&validateNameAndLastName();
    }
    private String registInCloud(){
        return FirebaseInstanceId.getInstance().getToken();
    }

    @Override
    protected void processConcreteResponse(Response response) {
        if (response.isSuccess()) {
            saveProfileAndChangeActivity(getResource(), response);
        } else {
            showMessageError(response.getMessage());
        }
    }
}
