package uy.com.searchandfind;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by strat on 02/11/2017.
 */

public abstract class Observable {

    private static Map<String,List<Observer>> observersByTag;

    public static void addObserver(String tag,Observer obs){
        if(getObserversByTag().containsKey(tag)){
            getObserversByTag().get(tag).add(obs);
        }else{
            List<Observer> observers=new ArrayList<>();
            observers.add(obs);
            getObserversByTag().put(tag,observers);
        }
    }

    public static void removeObserver(String tag,Observer obs){
        if(getObserversByTag().containsKey(tag)){
            getObserversByTag().get(tag).remove(obs);
        }
        if(getObserversByTag().get(tag).isEmpty()){
            List<Observer> observers=getObserversByTag().get(tag);
            getObserversByTag().remove(observers);
        }
    }

    public static boolean hasObserver(String tag){
        return getObserversByTag().get(tag)!=null && !getObserversByTag().get(tag).isEmpty();
    }

    public static Map<String,List<Observer>> getObserversByTag() {
        if(observersByTag ==null)
            observersByTag =new HashMap();
        return observersByTag;
    }

    public static void notifyObservers(String tag){
        for(Observer obs : getObserversByTag().get(tag)){
            obs.update();
        }
    }
}
