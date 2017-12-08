package uy.com.searchandfind.login;

import android.app.ProgressDialog;
import android.graphics.Color;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.firebase.auth.AuthCredential;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;

import butterknife.ButterKnife;
import butterknife.InjectView;
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
import uy.com.searchandfind.saler.SalerDTO;
import uy.com.searchandfind.saler.activity.SalerActivity;
import uy.com.searchandfind.saler.SalerResponse;
import uy.com.searchandfind.saler.SalerResponseBuilder;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.FieldValidator;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.user.UserDTO;
import uy.com.searchandfind.user.UserHandler;
import uy.com.searchandfind.user.UserRegisterActivity;
import uy.com.searchandfind.user.UserRequest;

import android.content.Intent;


import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.api.Status;
import com.google.firebase.auth.GoogleAuthProvider;
import com.google.firebase.auth.AuthResult;
import com.google.android.gms.tasks.Task;

import com.google.android.gms.common.SignInButton;
import com.google.firebase.iid.FirebaseInstanceId;
import com.google.gson.Gson;

public class LoginActivity extends SearchAndFindActivity implements
        GoogleApiClient.OnConnectionFailedListener{

    private static final String TAG = "LoginActivity";
    private static final int REQUEST_SIGNUP = 0;
    private static final int RC_SIGN_IN = 9001;
    private FirebaseAuth mAuth;
    private FirebaseAuth.AuthStateListener mAuthListener;
    private GoogleApiClient mGoogleApiClient;
    private ServerBackendInvoker serverInvoker;
    private Gson gson;
    @InjectView(R.id.input_email) EditText _emailText;
    @InjectView(R.id.input_password) EditText _passwordText;
    @InjectView(R.id.btn_login) Button _loginButton;
    @InjectView(R.id.link_signup) TextView _signupLink;
    @InjectView(R.id.g_sign_in_button) SignInButton _gsignin;
    @InjectView(R.id.status) TextView mStatusTextView;
    @InjectView(R.id.clientProfileSwitch)Switch clientProfileSwitch;
    @InjectView(R.id.salerProfileSwitch) Switch salerProfileSwitch;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        ButterKnife.inject(this);
        GoogleSignInOptions gso = buildGoogleSignInOption();
        mGoogleApiClient = buildGoogleApi(gso);
        mAuth = FirebaseAuth.getInstance();
        mAuthListener = buildAuthListener();
        _loginButton.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                signIn();
            }
        });
        _signupLink.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                signup();
            }
        });
        clientProfileSwitch.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view) {
                verifyClientSwitchState();
            }
        });
        salerProfileSwitch.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view) {
                verifySalerSwitchState();
            }
        });
        _gsignin.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                gSignIn();
            }
        });
        _gsignin.requestFocus();
        salerProfileSwitch.setVisibility(View.INVISIBLE);
        if(hasProfile()){
            hideSwitchAndCreateAccount();
        }
    }

    private boolean hasProfile(){
        return !SharedPreferenceHandler.getValue(getApplicationContext(),getString(R.string.current_profile)).isEmpty();
    }
    private void hideSwitchAndCreateAccount() {
        String profile = SharedPreferenceHandler.getValue(getApplicationContext(),getString(R.string.temporal_profile));
        if (profile.equals(getString(R.string.client_profile)))
            clientProfileSwitch.setChecked(true);
        //_signupLink.setVisibility(View.GONE);
    }

    @NonNull
    private FirebaseAuth.AuthStateListener buildAuthListener() {
        return new FirebaseAuth.AuthStateListener() {
            @Override
            public void onAuthStateChanged(@NonNull FirebaseAuth firebaseAuth) {
                FirebaseUser user = firebaseAuth.getCurrentUser();
                if (user != null) {
                    Log.d(TAG, "onAuthStateChanged:signed_in:" + user.getUid());
                } else {
                    Log.d(TAG, "onAuthStateChanged:signed_out");
                }
            }
        };
    }

    @NonNull
    private GoogleApiClient buildGoogleApi(GoogleSignInOptions gso) {
        return new GoogleApiClient.Builder(this)
                .enableAutoManage(this , this )
                .addApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .build();
    }

    @NonNull
    private GoogleSignInOptions buildGoogleSignInOption() {
        return new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
                .requestIdToken(getString(R.string.default_web_client_id))
                .requestEmail()
                .build();
    }

    @Override
    public void onStart() {
        super.onStart();
        FirebaseUser currentUser = mAuth.getCurrentUser();
        mAuth.addAuthStateListener(mAuthListener);
    }

    private void signIn (){
        if(switchIsChecked()) {
            saveTemporalProfile();
            syfSignIn();
        }else{
            ShowErrorMessageHandler.showMessageError(LoginActivity.this, "Debe seleccionar perfil de usuario",Toast.LENGTH_LONG);
        }
    }


    private void gSignIn() {
       if(switchIsChecked()) {
            saveTemporalProfile();
            SharedPreferenceHandler.storeValue(getApplicationContext(),getString(R.string.authentication_type),getString(R.string.google_authentication));
            Intent signInIntent = Auth.GoogleSignInApi.getSignInIntent(mGoogleApiClient);
            startActivityForResult(signInIntent, RC_SIGN_IN);
        }else{
            ShowErrorMessageHandler.showMessageError(LoginActivity.this, "Debe seleccionar perfil de usuario",Toast.LENGTH_LONG);
        }
    }

    private void syfSignIn() {
        _emailText.setError(null);
        _passwordText.setError(null);
        SharedPreferenceHandler.storeValue(getApplicationContext(),getString(R.string.authentication_type),getString(R.string.search_and_find_authentication));
        if (validatePassword()&&validateUserMail()) {
            UserRequest request=buildRequest();
            decideAndDoLogin(request);
        }
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == RC_SIGN_IN) {
            GoogleSignInResult result = Auth.GoogleSignInApi.getSignInResultFromIntent(data);
            Log.d(TAG, "handleSignInResult:" + result.isSuccess());
            Log.d(TAG, "handleSignInResult:" + result.getStatus());
            Status a = result.getStatus();
            handleSignInResult(result);
        }
    }

    private void handleSignInResult(GoogleSignInResult result)  {
        if (result.isSuccess()) {
            GoogleSignInAccount acct = result.getSignInAccount();
            firebaseAuthWithGoogle(acct);
            singInGoogleAuthentication(acct);
        } else {
            ShowErrorMessageHandler.showMessageError(LoginActivity.this,"Error al autenticar con Google",Toast.LENGTH_LONG);
        }
    }

    private void singInGoogleAuthentication(GoogleSignInAccount account)  {
        UserRequest request=new UserRequest();
        request.setMail(account.getEmail());
        request.setAuthenticationType(getString(R.string.google_authentication));
        request.setCurrentToken(account.getIdToken());
        request.setName(account.getGivenName());
        request.setLastName(account.getFamilyName());
        request.setDeviceId(registInCloud());
        decideAndDoLogin(request);
    }

    private String registInCloud(){
        String currentDeviceId=SharedPreferenceHandler.getValue(getApplicationContext(),getString(R.string.device_token));
        if(FieldValidator.isFieldEmpty(currentDeviceId))
            currentDeviceId=FirebaseInstanceId.getInstance().getToken();
        return currentDeviceId;
    }

    private void firebaseAuthWithGoogle(GoogleSignInAccount acct) {
        Log.d(TAG, "firebaseAuthWithGoogle:" + acct.getId());
        Log.d(TAG, "TOKEN:" + acct.getIdToken());
        AuthCredential credential = GoogleAuthProvider.getCredential(acct.getIdToken(), null);
        mAuth.signInWithCredential(credential)
                .addOnCompleteListener(this, new OnCompleteListener<AuthResult>() {
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        if (task.isSuccessful()) {
                            Log.d(TAG, "signInWithCredential:success");
                        } else {
                            Log.w(TAG, "signInWithCredential:failure", task.getException());
                            Toast.makeText(LoginActivity.this, "Authentication failed.",
                                    Toast.LENGTH_SHORT).show();
                        }
                    }
                });
    }

    @Override
    public void onConnectionFailed(ConnectionResult connectionResult) {
        Log.d(TAG, "onConnectionFailed:" + connectionResult);
        ShowErrorMessageHandler.showMessageError(LoginActivity.this,"Error al conectar", Toast.LENGTH_LONG);
    }

    @Override
    public void onStop() {
        super.onStop();
        if (mAuthListener != null) {
            mAuth.removeAuthStateListener(mAuthListener);
        }
    }

    private void signup ()
    {
        if(switchIsChecked()){
            saveTemporalProfile();
            ActivityHandler.changeActivity(this, UserRegisterActivity.class);
        }else{
            ShowErrorMessageHandler.showMessageError(LoginActivity.this, "Debe seleccionar perfil de usuario",Toast.LENGTH_LONG);
        }
    }

    private void decideAndDoLogin(UserRequest request)  {
        String temporalProfile=SharedPreferenceHandler.getValue(getApplicationContext(),getString(R.string.temporal_profile));
        serverInvoker = buildServerInvokerByProfile(temporalProfile);
        String url= URLBuilder.buildPostURL(temporalProfile, getString(R.string.sign_in_operation));
        Object[] params= ServerBackendControllerUtil.buildQueryParams(serverInvoker,url,getApplicationContext(),"POST",request);
        invokeBackend(params,getString(R.string.authenticate), LoginActivity.this);
    }

    private void storeProfile(String temporalProfile, Response response){
        SharedPreferenceHandler.storeValue(getApplicationContext(), getString(R.string.current_profile),temporalProfile);
        SharedPreferenceHandler.storeValue(getApplicationContext(), getString(R.string.token),getCurrentToken(temporalProfile,response));
        SharedPreferenceHandler.storeValue(getApplicationContext(), getString(R.string.current_user),getJsonUserDTO(temporalProfile,response));
    }

    private String getJsonUserDTO(String profile, Response response) {
        gson=new Gson();
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

    private ServerBackendInvoker buildServerInvokerByProfile(String temporalProfile){
        if(temporalProfile.equals(getString(R.string.client_profile))){
            return new ServerBackendInvoker<UserRequest, ClientResponse>(new ClientResponseBuilder());
        }else{
            return new ServerBackendInvoker<UserRequest, SalerResponse>(new SalerResponseBuilder());
        }
    }

    private UserRequest buildRequest() {
        UserRequest request=new UserRequest();
        request.setMail(_emailText.getText().toString());
        request.setPassword(_passwordText.getText().toString());
        request.setAuthenticationType(getString(R.string.search_and_find_authentication));
        request.setDeviceId(registInCloud());
        return request;
    }

    private boolean validateUserMail() {
        if(!FieldValidator.isValidMail(_emailText.getText().toString())){
            _emailText.setError(getString(R.string.error_invalid_email));
            _emailText.requestFocus();

            return false;
        }
        if(FieldValidator.isFieldEmpty(_emailText.getText().toString())){
            _emailText.setError(getString(R.string.error_field_required));
            _emailText.requestFocus();
            return false;
        }
        return true;
    }

    private boolean validatePassword() {
        if(!FieldValidator.isAValidPassword(_passwordText.getText().toString())){
            _passwordText.setError(getString(R.string.error_invalid_password));
            _passwordText.requestFocus();
            return false;
        }
        return true;
    }
    @Override
    public void onBackPressed() {
        // disable going back to the MainActivity
        moveTaskToBack(true);
    }

    private void changeActivityByProfile(String temporalProfile){
        if(temporalProfile.equals(getString(R.string.client_profile))){
            ActivityHandler.changeActivity(getApplicationContext(), ClientActivity.class);
        }else{
            ActivityHandler.changeActivity(getApplicationContext(), SalerActivity.class);
        }
        finish();
    }
    public void onLoginFailed() {
        Toast.makeText(getBaseContext(), "Logueo fallido", Toast.LENGTH_LONG).show();
        _loginButton.setEnabled(true);
    }

    private void verifyClientSwitchState(){
        if(clientProfileSwitch.isChecked())
        {
            changeSalerSwitchStatus(false);
        }
        else {
            changeSalerSwitchStatus(true);
        }
    }

    private void verifySalerSwitchState(){
        if(salerProfileSwitch.isChecked())
        {
            changeClientSwitchStatus(false);
        }
        else {
            changeClientSwitchStatus(true);
        }
    }
    private void changeSalerSwitchStatus(boolean status) {
        salerProfileSwitch.setChecked(status);
    }

    private void changeClientSwitchStatus(boolean status) {clientProfileSwitch.setChecked(status);}

    private boolean switchIsChecked(){
        return hasProfile() || salerProfileSwitch.isChecked() || clientProfileSwitch.isChecked();
    }

    private void saveTemporalProfile(){
        if(clientProfileSwitch.isChecked()){
            SharedPreferenceHandler.storeValue(getApplicationContext(),getString(R.string.temporal_profile),getString(R.string.client_profile));
        }else if(salerProfileSwitch.isChecked()){
            SharedPreferenceHandler.storeValue(getApplicationContext(),getString(R.string.temporal_profile),getString(R.string.saler_profile));
        }
    }


    @Override
    protected void processConcreteResponse(Response response) {
        if (response.isSuccess()) {
            String temporalProfile=SharedPreferenceHandler.getValue(getApplicationContext(),getString(R.string.temporal_profile));
            storeProfile(temporalProfile,response);
            changeActivityByProfile(temporalProfile);
        } else {
            SharedPreferenceHandler.removeValue(getApplicationContext(),getString(R.string.authentication_type));
            ShowErrorMessageHandler.showMessageError(LoginActivity.this, response.getMessage(), Toast.LENGTH_LONG);
        }
    }
}
