package uy.com.searchandfind.saler;

import com.google.gson.Gson;

import uy.com.searchandfind.Response;
import uy.com.searchandfind.service.backend.ResponseBuilder;

public class SalerResponseBuilder implements ResponseBuilder<SalerResponse>{
    private Gson gson;

    public SalerResponseBuilder(){
        gson=new Gson();
    }

    @Override
    public SalerResponse buildResponse(String jsonResponse) {
        return gson.fromJson(jsonResponse,SalerResponse.class);
    }

    @Override
    public SalerResponse buildBadResponse(String message) {
        return new SalerResponse(message);
    }
}
