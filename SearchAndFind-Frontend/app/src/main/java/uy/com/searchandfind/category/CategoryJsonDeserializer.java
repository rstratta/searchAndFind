package uy.com.searchandfind.category;

import com.google.gson.Gson;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;

import java.lang.reflect.Type;

/**
 * Created by strat on 24/10/2017.
 */

public class CategoryJsonDeserializer implements JsonDeserializer<CategoryDTO> {
    private Gson gson;

    public CategoryJsonDeserializer(){
        gson=new Gson();
    }

    @Override
    public CategoryDTO deserialize(JsonElement json, Type typeOfT, JsonDeserializationContext context) throws JsonParseException {
        return gson.fromJson(json,CategoryDTO.class);
    }
}
