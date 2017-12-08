package uy.com.searchandfind.client;

import com.google.gson.Gson;

import uy.com.searchandfind.service.backend.ResponseBuilder;

/**
 * Created by strat on 30/09/2017.
 */

public class ClientResponseBuilder implements ResponseBuilder<ClientResponse> {
    private Gson gson;

    public ClientResponseBuilder(){
        gson=new Gson();
    }
    @Override
    public ClientResponse buildResponse(String jsonResponse) {
        return gson.fromJson(jsonResponse, ClientResponse.class);

    }

    @Override
    public ClientResponse buildBadResponse(String message) {
        return new ClientResponse(message);
    }
}
