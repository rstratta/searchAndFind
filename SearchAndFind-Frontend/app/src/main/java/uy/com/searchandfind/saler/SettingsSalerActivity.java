package uy.com.searchandfind.saler;

import android.content.Context;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.app.WindowDecorActionBar;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import butterknife.ButterKnife;
import butterknife.InjectView;
import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.SearchAndFindActivity;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.service.backend.ServerBackendController;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.service.cloud.location.LocationDTO;
import uy.com.searchandfind.service.cloud.location.LocationService;
import uy.com.searchandfind.service.cloud.location.LocationServiceException;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.user.UserDTO;
import uy.com.searchandfind.user.UserHandler;
import uy.com.searchandfind.*;
import uy.com.searchandfind.user.UserRequest;


public class SettingsSalerActivity extends SearchAndFindActivity {

    private ServerBackendInvoker<SalerRequest, SalerResponse> serverInvoker;
    String localName;
    EditText _localName  ;
    EditText _localPhone ;
    EditText _open       ;
    EditText _close      ;
    CheckBox _monday     ;
    CheckBox _tuesday    ;
    CheckBox _wednesday  ;
    CheckBox _thursday   ;
    CheckBox _friday     ;
    CheckBox _saturday   ;
    CheckBox _sunday     ;
    int  localPhone, open, close;
    boolean monday, tuesday, wednesday, thursday, friday, saturday, sunday;
    boolean areValidFields;
    private LocationService locationService;
    private LocationDTO locationDTO;
    private boolean isLoadConfiguration;
    @InjectView(R.id.locationButton)    ImageButton locationButton;
    @InjectView(R.id.shop_address) TextView shopAddress;
    private SalerDTO saler;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings_saler);
        locationService=new LocationService();
        locationDTO=null;
        Button _btnSave = (Button) findViewById(R.id.btn_save);
        _localName = (EditText) findViewById(R.id.local_name);
        _localPhone = (EditText) findViewById(R.id.telephone_number);
        _open = (EditText) findViewById(R.id.open);
        _close = (EditText) findViewById(R.id.close);
        _monday = (CheckBox) findViewById(R.id.monday);
        _tuesday = (CheckBox) findViewById(R.id.tuesday);
        _wednesday = (CheckBox) findViewById(R.id.wednesday);
        _thursday = (CheckBox) findViewById(R.id.thursday);
        _friday = (CheckBox) findViewById(R.id.friday);
        _saturday = (CheckBox) findViewById(R.id.saturday);
        _sunday = (CheckBox) findViewById(R.id.sunday);

        _btnSave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                asignation();
                areValidFields = validateFields();
                if (areValidFields)
                {
                SalerRequest request = buildRequest();
                saveChanges(request);
                }
            }
        });
        ButterKnife.inject(this);
        locationButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){
                getCurrentLocation();
            }
        });
        fillView();
    }

    private void fillView() {
            getSalerById();

    }

    private void fillBySaler(SalerDTO saler) {
        if(!FieldValidator.isFieldEmpty(saler.getShopName())){
            _localName.setText(saler.getShopName());
        }
        if(!FieldValidator.isFieldEmpty(saler.getShopPhone())){
            _localPhone.setText(String.valueOf(saler.getShopPhone()));
        }
        if(saler.getLatitude()!=0){
            fillShopAddressFromLocation(saler.getLatitude(),saler.getLength());
        }
        if(saler.getShopHourOpen()>=0 && !FieldValidator.isFieldEmpty(saler.getShopName())){
            _open.setText(String.valueOf(saler.getShopHourOpen()));
            _close.setText(String.valueOf(saler.getShopHourClose()));
        }
        checkBoxs(saler);

    }


    private void checkBoxs(SalerDTO saler) {
        if(saler.getShopDaysOpen().contains("L")){
            _monday.setChecked(true);
        }
        if(saler.getShopDaysOpen().contains("M")){
            _tuesday.setChecked(true);
        }
        if(saler.getShopDaysOpen().contains("X")){
            _wednesday.setChecked(true);
        }
        if(saler.getShopDaysOpen().contains("J")){
            _thursday.setChecked(true);
        }
        if(saler.getShopDaysOpen().contains("V")){
            _friday.setChecked(true);
        }
        if(saler.getShopDaysOpen().contains("S")){
            _saturday.setChecked(true);
        }
        if(saler.getShopDaysOpen().contains("D")){
            _sunday.setChecked(true);
        }
    }

     private void asignation(){
        _localName.setError(null);
        _localPhone.setError(null);
        _monday.setError(null);
        _open.setError(null);
        _close.setError(null);
        if (!TextUtils.isEmpty(_localPhone.getText().toString()))
            localPhone  = Integer.parseInt(_localPhone.getText().toString());
        if (!TextUtils.isEmpty(_open.getText().toString()))
            open        = Integer.parseInt(_open.getText().toString());
        if (!TextUtils.isEmpty(_close.getText().toString()))
            close       = Integer.parseInt(_close.getText().toString());
        localName   = _localName.getText().toString();
        monday      = _monday.isChecked();
        tuesday     = _tuesday.isChecked();
        wednesday   = _wednesday.isChecked();
        thursday    = _thursday.isChecked();
        friday      = _friday.isChecked();
        saturday    = _saturday.isChecked();
        sunday      = _sunday.isChecked();

    }
    private boolean validateFields(){
        boolean isValid = true;
        if (FieldValidator.isFieldEmpty(localName)){
            _localName.setError("Debe ingresar un nombre de local");
            _localName.requestFocus();
            return false;
        }
        if (FieldValidator.isFieldEmpty(localPhone)){
            _localPhone.setError("Debe ingresar un número telefónico");
            _localPhone.requestFocus();
            return false;
        }
        if (!_monday.isChecked()&&!_tuesday.isChecked()&&!_wednesday.isChecked()&&!_thursday.isChecked()&&!_friday.isChecked()&&!_saturday.isChecked()&&!_sunday.isChecked()){
            _sunday.setError("_");
            ShowErrorMessageHandler.showMessageError(SettingsSalerActivity.this,"Debe ingresar al menos un dia de apertura de local", Toast.LENGTH_LONG);
            _sunday.requestFocus();
            return false;
        }
        if (!FieldValidator.isValidHour(open) || !FieldValidator.isValidHour(close)){
            _open.setError("La hora de apertura y cierre debe ser entre 0 y 24");
            _open.requestFocus();
            return false;
        }
        if ( close <= open){
            _open.setError("La hora de apertura del local debe ser menor a la de cierre");
            _open.requestFocus();
            return false;
        }
        if(locationDTO==null && FieldValidator.isFieldEmpty(shopAddress.getText().toString())){
            shopAddress.setError("Debe indicar una dirección de local");
            shopAddress.requestFocus();
            return false;
        }
        return isValid;
    }
    private SalerRequest buildRequest(){

        SalerRequest request = new SalerRequest(saler);
        request.setShopName(_localName.getText().toString());
        request.setMail(SharedPreferenceHandler.getSalerMail(getApplicationContext()));
        request.setShopPhone(_localPhone.getText().toString());
        request.setShopHourOpen(open);
        request.setShopHourClose(close);
        request.setShopDaysOpen(getStringDays());
        request.setShopAddress(shopAddress.getText().toString());
        try {
                locationDTO=locationService.getLocationFromAddress(getApplicationContext(),shopAddress.getText().toString());
                request.setLatitude(locationDTO.getLatitude());
                request.setLength(locationDTO.getLength());
            } catch (IOException e) {
                ShowErrorMessageHandler.showMessageError(this,"No es posible obtener su ubicación",Toast.LENGTH_LONG);
            }
        return request;
    }

    private void processAuthenticationException(String message) {
        ShowErrorMessageHandler.showMessageError(this,message, Toast.LENGTH_LONG);
        processAuthenticationError(getApplicationContext());
        finish();
    }


    private String getStringDays(){
        String days = "";
        if (monday)
            days += "L";
        if (tuesday)
            days += "M";
        if (wednesday)
            days += "X";
        if (thursday)
            days += "J";
        if (friday)
            days += "V";
        if (saturday)
            days += "S";
        if (sunday)
            days += "D";
        return days;
    }

    private void saveChanges(SalerRequest request) {
        serverInvoker = new ServerBackendInvoker<SalerRequest, SalerResponse>(new SalerResponseBuilder());
        String url=URLBuilder.buildPostURL("salers",getString(R.string.update_saler_account_operation));
        Object[] params= ServerBackendControllerUtil.buildQueryParams(serverInvoker,url,getApplicationContext(),"POST",request);
        invokeBackend(params,getString(R.string.updating_account),SettingsSalerActivity.this);
    }



   private void getCurrentLocation(){
        try {
            if(locationService.hasPermissions(this)) {
                locationDTO = locationService.getCurrentLocation(this);
                fillShopAddressFromLocation(locationDTO.getLatitude(), locationDTO.getLength());
            }
        }catch(LocationServiceException e){
            ShowErrorMessageHandler.showMessageError(this,e.getMessage(),Toast.LENGTH_LONG);
        }
    }

    private void fillShopAddressFromLocation(double latitude, double length) {
        try {
            String address = locationService.getAddressFromLocation(this, latitude, length);
            shopAddress.setText(address);
            locationDTO=new LocationDTO(latitude,length);
        }catch(IOException e){
            ShowErrorMessageHandler.showMessageError(SettingsSalerActivity.this,"Error inesperado, reintente...", Toast.LENGTH_LONG);
        }
    }

    @Override
    protected void processConcreteResponse(Response response) {
        if(response.isAuthenticationError()){
            processAuthenticationException(response.getMessage());
        }else if(!response.isSuccess()) {
            ShowErrorMessageHandler.showMessageError(SettingsSalerActivity.this,response.getMessage(), Toast.LENGTH_LONG);
        }else{
            processByResponseType(response);
        }
    }

    public void processByResponseType(Response response){
        if(isLoadConfiguration){
            isLoadConfiguration = false;
            saler=((SalerResponse) response).getSalerDTO();
            fillBySaler(saler);
        }else{
            ShowErrorMessageHandler.showMessageError(SettingsSalerActivity.this,getString(R.string.update_account_success), Toast.LENGTH_LONG);
            this.finish();
        }
    }

    private void getSalerById()  {
        isLoadConfiguration = true;
        ServerBackendInvoker<UserRequest, SalerResponse> serverInvoker=new ServerBackendInvoker(new SalerResponseBuilder());
        String url=URLBuilder.buildGetURL("salers","saler",UserHandler.getCurrentUserId(getApplicationContext()));
        Object[] params= ServerBackendControllerUtil.buildQueryParams(serverInvoker,url,getApplicationContext(),"GET");
        invokeBackend(params, getString(R.string.getting_user),SettingsSalerActivity.this);
    }
}
