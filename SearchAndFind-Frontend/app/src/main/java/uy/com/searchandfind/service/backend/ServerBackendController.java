package uy.com.searchandfind.service.backend;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.os.AsyncTask;

import uy.com.searchandfind.Request;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.SearchAndFindAuthenticationException;
import uy.com.searchandfind.service.AsyncResponse;

/**
 * Created by strat on 30/10/2017.
 */

public class ServerBackendController extends AsyncTask<Object, Void,Response> {


    private AsyncResponse asyncResponse;

    public ServerBackendController(AsyncResponse asyncResponse){
        this.asyncResponse=asyncResponse;
    }

    @Override
    protected Response doInBackground(Object[] objects) {
        if(objects.length<4){
            return new Response("Error al conectar con servidor");
        }
        ServerBackendInvoker invoker=(ServerBackendInvoker)objects[0];
        String urlInvoker=(String)objects[1];
        Context context=(Context)objects[2];
        String httpVerb=(String)objects[3];
        Request request=null;

        if(objects.length>4){
            request=(Request)objects[4];
        }
        return invokeServer(invoker, urlInvoker, context, httpVerb, request);
    }

    @Override
    protected void onPostExecute(Response response){
        if(asyncResponse!=null){
            asyncResponse.processResponse(response);
        }
    }

    private Response invokeServer(ServerBackendInvoker invoker, String urlInvoker, Context context, String httpVerb, Request request) {
        if (request != null) {
            return invoker.invokeWithAuthentication(context, urlInvoker, request, httpVerb);
        } else {
            return invoker.invokeWithAuthenticationWithoutRequest(context, urlInvoker, httpVerb);
        }
    }
}
