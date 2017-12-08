package uy.com.searchandfind.service.backend;

import android.content.Context;
import uy.com.searchandfind.Request;


/**
 * Created by strat on 30/10/2017.
 */

public class ServerBackendControllerUtil {


    public static Object[] buildQueryParams(ServerBackendInvoker serverInvoker, String url, Context context, String httpVerb) {
        Object[] params=new Object[5];
        fillParams(params,serverInvoker, url,context,httpVerb);
        return params;
    }

    public static Object[] buildQueryParams(ServerBackendInvoker serverInvoker, String url, Context context, String httpVerb, Request request) {
        Object[] params=new Object[5];
        fillParams(params,serverInvoker,url,context,httpVerb);
        params[4]=request;
        return params;
    }

    private static void fillParams(Object[] params, ServerBackendInvoker serverInvoker, String url, Context context, String httpVerb){
        params[0]=serverInvoker;
        params[1]=url;
        params[2]=context;
        params[3]=httpVerb;
    }
}
