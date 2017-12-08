package uy.com.searchandfind.user;

import com.google.gson.Gson;

import uy.com.searchandfind.Response;
import uy.com.searchandfind.service.backend.ResponseBuilder;

/**
 * Created by strat on 07/10/2017.
 */

public class SimpleUserResponseBuilder implements ResponseBuilder<Response> {
    private Gson gson;

    public SimpleUserResponseBuilder(){
        gson=new Gson();
    }
    @Override
    public Response buildResponse(String jsonResponse) {
        return gson.fromJson(jsonResponse,Response.class);
    }

    @Override
    public Response buildBadResponse(String message) {
        return new Response(message);
    }
}
