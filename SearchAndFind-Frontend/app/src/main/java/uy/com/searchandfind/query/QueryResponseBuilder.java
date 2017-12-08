package uy.com.searchandfind.query;


import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.text.DateFormat;
import java.util.ArrayList;
import java.util.List;
import uy.com.searchandfind.service.backend.ResponseBuilder;

/**
 * Created by strat on 24/10/2017.
 */

public class QueryResponseBuilder implements ResponseBuilder<QueryResponse> {
    private Gson gson;


    public QueryResponseBuilder(){
        gson=new GsonBuilder().disableHtmlEscaping().serializeNulls().
                registerTypeAdapter(List.class, new AvailableSalerTenderJsonDeserializer()).setDateFormat("EEE, dd MMM yyyy HH:mm:ss zzz").create();
    }
    @Override
    public QueryResponse buildResponse(String jsonResponse) {
        return gson.fromJson(jsonResponse,QueryResponse.class);
    }

    @Override
    public QueryResponse buildBadResponse(String message) {
       return new QueryResponse(message);
    }
}
