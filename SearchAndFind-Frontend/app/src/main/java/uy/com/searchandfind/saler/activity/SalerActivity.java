package uy.com.searchandfind.saler.activity;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Color;
import android.support.annotation.NonNull;
import android.support.v7.app.AlertDialog;
import android.os.Bundle;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.zip.Inflater;

import butterknife.ButterKnife;
import butterknife.InjectView;
import uy.com.searchandfind.CacheManager;
import uy.com.searchandfind.Observer;
import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.SearchAndFindAuthenticationException;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.SearchAndFindActivity;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.category.CategoryDTO;
import uy.com.searchandfind.client.activity.ClientHistoryActivity;
import uy.com.searchandfind.login.LoginActivity;
import uy.com.searchandfind.query.QueryDTO;
import uy.com.searchandfind.saler.SalerCategoryDTO;
import uy.com.searchandfind.saler.SalerRequest;
import uy.com.searchandfind.saler.SalerResponse;
import uy.com.searchandfind.saler.SalerResponseBuilder;
import uy.com.searchandfind.saler.SettingsSalerActivity;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.tender.activity.TenderSalerActivity;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.user.UserHandler;

public class SalerActivity extends SearchAndFindActivity implements Observer{

    private ServerBackendInvoker<SalerRequest, SalerResponse> serverInvoker;
    private Collection<SalerCategoryDTO> listCategoriesRequest;
    private boolean[] checkedCategories;
    private String[] listCategories;
    private ArrayList<Integer> positionsCategories = new ArrayList<>();
    @InjectView(R.id.pending_query_list) ListView listView;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_saler);
        ButterKnife.inject(this);
        setEmptyView();
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position, long l) {
                goToTender((QueryDTO)listView.getAdapter().getItem(position));
            }
        });
        CacheManager.getInstance().addObserver(getString(R.string.pending_queries),this);
        update();
    }



    private void setEmptyView() {
        TextView emptyView = new TextView(getApplicationContext());
        emptyView.setLayoutParams(new ViewGroup.LayoutParams(ViewGroup.LayoutParams.FILL_PARENT, ViewGroup.LayoutParams.FILL_PARENT));
        emptyView.setText("Usted no tiene consultas pendientes");
        emptyView.setVisibility(View.GONE);
        emptyView.setGravity(Gravity.CENTER_VERTICAL | Gravity.CENTER_HORIZONTAL);
        ((ViewGroup)listView.getParent()).addView(emptyView);
        listView.setEmptyView(emptyView);
    }

    @Override
    protected void onResume() {
        super.onResume();
        this.registerReceiver(mMessageReceiver, new IntentFilter(getString(R.string.pending_queries)));
        listView.deferNotifyDataSetChanged();
    }

    @Override
    protected void onPause() {
        super.onPause();
        this.unregisterReceiver(mMessageReceiver);
    }

    private BroadcastReceiver mMessageReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            update();
        }
    };



    private void goToTender(QueryDTO query) {
        CacheManager.getInstance().bind("currentQuery", query);
        ActivityHandler.changeActivity(getApplicationContext(), TenderSalerActivity.class);
    }


    private void ConfigureAccount(){
        ActivityHandler.changeActivity(getApplicationContext(),SettingsSalerActivity.class);
    }

    private void TransactionHistory(){
        ActivityHandler.changeActivity(getApplicationContext(),ClientHistoryActivity.class);
    }


    private void sendSalerCategoryRequest() throws SearchAndFindAuthenticationException {
        Collection<SalerCategoryDTO> listToReturn = createUpdateCategoryResponse();
        SalerRequest salerRequest = new SalerRequest();
        String idSaler = UserHandler.getCurrentUserId(getApplicationContext());
        salerRequest.setUserId(idSaler);
        salerRequest.setSalerCategoryDTO(listToReturn);
        String url=URLBuilder.buildPostURL("salers",getString(R.string.update_categories_from_saler));
        Object[] params=ServerBackendControllerUtil.buildQueryParams(serverInvoker,url,getApplicationContext(),"PUT",salerRequest);
        invokeBackend(params,getString(R.string.updating_categories),SalerActivity.this);
    }

    private Collection<SalerCategoryDTO> createUpdateCategoryResponse(){
        boolean hadCategory;
        Collection<SalerCategoryDTO> listCategoriesResponse = copySalerCategoryDTOCollection(listCategoriesRequest);
        for(int i=0; i < checkedCategories.length;i++){
            hadCategory = ((List<SalerCategoryDTO>)listCategoriesResponse).get(i).getHasCategory();

            if((!checkedCategories[i] && hadCategory)||(checkedCategories[i] && !hadCategory)){
                ((List<SalerCategoryDTO>)listCategoriesResponse).get(i).setIsUpdated(true);
            }else {
                ((List<SalerCategoryDTO>)listCategoriesResponse).get(i).setIsUpdated(false);
            }

        }
        return listCategoriesResponse;
    }
    private Collection<SalerCategoryDTO> copySalerCategoryDTOCollection(Collection<SalerCategoryDTO> list){
        Collection<SalerCategoryDTO> listToReturn = new ArrayList<SalerCategoryDTO>();
        for(SalerCategoryDTO salerCategoryDTO : list){
            listToReturn.add(salerCategoryDTO);
        }
        return listToReturn;
    }
    private String[] convertToStringArray(Collection<SalerCategoryDTO> listCategoriesResponse) {
        String[] listToReturn = new String[listCategoriesResponse.size()];
        int i = 0;
        for(SalerCategoryDTO salerCategoryDto : listCategoriesResponse){
            listToReturn[i] = salerCategoryDto.getCategory().getCategoryName();
            i++;
        }
        return listToReturn;


    }
    private boolean[] convertToBooleanArray(Collection<SalerCategoryDTO> listCategoriesResponse) {
        boolean[] listToReturn = new boolean[listCategoriesResponse.size()];
        int i = 0;
        for(SalerCategoryDTO salerCategoryDto : listCategoriesResponse){
            listToReturn[i] = salerCategoryDto.getHasCategory();
            i++;
        }
        return listToReturn;


    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.saler_menu, menu);
        return true;
    }

    private void showCategories(){
        serverInvoker =  new ServerBackendInvoker<SalerRequest,SalerResponse>(new SalerResponseBuilder());
        String idSaler = UserHandler.getCurrentUserId(getApplicationContext());
        String url=URLBuilder.buildGetURL("salers",getString(R.string.categories_from_saler),idSaler);
        Object[] params= ServerBackendControllerUtil.buildQueryParams(serverInvoker,url,getApplicationContext(),"GET");
        invokeBackend(params, getString(R.string.getting_categories),SalerActivity.this);
    }

    private void processSalerResponse(SalerResponse response) {
        if(response.getSalerCategoryDTO()!=null && !response.getSalerCategoryDTO().isEmpty()){
            listCategoriesRequest = response.getSalerCategoryDTO();
            listCategories = convertToStringArray(listCategoriesRequest);
            checkedCategories = convertToBooleanArray(listCategoriesRequest);
            AlertDialog.Builder mBuilder = getCategoryAlertBuilder();
            configCategoryAlertBuilder(mBuilder);
            AlertDialog mDialog = mBuilder.create();
            mDialog.show();
        }
    }

    private void configCategoryAlertBuilder(AlertDialog.Builder mBuilder) {
        mBuilder.setCancelable(false);
        mBuilder.setPositiveButton(R.string.btnCategoryOk, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int which) {
                String category = "";
                for (int i = 0; i < positionsCategories.size(); i++) {
                    category = category + listCategories[positionsCategories.get(i)];
                    if (i != positionsCategories.size() - 1) {
                        category = category + ", ";
                    }
                }
            try{
                    sendSalerCategoryRequest();
            }catch(SearchAndFindAuthenticationException e){
                processAuthenticationException(e.getMessage());
            }

            }
        });
        mBuilder.setNegativeButton(R.string.btnCategoryDismiss, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
                dialogInterface.dismiss();
            }
        
        });

    }

    @NonNull
    private AlertDialog.Builder getCategoryAlertBuilder() {
        AlertDialog.Builder mBuilder = new AlertDialog.Builder(SalerActivity.this);
        mBuilder.setTitle(R.string.btnCategoryTitle);
        mBuilder.setMultiChoiceItems(listCategories, checkedCategories, new DialogInterface.OnMultiChoiceClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int position, boolean isChecked) {
                if (isChecked) {
                    if (!positionsCategories.contains(position)) {
                        positionsCategories.add(position);
                    }
                } else if (positionsCategories.contains(position)) {
                    int positionToDelete = positionsCategories.indexOf(new Integer(position));
                    positionsCategories.remove(positionToDelete);
                }

            }
        });
        return mBuilder;
    }


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.confAccount:
                ConfigureAccount();
                return true;
            case R.id.confCategories:
                showCategories();
                return true;
            case R.id.history:
                TransactionHistory();
                return true;
            case R.id.saler_logout:
                super.logoutUser(getApplicationContext());
                ActivityHandler.closeAllAndGo(getApplicationContext(), LoginActivity.class);
            default:
                return super.onOptionsItemSelected(item);
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
            ShowErrorMessageHandler.showMessageError(SalerActivity.this,response.getMessage(), Toast.LENGTH_LONG);
        }else{
            processSalerResponse((SalerResponse)response);
        }
    }

    @Override
    public void update() {
        if(CacheManager.getInstance().lookup(getString(R.string.pending_queries))!=null) {
            listView.setAdapter(new PendingSalerQueriesAdapter(this, (List<QueryDTO>) CacheManager.getInstance().lookup(getString(R.string.pending_queries)), getApplicationContext()));
        }
    }
}
