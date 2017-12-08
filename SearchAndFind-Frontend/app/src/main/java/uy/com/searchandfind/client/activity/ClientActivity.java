package uy.com.searchandfind.client.activity;

import android.app.Fragment;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.location.Location;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapFragment;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

import java.util.ArrayList;
import java.util.List;

import butterknife.ButterKnife;
import butterknife.InjectView;
import uy.com.searchandfind.FieldValidator;
import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.SearchAndFindAuthenticationException;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.SearchAndFindActivity;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.category.CategoryDTO;
import uy.com.searchandfind.category.CategoryResponse;
import uy.com.searchandfind.category.CategoryResponseBuilder;
import uy.com.searchandfind.login.LoginActivity;
import uy.com.searchandfind.query.QueryDTO;
import uy.com.searchandfind.query.QueryRequest;
import uy.com.searchandfind.query.QueryResponse;
import uy.com.searchandfind.query.QueryResponseBuilder;
import uy.com.searchandfind.query.SalerAvailableForTenderDTO;
import uy.com.searchandfind.saler.SalerRequest;
import uy.com.searchandfind.service.AsyncResponse;
import uy.com.searchandfind.service.backend.ServerBackendController;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.service.cloud.location.LocationDTO;
import uy.com.searchandfind.service.cloud.location.LocationService;
import uy.com.searchandfind.service.cloud.location.LocationServiceException;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.tender.activity.TenderClientActivity;
import uy.com.searchandfind.tender.activity.TenderSalerActivity;
import uy.com.searchandfind.user.UserHandler;
import uy.com.searchandfind.user.UserRequest;

public class ClientActivity extends SearchAndFindActivity implements OnMapReadyCallback{

    @InjectView(R.id.categoryName) TextView categoryName;
    @InjectView(R.id.categoryDescription) TextView categoryDescription;
    @InjectView(R.id.categoryListView) ListView catListView;
    @InjectView(R.id.doQueryButton)Button doQueryButton;

