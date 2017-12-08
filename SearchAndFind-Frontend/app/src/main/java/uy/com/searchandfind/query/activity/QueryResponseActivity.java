package uy.com.searchandfind.query.activity;

import android.app.AlertDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TabHost;
import android.widget.Toast;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

import java.util.List;

import butterknife.ButterKnife;
import butterknife.InjectView;
import uy.com.searchandfind.CacheManager;
import uy.com.searchandfind.Observer;
import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.SearchAndFindActivity;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.client.activity.ClientActivity;
import uy.com.searchandfind.query.QueryRequest;
import uy.com.searchandfind.query.QueryResponse;
import uy.com.searchandfind.query.QueryResponseBuilder;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.service.cloud.location.LocationDTO;
import uy.com.searchandfind.service.cloud.location.LocationService;
import uy.com.searchandfind.service.cloud.location.LocationServiceException;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.tender.TenderDTO;
import uy.com.searchandfind.tender.activity.TenderClientActivity;
import uy.com.searchandfind.user.UserHandler;

public class QueryResponseActivity extends SearchAndFindActivity implements OnMapReadyCallback, Observer {


    @InjectView(android.R.id.tabhost)   TabHost tabHost;
    @InjectView(R.id.query_result_list)   ListView listView;
    @InjectView(R.id.query_map_layout) LinearLayout queryMapLayout;

