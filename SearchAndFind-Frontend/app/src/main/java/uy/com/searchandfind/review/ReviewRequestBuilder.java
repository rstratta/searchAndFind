package uy.com.searchandfind.review;

import com.google.gson.Gson;

import uy.com.searchandfind.Response;
import uy.com.searchandfind.service.backend.ResponseBuilder;

/**
 * Created by Luis on 5/11/2017.
 */

public class ReviewRequestBuilder implements ResponseBuilder<Response> {

    private Gson gson;

    public ReviewRequestBuilder(){
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
