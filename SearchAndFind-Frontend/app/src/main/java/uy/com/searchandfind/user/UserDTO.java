package uy.com.searchandfind.user;

import java.util.Collection;

import uy.com.searchandfind.Response;
import uy.com.searchandfind.saler.SalerCategoryDTO;

/**
 * Created by strat on 30/09/2017.
 */

public class UserDTO {
    private String Id ;
    private String DeviceId ;
    private String Name ;
    private String LastName ;
    private String MailAddress ;
    private String CurrentToken ;
    public UserDTO(){

    }
    public String getDeviceId() {
        return DeviceId;
    }

    public void setDeviceId(String aDeviceid) {
        DeviceId = aDeviceid;
    }


    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }
    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getLastName() {
        return LastName;
    }

    public void setLastName(String lastName) {
        LastName = lastName;
    }

    public String getMailAddress() {
        return MailAddress;
    }

    public void setMailAddress(String mailAddress) {
        MailAddress = mailAddress;
    }

    public String getCurrentToken() {
        return CurrentToken;
    }

    public void setCurrentToken(String currentToken) {
        CurrentToken = currentToken;
    }
}
