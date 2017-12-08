package uy.com.searchandfind.tender.activity;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.support.v4.app.ActivityCompat;
import android.os.Bundle;
import android.util.Base64;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import butterknife.ButterKnife;
import butterknife.InjectView;
import uy.com.searchandfind.CacheManager;
import uy.com.searchandfind.FieldValidator;
import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.SearchAndFindActivity;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.client.activity.ClientActivity;
import uy.com.searchandfind.query.activity.QueryResponseActivity;
import uy.com.searchandfind.saler.SalerDTO;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.tender.TenderDTO;
import uy.com.searchandfind.tender.TenderRequest;
import uy.com.searchandfind.tender.TenderResponse;
import uy.com.searchandfind.tender.TenderResponseBuilder;
import uy.com.searchandfind.user.UserHandler;
import uk.co.senab.photoview.PhotoViewAttacher;


public class TenderClientActivity extends SearchAndFindActivity {

    private ServerBackendInvoker<TenderRequest, TenderResponse> serverInvoker;
    @InjectView(R.id.textMessageTenderResponse) TextView _description;
    @InjectView(R.id.textAmountTenderResponse) TextView _amount;
    @InjectView(R.id.btn_call) Button _btncallButton;
    @InjectView(R.id.btn_Confirm) Button _btnConfirmButton;
    @InjectView(R.id.image) ImageView _image;
    SalerDTO saler;
    TenderRequest tenderRequest;
    private PhotoViewAttacher photoViewAttacher ;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_tender_client);
        ButterKnife.inject(this);
        tenderRequest = new TenderRequest();
        _btncallButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                    callUser();
            }
        });
        _btnConfirmButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                confirmTender();
            }
        });
        showMessageTender();


    }
    private void confirmTender(){
        serverInvoker =  new ServerBackendInvoker<TenderRequest, TenderResponse>(new TenderResponseBuilder());
        String url= URLBuilder.buildGetURL("tenders",getString(R.string.confirm_tender_saler),"");
        Object[] params= ServerBackendControllerUtil.buildQueryParams(serverInvoker,url,getApplicationContext(),"POST",tenderRequest);
        invokeBackend(params, getString(R.string.confirm_mesage_saler),TenderClientActivity.this);
    }
    private void showMessageTender(){
        serverInvoker =  new ServerBackendInvoker<TenderRequest, TenderResponse>(new TenderResponseBuilder());
        String idClient = UserHandler.getCurrentUserId(getApplicationContext());
        TenderDTO dto= (TenderDTO)CacheManager.getInstance().lookup("currentTender");
        String idTender =dto.getTenderId();
        tenderRequest.setTenderId(idTender);
        tenderRequest.setUserId(idClient);
        tenderRequest.setQueryId(dto.getQueryId());
        String url= URLBuilder.buildGetURL("tenders",getString(R.string.view_tender_saler),"");
        Object[] params= ServerBackendControllerUtil.buildQueryParams(serverInvoker,url,getApplicationContext(),"POST",tenderRequest);
        invokeBackend(params, getString(R.string.getting_mesage_saler),TenderClientActivity.this);

    }
    private void processTenderResponse(TenderResponse responseTender) {
        TenderDTO tender = responseTender.getTenderDTO();
        if(tender.getState().equals("A")){
            CacheManager.getInstance().unbind(getString(R.string.tender_of_query));
            SharedPreferenceHandler.removeValue(getApplicationContext(), getString(R.string.SHQueryPending));

            ActivityHandler.closeAllAndGo(getApplicationContext(), ClientActivity.class);
            finish();
        }else {
            saler = tender.getSalerDTO();
            if (!FieldValidator.isFieldEmpty(tender.getTenderAmount())) {
                _amount.setText("Precio: $ " + tender.getTenderAmount() + "");
            }
            if (!FieldValidator.isFieldEmpty(tender.getDescription())) {
                _description.setText("Mensaje: " + tender.getDescription());
            }
            if (tender.getImages().size() > 0) {
                String data = tender.getImages().toArray()[0].toString();
                byte[] byteData = Base64.decode(data, Base64.DEFAULT);
                Bitmap photo = BitmapFactory.decodeByteArray(byteData, 0, byteData.length);
                _image.setImageBitmap(photo);
                photoViewAttacher = new PhotoViewAttacher(_image);
            }
        }

    }
    private void callUser(){
        if(!FieldValidator.isFieldEmpty(saler.getShopPhone())) {
            Intent i = new Intent(Intent.ACTION_CALL, Uri.parse("tel:" +saler.getShopPhone()));
            if (ActivityCompat.checkSelfPermission(TenderClientActivity.this, Manifest.permission.CALL_PHONE) != PackageManager.PERMISSION_GRANTED) {
                return;
            }
            startActivity(i);
        }

    }

    private void processAuthenticationException(String message) {
        ShowErrorMessageHandler.showMessageError(this,message, Toast.LENGTH_LONG);
        processAuthenticationError(getApplicationContext());
        finish();
    }
    @Override
    protected void processConcreteResponse(Response response) {
        if(response.isAuthenticationError()) {
            processAuthenticationException(response.getMessage());
        }else if(!response.isSuccess()){
            ShowErrorMessageHandler.showMessageError(TenderClientActivity.this,response.getMessage(), Toast.LENGTH_LONG);
        }else{
            processTenderResponse((TenderResponse)response);
        }
    }

}
