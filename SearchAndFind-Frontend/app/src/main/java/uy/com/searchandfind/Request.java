package uy.com.searchandfind;

/**
 * Created by strat on 10/10/2017.
 */

public class Request {
    private String AuthenticationType;
    private String CurrentToken;
    private String UserId;

    public String getAuthenticationType() {
        return AuthenticationType;
    }

    public void setAuthenticationType(String authenticationType) {
        AuthenticationType = authenticationType;
    }

    public String getCurrentToken() {
        return CurrentToken;
    }

    public void setCurrentToken(String currentToken) {
        CurrentToken = currentToken;
    }

    public String getUserId() {
        return UserId;
    }

    public void setUserId(String userId) {
        UserId = userId;
    }
}
