package uy.com.searchandfind;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by strat on 02/11/2017.
 */

public class CacheManager  extends Observable{
    private static Map<String,Object> cacheMap;
    private static CacheManager instance;

    private CacheManager(){
        cacheMap=new HashMap();
    }

    public static CacheManager getInstance(){
        if(instance==null){
            instance=new CacheManager();
        }
        return instance;
    }


    public void bind(String key, Object value){
        cacheMap.put(key,value);
    }

    public Object lookup(String key){
        if(existKey(key)){
            return cacheMap.get(key);
        }
        return null;
    }

    public boolean existKey(String key){
        return cacheMap.get(key)!=null;
    }

    public void unbind(String key){
        if(cacheMap.containsKey(key)){
            cacheMap.remove(key);
        }
    }

    public void reset() {
        cacheMap=new HashMap<>();
    }
}