    private LocationDTO locationDTO;
    private LocationService locationService;
    private GoogleMap myMap;
    private List<CategoryDTO>  categories;
    private List<String>  categoryByName;
    private ArrayAdapter<String> adapter;
    boolean isPendingQuery;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_client);
        ButterKnife.inject(this);
        locationService=new LocationService();
        sendCategoriesOperation(getString(R.string.getting_categories));
        SupportMapFragment mapFragment =(SupportMapFragment)getSupportFragmentManager().findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);
        refreshPendingQuery();
        if (isPendingQuery) {
            setButtomAppearance(false);
        }
        doQueryButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (isPendingQuery)
                    cancelQuery();
                else if (validateFields())
                {
                    sendQueryOperation(buildRequest(),getString(R.string.do_query_operation),getString(R.string.do_query_message));
                }
            }
        });
        createTextWatcher();
        addListenerOnCategoryListView();
    }

    private void cancelQuery() {
        sendQueryOperation(buildRequest(),getString(R.string.cancel_query_operation),getString(R.string.cancel_query_message));
     }

    private void cleanScreenAfterCancelQuery() {
        categoryName.setText("");
        categoryDescription.setText("");
        myMap.clear();
        getCurrentLocation();
        changeActionButton(true);
    }

    private void addListenerOnCategoryListView() {
        if(categories!=null && !categories.isEmpty()) {
            catListView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                    categoryName.setText((catListView.getItemAtPosition(i).toString()));
                    catListView.setVisibility(View.GONE);
                }
            });
        }
    }

    private void processQueryResponse(QueryResponse response) {
        QueryDTO query=response.getQueryDTO();
        for(SalerAvailableForTenderDTO saler : query.getSalers()){
            myMap.addMarker(buildTemporalSalerMarker(saler));
        }
        LatLng currentLocation = new LatLng(locationDTO.getLatitude(), locationDTO.getLength());
        myMap.animateCamera(CameraUpdateFactory.newLatLngZoom(currentLocation, 13.0f));
        changeActionButton(false);
        myMap.getUiSettings().setScrollGesturesEnabled(true);
    }

    private MarkerOptions buildTemporalSalerMarker(SalerAvailableForTenderDTO saler) {
        LatLng salerLocation = new LatLng(saler.getLatitude(), saler.getLength());
        return new MarkerOptions().position(salerLocation).title(saler.getShopName())
                .icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_YELLOW)).snippet("Esperando oferta!");
    }

    private void showMessageError(String message) {
        ShowErrorMessageHandler.showMessageError(this,message,Toast.LENGTH_LONG);
    }

    private QueryRequest buildRequest(){
        QueryRequest request=new QueryRequest();
        request.setCategory(categoryName.getText().toString());
        request.setDescritpion(categoryDescription.getText().toString());
        request.setLatitude(locationDTO.getLatitude());
        request.setLength(locationDTO.getLength());
        request.setUserId(UserHandler.getCurrentUserId(getApplicationContext()));
        return request;
    }

    private void sendQueryOperation(QueryRequest request, String operation, String message){
        final Object[] queryParams = buildQueryParams(request, operation);
        invokeBackend(queryParams, message, ClientActivity.this);
    }

    private void changeActionButton(boolean isQueryPending){
        if (isQueryPending)
        {
            setButtomAppearance(isQueryPending);
            SharedPreferenceHandler.removeValue(getApplicationContext(), getString(R.string.SHQueryPending));
        }
        else{
            setButtomAppearance(isQueryPending);
            SharedPreferenceHandler.storeValue(getApplicationContext(),getString(R.string.SHQueryPending),"1");
        }
        refreshPendingQuery();
    }
    private void refreshPendingQuery() {
        isPendingQuery = !SharedPreferenceHandler.getValue(getApplicationContext(), getString(R.string.SHQueryPending)).isEmpty();
    }
    private void setButtomAppearance(boolean isPendingQuery){
        if (isPendingQuery)
        {
            doQueryButton.setBackgroundColor(Color.parseColor("#00ABF5"));
            doQueryButton.setText("REALIZAR BÚSQUEDA");
        }else{
            doQueryButton.setBackgroundColor(Color.parseColor("#FF0000"));
            doQueryButton.setText("CANCELAR BÚSQUEDA");
        }
    }

    private Object[] buildQueryParams(QueryRequest request, String operation) {
        ServerBackendInvoker<QueryRequest, QueryResponse> invoker=new ServerBackendInvoker (new QueryResponseBuilder());
        String url=URLBuilder.buildPostURL(getString(R.string.query_resource), operation);
        return ServerBackendControllerUtil.buildQueryParams(invoker,url,getApplicationContext(),"POST",request);
    }

    private Object[] buildCategoriesParams() {
        ServerBackendInvoker<UserRequest, CategoryResponse> invoker=new ServerBackendInvoker (new CategoryResponseBuilder());
        String url=URLBuilder.buildGetURL(getString(R.string.category_resource),getString(R.string.all_categories),UserHandler.getCurrentUserId(getApplicationContext()));
        return ServerBackendControllerUtil.buildQueryParams(invoker,url,getApplicationContext(),"GET");
    }

    private void sendCategoriesOperation(String message){
        final Object[] queryParams=buildCategoriesParams();
        invokeBackend(queryParams,message,ClientActivity.this);
    }

    @Override
    protected void processConcreteResponse(Response response) {
        if(response.isAuthenticationError()){
            processAuthenticationException(response.getMessage());
        }else if(!response.isSuccess()){
            showMessageError(response.getMessage());
        }else{
            decideByResponseType(response);
        }
    }

    private void decideByResponseType(Response response) {
        if(response instanceof CategoryResponse){
            processAsCategoryResponse((CategoryResponse)response);
        }else{
            processAsQueryResponse((QueryResponse) response);
        }
    }

    private void processAsCategoryResponse(CategoryResponse response) {
        buildCategoryListFromResponse(response);
    }

    private void processAsQueryResponse(QueryResponse response) {
        if(response.getQueryDTO()!=null){
            processQueryResponse(response);
        }else{
            cleanScreenAfterCancelQuery();
        }
    }



    private boolean validateFields(){
        if(FieldValidator.isFieldEmpty(categoryName.getText().toString())){
            categoryName.setError(getString(R.string.need_input_category_name));
            categoryName.requestFocus();
            return false;
        }
        if(!FieldValidator.isNotNull(locationDTO)){
            ShowErrorMessageHandler.showMessageError(this,getString(R.string.location_error),Toast.LENGTH_LONG);
            return false;
        }
        return true;
    }
    private void createTextWatcher(){
        categoryName.addTextChangedListener(new TextWatcher() {

            @Override
            public void onTextChanged(CharSequence categoryInput, int arg1, int arg2, int arg3) {
                if(!categoryInput.toString().isEmpty()&&categories!=null){
                    fillCategoryListView();
                    searchCategory(categoryInput.toString());
                    catListView.setVisibility(View.VISIBLE);
                }else{
                    catListView.setVisibility(View.GONE);
                }
            }

            @Override
            public void beforeTextChanged(CharSequence arg0, int arg1, int arg2, int arg3) { }

            @Override
            public void afterTextChanged(Editable arg0) {}
        });
    }

    private void searchCategory(String category){
        for(CategoryDTO categoryDTO : categories){
            if(!categoryDTO.getCategoryName().toUpperCase().contains(category.toUpperCase())){
                categoryByName.remove(categoryDTO.getCategoryName());
            }
        }
        adapter.notifyDataSetChanged();
    }


    private void buildCategoryListFromResponse(CategoryResponse response) {
        categories=new ArrayList();
        for(int i=0;i<response.getCategories().length;i++){
            categories.add(response.getCategories()[i]);
        }
        addListenerOnCategoryListView();
    }

    private void buildCategoryNameList(){
        categoryByName=new ArrayList();
        for(CategoryDTO category : categories){
            categoryByName.add(category.getCategoryName());
        }
    }
    private void fillCategoryListView() {
        buildCategoryNameList();
        adapter = new ArrayAdapter<String>(this, R.layout.category_list_item, R.id.categoryItem, categoryByName);
        catListView.setAdapter(adapter);

    }

    private void processAuthenticationException(String message) {
        ShowErrorMessageHandler.showMessageError(this,message, Toast.LENGTH_LONG);
        processAuthenticationError(getApplicationContext());
        finish();
    }



    public void onMapReady(GoogleMap googleMap) {
        myMap=googleMap;
        myMap.getUiSettings().setScrollGesturesEnabled(false);
        if(locationService.hasPermissions(this)){
            getCurrentLocation();
        }
    }

    private void getCurrentLocation(){
        try {
            locationDTO = locationService.getCurrentLocation(this);
            LatLng currentLocation = new LatLng(locationDTO.getLatitude(), locationDTO.getLength());
            myMap.addMarker(new MarkerOptions().position(currentLocation).title("Tu posición!"));
            myMap.animateCamera(CameraUpdateFactory.newLatLngZoom(currentLocation, 15.0f));
        } catch (LocationServiceException e) {
            ShowErrorMessageHandler.showMessageError(this,e.getMessage(), Toast.LENGTH_LONG);
        }catch(Exception e){
            ShowErrorMessageHandler.showMessageError(this,getString(R.string.location_error), Toast.LENGTH_LONG);
        }
    }



    @Override
    public void onRequestPermissionsResult(int requestCode,
                                           String permissions[], int[] grantResults) {
        switch (requestCode) {
            case LocationService.REQUEST_PERMISSION_STATE: {
                if (grantResults.length > 0
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                   getCurrentLocation();
                } else {
                    ShowErrorMessageHandler.showMessageError(this,"No es posible completar la acción por falta de permisos",Toast.LENGTH_LONG);
                }
                return;
            }
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.client_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.history:
                ActivityHandler.changeActivity(getApplicationContext(),ClientHistoryActivity.class);
                return true;
            case R.id.client_logout:
                super.logoutUser(getApplicationContext());
                ActivityHandler.closeAllAndGo(getApplicationContext(), LoginActivity.class);
            default:
                return super.onOptionsItemSelected(item);
        }
    }



    @Override
    protected void onSaveInstanceState(Bundle outState)
    {
        super.onSaveInstanceState(outState);
    }



}
