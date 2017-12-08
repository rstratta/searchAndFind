package uy.com.searchandfind.main;


import android.nfc.Tag;
import android.os.AsyncTask;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthCredential;
import com.google.firebase.auth.EmailAuthProvider;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;

import uy.com.searchandfind.R;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.client.activity.ClientActivity;
import uy.com.searchandfind.login.LoginActivity;

import uy.com.searchandfind.query.activity.QueryResponseActivity;
import uy.com.searchandfind.saler.activity.SalerActivity;
import uy.com.searchandfind.service.cloud.message.MessageHandler;
import uy.com.searchandfind.service.cloud.register.ServiceCloudRegister;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;


public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        AsyncTask task = new DecideNextActivity();
        task.execute(this);
        finish();
    }

    private class DecideNextActivity extends AsyncTask{

        @Override
        protected String doInBackground(Object[] objects) {
            decideNextActivity();
            return "success";
        }

        private void decideNextActivity() {
            String profile= SharedPreferenceHandler.getValue(getApplicationContext(),getString(R.string.current_profile));
            String currentToken=SharedPreferenceHandler.getValue(getApplicationContext(),getString(R.string.token));
            if(!currentToken.isEmpty()){
                decideNextActivityByProfile(profile);
            }else{
                ActivityHandler.changeActivity(getApplicationContext(), LoginActivity.class);
            }
        }


        private void decideNextActivityByProfile(String profile) {
            String clientProfile=getString(R.string.client_profile);
            String salerProfile=getString(R.string.saler_profile);
            if(profile.equals(clientProfile)){
                ActivityHandler.changeActivity(getApplicationContext(), ClientActivity.class);
            }else  if(profile.equals(salerProfile)){
                ActivityHandler.changeActivity(getApplicationContext(), SalerActivity.class);
            }
        }
    }
}
