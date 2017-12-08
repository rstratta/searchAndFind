package uy.com.searchandfind.service.backend;

import android.content.Context;
import android.os.StrictMode;
import android.util.Log;
import com.google.gson.Gson;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.StringCharacterIterator;

import uy.com.searchandfind.R;
import uy.com.searchandfind.Response;
import uy.com.searchandfind.SearchAndFindAuthenticationException;
import uy.com.searchandfind.sharedpreference.SharedPreferenceHandler;

/**
 * Created by strat on 30/09/2017.
 */

public class ServerBackendInvoker<A,B extends Response> {
    private final String HTTP_GET="GET";
    private final String HTTP_POST="POST";
    private final String HTTP_PUT="PUT";
    private final String HTTP_DELETE="DELETE";
    private final String [] HTTP_VERBS={HTTP_GET,HTTP_POST,HTTP_PUT, HTTP_DELETE};
    private ResponseBuilder<B> responseBuilder;
    private Gson gson;

    public ServerBackendInvoker(ResponseBuilder<B> responseBuilder){
        this.responseBuilder=responseBuilder;
        this.gson=new Gson();
    }

    public B invokeWithAuthentication(Context context,String urlToInvoke, A request, String httpVerb){
        B response=null;
        try {
            HttpURLConnection urlConnection=getAuthenticationURLConnection(context,urlToInvoke);
            urlConnection.setRequestMethod(getVerbFromArray(httpVerb));
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
            OutputStream outputStream = urlConnection.getOutputStream();
            outputStream.write(getBytesFromRequest(request));
            outputStream.flush();
            BufferedReader reader=new BufferedReader(new InputStreamReader(
                    urlConnection.getInputStream(), "UTF-8"));
            String responseJson=reader.readLine();
            reader.close();
            urlConnection.disconnect();
            response=responseBuilder.buildResponse(responseJson);
        }catch(Exception e){
            Log.e("Error", "_" + e.getMessage());
            response=responseBuilder.buildBadResponse("Error al conectar con servidor");
        }
        return response;
    }

    public B invokeWithAuthenticationWithoutRequest(Context context,String urlToInvoke, String httpVerb){
        B response=null;
        try {
            HttpURLConnection urlConnection=getAuthenticationURLConnection(context,urlToInvoke);
            urlConnection.setRequestMethod(getVerbFromArray(httpVerb));
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
            BufferedReader reader=new BufferedReader(new InputStreamReader(
                    urlConnection.getInputStream(), "UTF-8"));
            String responseJson=reader.readLine();
            reader.close();
            urlConnection.disconnect();
            response=responseBuilder.buildResponse(responseJson);
        }catch(Exception e){
            Log.e("Error", "_" + e.getMessage());
            response=responseBuilder.buildBadResponse("Error al conectar con servidor");
        }
        return response;
    }
    private boolean needConfigDoOutput(String httpVerb){
        return httpVerb.equals(HTTP_POST);
    }

    private HttpURLConnection getSimpleURLConnection(String urlToInvoke)throws Exception{
        URL url = new URL(urlToInvoke);
        HttpURLConnection urlConnection= (HttpURLConnection)url.openConnection();
        urlConnection.setRequestProperty("Content-Type", "application/json");
        return urlConnection;
    }

    private HttpURLConnection getAuthenticationURLConnection(Context context, String urlToInvoke)throws Exception{
        HttpURLConnection urlConnection= getSimpleURLConnection(urlToInvoke);
        urlConnection.setRequestProperty(context.getString(R.string.authentication_type),  SharedPreferenceHandler.getValue(context, context.getString(R.string.authentication_type)));
         urlConnection.setRequestProperty(context.getString(R.string.token),  SharedPreferenceHandler.getValue(context, context.getString(R.string.token)));
        return urlConnection;
    }

    private String getVerbFromArray(String httpVerb)throws Exception{
        for(int i=0; i<HTTP_VERBS.length; i++){
            if(httpVerb.equals(HTTP_VERBS[i])){
                return httpVerb;
            }
        }
        throw new Exception("Error interno. Se recibe verbo no habilitado");
    }

    private byte[] getBytesFromRequest(A request){
        String a = gson.toJson(request);
        return a.getBytes();
    }
}
