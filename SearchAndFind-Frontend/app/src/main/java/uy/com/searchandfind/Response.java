package uy.com.searchandfind;

public class Response {
    private boolean Success;
    private String Message;
    private boolean AuthenticationError;

    public Response(){
        Success=true;
    }
    public Response(String message){
        Success=false;
        this.Message=message;
    }

    public boolean isSuccess() {
        return Success;
    }

    public void setSuccess(boolean success) {
        Success = success;
    }

    public String getMessage() {
        return Message;
    }

    public void setMessage(String message) {
        Message = message;
    }

    public boolean isAuthenticationError() {
        return AuthenticationError;
    }

    public void setAuthenticationError(boolean authenticationError) {
        AuthenticationError = authenticationError;
    }
}
