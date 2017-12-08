package uy.com.searchandfind.tender;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import uy.com.searchandfind.query.AvailableSalerTenderJsonDeserializer;
import uy.com.searchandfind.saler.SalerDTO;
import uy.com.searchandfind.service.backend.ResponseBuilder;

public class TenderResponseBuilder implements ResponseBuilder<TenderResponse> {
    private Gson gson;

    public TenderResponseBuilder(){
        gson=new Gson();

    }

    @Override
    public TenderResponse buildResponse(String jsonResponse) {
        return gson.fromJson(jsonResponse,TenderResponse.class);
    }

    @Override
    public TenderResponse buildBadResponse(String message) {
        return new TenderResponse(message);
    }
}
