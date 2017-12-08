package uy.com.searchandfind.query;

import com.google.gson.Gson;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;

import java.lang.reflect.Type;

import uy.com.searchandfind.category.CategoryDTO;

/**
 * Created by strat on 24/10/2017.
 */

public class AvailableSalerTenderJsonDeserializer implements JsonDeserializer<SalerAvailableForTenderDTO> {

    private Gson gson;

    public AvailableSalerTenderJsonDeserializer() {
        gson=new Gson();
    }

    @Override
    public SalerAvailableForTenderDTO deserialize(JsonElement json, Type typeOfT, JsonDeserializationContext context) throws JsonParseException {
        return gson.fromJson(json,SalerAvailableForTenderDTO.class);
    }
}
