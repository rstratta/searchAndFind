package uy.com.searchandfind.category;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.ArrayList;
import java.util.List;

import uy.com.searchandfind.service.backend.ResponseBuilder;

/**
 * Created by strat on 23/10/2017.
 */

public class CategoryResponseBuilder implements ResponseBuilder<CategoryResponse> {
    private Gson gson;

    public CategoryResponseBuilder(){
       try {
           gson = new GsonBuilder().disableHtmlEscaping().serializeNulls().
                   registerTypeAdapter(List.class, new CategoryJsonDeserializer()).create();
       }catch(Exception e){
           e.printStackTrace();
       }
    }


    @Override
    public CategoryResponse buildResponse(String jsonResponse) {
        return gson.fromJson(jsonResponse,CategoryResponse.class);
    }

    @Override
    public CategoryResponse buildBadResponse(String message) {
        return new CategoryResponse(message);
    }
}