    private GoogleMap myMap;
    private LocationDTO locationDTO;
    private LocationService locationService;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_query_response);
        ButterKnife.inject(this);
        tabHost.setup();
        SupportMapFragment mapFragment =(SupportMapFragment)getSupportFragmentManager().findFragmentById(R.id.queryResultMap);
        mapFragment.getMapAsync(this);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            public void onItemClick(AdapterView parent, View view, int position, long id) {
                TenderDTO tenderDTO=(TenderDTO)parent.getAdapter().getItem(position);
                if(tenderDTO.getState()!="A")
                    goToTenderDetail(tenderDTO);
            }
        });


        tabHost.addTab(tabHost.newTabSpec(getString(R.string.lbl_query_map)).setIndicator(getString(R.string.lbl_query_map)).setContent(new TabHost.TabContentFactory() {
            public View createTabContent(String arg0) {
                return queryMapLayout;
            }
        }));
        tabHost.addTab(tabHost.newTabSpec(getString(R.string.lbl_query_list)).setIndicator(getString(R.string.lbl_query_list)).setContent(new TabHost.TabContentFactory() {
            public View createTabContent(String arg0) {
                return listView;
            }
        }));

        locationService=new LocationService();

        CacheManager.getInstance().addObserver(getString(R.string.tender_of_query),this);
        tabHost.setOnTabChangedListener(new TabHost.OnTabChangeListener() {
            @Override
            public void onTabChanged(String s) {
                update();
            }
        });
    }

    private void goToTenderDetail(TenderDTO tenderDTO) {
        CacheManager.getInstance().bind("currentTender",tenderDTO);
        ActivityHandler.changeActivity(getApplicationContext(), TenderClientActivity.class);
    }

    @Override
    public void onBackPressed(){
        AlertDialog alertbox = new AlertDialog.Builder(this)
                .setMessage("Desea cancelar la consulta en curso?")
                .setPositiveButton("Sí", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface arg0, int arg1) {
                        cancelAllTendersAndQuery();
                    }
                })
                .setNegativeButton("No", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface arg0, int arg1) {
                    }
                })
                .show();
    }

    private void cancelAllTendersAndQuery() {
        sendQueryOperation(buildRequest(),getString(R.string.cancel_query_operation),getString(R.string.cancel_query_message));
    }

    private QueryRequest buildRequest(){
        List<TenderDTO> tenders=(List<TenderDTO>)CacheManager.getInstance().lookup(getString(R.string.tender_of_query));
        QueryRequest request=new QueryRequest();
        request.setQueryId(tenders.get(0).getQueryId());
        request.setUserId(UserHandler.getCurrentUserId(getApplicationContext()));
        return request;
    }

    private void sendQueryOperation(QueryRequest request, String operation, String message){
        final Object[] queryParams = buildQueryParams(request, operation);
        invokeBackend(queryParams, message, QueryResponseActivity.this);
    }

    private Object[] buildQueryParams(QueryRequest request, String operation) {
        ServerBackendInvoker<QueryRequest, QueryResponse> invoker=new ServerBackendInvoker (new QueryResponseBuilder());
        String url= URLBuilder.buildPostURL(getString(R.string.query_resource), operation);
        return ServerBackendControllerUtil.buildQueryParams(invoker,url,getApplicationContext(),"POST",request);
    }

    @Override
    protected void onResume() {
        super.onResume();
        this.registerReceiver(mMessageReceiver, new IntentFilter(getString(R.string.tender_of_query)));
    }

    @Override
    protected void onPause() {
        super.onPause();
        this.unregisterReceiver(mMessageReceiver);
        CacheManager.getInstance().removeObserver(getString(R.string.tender_of_query),this);
    }

    private BroadcastReceiver mMessageReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            update();
        }
    };

    @Override
    public void onDestroy(){
        CacheManager.getInstance().removeObserver(getString(R.string.tender_of_query),this);
        CacheManager.getInstance().unbind(getString(R.string.tender_of_query));
        super.onDestroy();
    }
    @Override
    protected void processConcreteResponse(Response response) {
        if(response instanceof  QueryResponse){
            processCancelQuery((QueryResponse)response);
        }
    }

    private void processCancelQuery(QueryResponse response) {
        CacheManager.getInstance().removeObserver(getString(R.string.tender_of_query),this);
        CacheManager.getInstance().unbind(getString(R.string.tender_of_query));
        SharedPreferenceHandler.removeValue(getApplicationContext(), getString(R.string.SHQueryPending));
        ActivityHandler.closeAllAndGo(getApplicationContext(), ClientActivity.class);
        finish();
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        myMap=googleMap;
        myMap.getUiSettings().setScrollGesturesEnabled(false);
        if(locationService.hasPermissions(this)){
            update();
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
    public void update() {

        fillViewFromCache();
    }

    private void fillViewFromCache() {
        if(CacheManager.getInstance().existKey(getString(R.string.tender_of_query))){
            Object objectCache=CacheManager.getInstance().lookup(getString(R.string.tender_of_query));
            if(objectCache!=null) {
                List<TenderDTO> tenders = (List<TenderDTO>)objectCache;
                fillCurrentView(tenders);
                return;
            }
        }
            ShowErrorMessageHandler.showMessageError(this,"No se encontraron ofertas",Toast.LENGTH_LONG);

    }

    private void fillCurrentView(List<TenderDTO> tenders) {
        switch(tabHost.getCurrentTab()){
            case 0:
                fillMap(tenders);
                return;
            case 1:
                QueryResponseItemAdapter adapter=new QueryResponseItemAdapter(this,(List<TenderDTO>)CacheManager.getInstance().lookup(getString(R.string.tender_of_query)));
                listView.setAdapter(adapter);
                return;
        }
    }

    private void fillMap(List<TenderDTO> tenders) {
         myMap.clear();
        myMap.getUiSettings().setAllGesturesEnabled(true);
        getCurrentLocation();
        for(TenderDTO tenderDTO:tenders)
             myMap.addMarker(buildTemporalSalerMarker(tenderDTO));
        myMap.setIndoorEnabled(true);
        myMap.setOnInfoWindowClickListener(new GoogleMap.OnInfoWindowClickListener() {
            @Override
            public void onInfoWindowClick(Marker marker) {
                goToTenderDetailByCoords(marker.getPosition().latitude, marker.getPosition().longitude);
            }
        });
    }

    private void goToTenderDetailByCoords(double latitude, double longitude) {
        List<TenderDTO> tenders=(List<TenderDTO>)CacheManager.getInstance().lookup(getString(R.string.tender_of_query));
        TenderDTO result=null;
        for(TenderDTO dto:tenders){
            if(dto.getSalerDTO().getLatitude()==latitude && dto.getSalerDTO().getLength()==longitude){
                result=dto;
                break;
            }
        }
        if(result!=null && result.getState()!="A"){
            goToTenderDetail(result);
        }
    }

    private MarkerOptions buildTemporalSalerMarker(TenderDTO tenderDTO) {
        LatLng salerLocation = new LatLng(tenderDTO.getSalerDTO().getLatitude(), tenderDTO.getSalerDTO().getLength());
        return new MarkerOptions().position(salerLocation).title(tenderDTO.getSalerDTO().getShopName())
                .icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_GREEN)).snippet("Oferta $"+tenderDTO.getTenderAmount());
    }

}
