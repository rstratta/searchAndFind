package uy.com.searchandfind.service.backend;

/**
 * Created by strat on 30/09/2017.
 */

public class URLBuilder {
    private final static String HTTP_PROTOCOL="http://";
    private final static String BASE_URL="/api/";
    private final static String SEPARATOR=":";
    private final static String SLASH="/";

    public static String buildGetURL(String resource, String action, String filter) {
        StringBuilder builder=new StringBuilder();
        builder.append(getResourceURL(resource));
        if(filter.isEmpty()){
            builder.append(action);
        }else{
            builder.append(filter).append(SLASH).append(action);
        }
        return builder.toString();
    }

    public static String buildPostURL(String resource, String action) {
        StringBuilder builder=new StringBuilder();
        builder.append(getResourceURL(resource)).append(action);
        return builder.toString();
    }

    private static String getResourceURL(String resource){
       return  new StringBuilder().append(HTTP_PROTOCOL).append(getServerIP()).
                 append(BASE_URL).
                append(resource).append(SLASH).toString();
    }

    private static String getServerIP(){return "searchandfind-001-site1.dtempurl.com";
    }

}
