package uy.com.searchandfind.client.activity;

import android.app.Dialog;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.RatingBar;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.activity.ActivityHandler;
import uy.com.searchandfind.activity.SearchAndFindActivity;
import uy.com.searchandfind.activity.ShowErrorMessageHandler;
import uy.com.searchandfind.client.tenders.AdapterTender;
import uy.com.searchandfind.review.ReviewRequest;
import uy.com.searchandfind.review.ReviewRequestBuilder;
import uy.com.searchandfind.saler.activity.SalerActivity;
import uy.com.searchandfind.service.backend.ServerBackendControllerUtil;
import uy.com.searchandfind.service.backend.ServerBackendInvoker;
import uy.com.searchandfind.service.backend.URLBuilder;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;
import uy.com.searchandfind.tender.TenderDTO;
import uy.com.searchandfind.tender.TenderRequest;
import uy.com.searchandfind.tender.TenderResponse;
import uy.com.searchandfind.tender.TenderResponseBuilder;
import uy.com.searchandfind.user.UserHandler;

public class ClientHistoryActivity extends SearchAndFindActivity {

    private ServerBackendInvoker<TenderRequest,TenderResponse> serverInvokerTender;
    private ServerBackendInvoker<ReviewRequest,Response> serverInvokerReview;
    private List<TenderDTO> tenders;
    private boolean isSaler;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_client_history);
        isSaler = getProfile();
        setTitle();
        getHistory();
    }

    private boolean getProfile(){
        String profile = SharedPreferenceHandler.getValue(getApplicationContext(), getString(R.string.current_profile));
        return (profile.equals(getString(R.string.saler_profile)));
    }

    @Override
    public void onBackPressed(){
        super.onBackPressed();
       if (isSaler) {
           ActivityHandler.closeAllAndGo(getApplicationContext(), SalerActivity.class);
       }else{
           ActivityHandler.closeAllAndGo(getApplicationContext(), ClientActivity.class);
       }
    }

    private void setTitle(){
        if(isSaler)
            setTitle("Mis Ventas");
    }

    private void getHistory() {
        serverInvokerTender = new ServerBackendInvoker<TenderRequest, TenderResponse>(new TenderResponseBuilder());
        String idSaler = UserHandler.getCurrentUserId(getApplicationContext());
        String url;
        if(isSaler)
            url = URLBuilder.buildGetURL("tenders", "tenderBySaler", idSaler);
        else
            url = URLBuilder.buildGetURL("tenders", "tenderByClient", idSaler);
        Object[] params = ServerBackendControllerUtil.buildQueryParams(serverInvokerTender, url, getApplicationContext(), "GET");
        invokeBackend(params, getString(R.string.getting_history), ClientHistoryActivity.this);
    }

    @Override
    public void processConcreteResponse(Response response) {
        if(response.isAuthenticationError()){
            processAuthenticationException(response.getMessage());
        }else if(!response.isSuccess()){
            ShowErrorMessageHandler.showMessageError(this,response.getMessage(),Toast.LENGTH_LONG);
        }else{
            decideByResponseType(response);
        }
    }

    private void decideByResponseType(Response response) {
        if(response instanceof TenderResponse){
            processAsHistoryResponse((TenderResponse)response);
        }else{
            getHistory();
        }
    }

    private void processAsHistoryResponse (TenderResponse response){

        tenders = new ArrayList<TenderDTO>();
        tenders = response.getTenders();

        ListView lv = (ListView) findViewById(R.id.listView);

        AdapterTender adapter = new AdapterTender(this, tenders);

        lv.setAdapter(adapter);

        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                if ((tenders.get(position).getPointsFromSaler()== 0 && isSaler) || (tenders.get(position).getPointsFromClient() == 0 && !isSaler)) {
                    final int pos = position;
                    final Dialog rankDialog = new Dialog(ClientHistoryActivity.this);
                    rankDialog.setContentView(R.layout.activity_rating);
                    rankDialog.setCancelable(true);
                    final RatingBar ratingBar = (RatingBar) rankDialog.findViewById(R.id.dialog_ratingbar);

                    Button updateButton = (Button) rankDialog.findViewById(R.id.rank_dialog_button);
                    updateButton.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            ReviewRequest request = BuildRequest(((int) ratingBar.getRating()), pos);
                            processRating(request);
                            rankDialog.dismiss();
                        }
                    });
                    rankDialog.show();
                }
            }
        });
    }

    public void processRating(ReviewRequest request){
        serverInvokerReview = new ServerBackendInvoker<ReviewRequest, Response>(new ReviewRequestBuilder());
        String idSaler = UserHandler.getCurrentUserId(getApplicationContext());
        String url;
        if(isSaler)
            url = URLBuilder.buildPostURL("reviews", "saler");
        else
            url = URLBuilder.buildPostURL("reviews", "client");
        Object[] params = ServerBackendControllerUtil.buildQueryParams(serverInvokerReview,url,getApplicationContext(),"POST",request);
        invokeBackend(params,getString(R.string.waitingRating),ClientHistoryActivity.this);
    }

    private ReviewRequest BuildRequest (int stars, int pos){
        ReviewRequest request = new ReviewRequest();
        TenderDTO tenderToProcess = tenders.get(pos);
        request.setTenderId(tenderToProcess.getTenderId());
        request.setPoints(stars);
        request.setUserId(UserHandler.getCurrentUserId(getApplicationContext()));
        return request;
    }
    private void processAuthenticationException(String message) {
        ShowErrorMessageHandler.showMessageError(this,message, Toast.LENGTH_LONG);
        processAuthenticationError(getApplicationContext());
        finish();
    }
}
